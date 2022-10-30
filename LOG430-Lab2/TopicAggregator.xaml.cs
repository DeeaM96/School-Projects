using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LOG430_VI
{
    /// <summary>
    /// Interaction logic for DataAggregator.xaml
    /// </summary>
    public partial class TopicAggregator : Window
    {
        private Aggregator aggregator;
        private MqttApi mqttApi;
        private string path = Environment.CurrentDirectory + @"\detector\detector{0}.txt";

        public TopicAggregator(MqttApi mqttApi, string topic)
        {
            InitializeComponent();

            this.mqttApi = mqttApi;
            selectedTopic.Content = topic;
        }

        private void AggregateTopic_Click(object sender, RoutedEventArgs e)
        {
            startAggregateTopic.IsEnabled = false;
            cancelAggregateTopic.IsEnabled = true;

            try
            {
                aggregator = new Aggregator(mqttApi, selectedTopic.Content.ToString(), DateTime.Parse(startDatePicker.Text), DateTime.Parse(endDatePicker.Text), TimeSpan.Parse(intervalTimeSpan.Text), (bool)nowCheckBox.IsChecked);

                if (aggregator.realTime)
                    aggregator.aggregatorThread = new Thread(realTimeAggregate);
                else aggregator.aggregatorThread = new Thread(pastAggregate);

                aggregator.aggregatorThread.Start(aggregator);
            }
            catch (ArgumentNullException argumentNullException)
            {
                Console.Error.WriteLine(argumentNullException);
                startAggregateTopic.IsEnabled = true;
                cancelAggregateTopic.IsEnabled = false;
                MessageBox.Show("The dates can't be empty.\nPlease enter new dates and try again.", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void isActive(object thread)
        {
            Thread t = (Thread)thread;
            while (t.IsAlive)
            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    aggregatorOutput.Items.Insert(0, new Output(DateTime.Now, aggregator.topic, "Aggregator is alive..."));
                });
                Thread.Sleep(1000);
            }
        }

        private void pastAggregate(object param)
        {
            //FIXME Interval
            try
            {
                Aggregator aggregator = (Aggregator)param;

                DateTime startDate = aggregator.startDate;
                DateTime endDate = aggregator.endDate;
                TimeSpan interval = aggregator.interval;

                if (DateTime.Compare(startDate, endDate) < 0)
                {
                    new Thread(isActive).Start(aggregator.aggregatorThread);

                    List<double> values = new List<double>();
                    List<int> indexes = getDetector(startDate, endDate);

                    if (indexes.Count > 0)
                    {
                        foreach (int i in indexes)
                        {
                            foreach (string line in ReadLines(String.Format(path, i)))
                            {
                                string topic = line.Split(';')[1];
                                string[] json = line.Split('{', '}');
                                DateTime date = DateTime.Parse(line.Substring(0, 19));

                                if (DateTime.Compare(date, startDate) >= 0
                                    && DateTime.Compare(date, endDate) <= 0
                                    && String.Equals(topic, aggregator.topic, StringComparison.OrdinalIgnoreCase))
                                {
                                    string jsontext = "{" + json[1] + "}";

                                    dynamic data = JObject.Parse(jsontext);

                                    try
                                    {
                                        if (data.Value != 0)
                                        {
                                            values.Add(Convert.ToDouble(data.Value));
                                        }
                                    }
                                    catch (Exception exception)
                                    {
                                        Console.Error.WriteLine(exception);
                                        break;
                                    }
                                }
                            }
                        }

                        calculate(values, aggregator);
                    }
                    else
                    {
                        Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            aggregatorOutput.Items.Insert(0, new Output(DateTime.Now, aggregator.topic, "Aggregator was cancelled. No data exists for the dates entered."));
                        });
                    }
                }
                else
                {
                    MessageBox.Show("The end date has to be after the start date.", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (ThreadAbortException ex)
            {
            }

            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                startAggregateTopic.IsEnabled = true;
                cancelAggregateTopic.IsEnabled = false;
            });
        }

        private void realTimeAggregate(object param)
        {
            try
            {
                Aggregator aggregator = (Aggregator)param;

                DateTime startDate = aggregator.startDate;
                DateTime endDate = aggregator.endDate;
                TimeSpan interval = aggregator.interval;

                if (DateTime.Compare(startDate, endDate) < 0)
                {
                    new Thread(isActive).Start(aggregator.aggregatorThread);

                    aggregator.mqttApi.tmpOutput = new List<string>();

                    DateTime start = startDate;
                    while (DateTime.Compare(DateTime.Now, endDate) <= 0)
                    {
                        DateTime now = DateTime.Now;
                        if (TimeSpan.Compare(now.Subtract(start), interval) >= 0)
                        {
                            new Thread(realTimeAggregateExport).Start(new object[2] { aggregator.mqttApi.tmpOutput, aggregator });
                            aggregator.mqttApi.tmpOutput = new List<string>();
                            start = now;
                        }
                    }

                    if (DateTime.Compare(DateTime.Now, endDate) > 0)
                    {
                        Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            aggregatorOutput.Items.Insert(0, new Output(DateTime.Now, aggregator.topic, "Aggregator is done."));
                        });
                    }
                }
                else
                {
                    MessageBox.Show("The end date has to be after the start date.", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (ThreadAbortException ex)
            {
            }

            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                startAggregateTopic.IsEnabled = true;
                cancelAggregateTopic.IsEnabled = false;
            });
        }

        private void realTimeAggregateExport(object param)
        {
            Array args = (Array)param;
            List<double> values = new List<double>();
            List<string> lines = (List<string>)args.GetValue(0);
            Aggregator aggregator = (Aggregator)args.GetValue(1);

            foreach (string line in lines)
            {
                string topic = line.Split(';')[0];
                string[] json = line.Split('{', '}');

                if (String.Equals(topic, aggregator.topic, StringComparison.OrdinalIgnoreCase))
                {
                    string jsontext = "{" + json[1] + "}";

                    dynamic data = JObject.Parse(jsontext);

                    try
                    {
                        if (data.Value != 0)
                        {
                            values.Add(Convert.ToDouble(data.Value));
                        }
                    }
                    catch (Exception exception)
                    {
                        Console.Error.WriteLine(exception);
                        break;
                    }
                }
            }

            calculate(values, aggregator);
        }

        private List<int> getDetector(DateTime startDate, DateTime endDate)
        {
            List<int> indexes = new List<int>();
            for (int i = 1; i <= 5; i++)
            {
                DateTime lineStart = DateTime.Parse(File.ReadLines(String.Format(path, i)).First().Substring(0, 19));
                DateTime lineEnd = DateTime.Parse(File.ReadLines(String.Format(path, i)).Last().Substring(0, 19));

                if ((i == 1 && DateTime.Compare(startDate, lineStart) < 0)
                    || (i == 5 && DateTime.Compare(endDate, lineEnd) > 0))
                {
                    return new List<int>();
                }
                else if (DateTime.Compare(endDate, lineStart) >= 0 && DateTime.Compare(startDate, lineEnd) <= 0)
                {
                    indexes.Add(i);
                }
            }

            return indexes;
        }

        private async void calculate(List<double> values, Aggregator aggregator)
        {
            if (values.Count > 0)
            {
                double Max = values.Max();
                double Min = values.Min();
                double Avg = values.Average();
                double Med = 0;
                values = values.OrderBy(a => a).ToList();
                int count = values.Count();
                if (count % 2 == 0)
                {
                    var mindex = count / 2;
                    Med = (values[mindex - 1] + values[mindex]) / 2;
                }
                else
                {
                    var mindex = (count + 1) / 2;
                    Med = values[mindex - 1];
                }

                aggregator.result = new Aggregator.Result(Max, Min, Avg, Med);
                await ExportToCSV.ExportToCSVAsync(aggregator);

                isDone(aggregator);
            }
            else
            {
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    aggregatorOutput.Items.Insert(0, new Output(DateTime.Now, aggregator.topic, "Aggregator was cancelled. No values found."));
                });
            }
        }

        private void isDone(Aggregator aggregator)
        {
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                aggregatorOutput.Items.Insert(0, new Output(DateTime.Now, aggregator.topic, "Aggregator is completed. The result was exported to vi_db.csv!"));
            });
        }

        static IEnumerable<string> ReadLines(string filename)
        {
            using (TextReader reader = File.OpenText(filename))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }

        private void CancelAggregateTopic_Click(object sender, RoutedEventArgs e)
        {
            if (aggregator != null)
            {
                aggregatorOutput.Items.Insert(0, new Output(DateTime.Now, aggregator.topic, "Aggregator was cancelled."));
                startAggregateTopic.IsEnabled = true;
                cancelAggregateTopic.IsEnabled = false;

                aggregator.aggregatorThread.Abort();
            }
        }

        private void NowCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            startDatePicker.IsEnabled = false;
            endDatePicker.Value = DateTime.Now;
        }

        private void NowCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            startDatePicker.IsEnabled = true;
            endDatePicker.Value = DateTime.Parse("2019-06-02 23:23");
        }
    }
}
