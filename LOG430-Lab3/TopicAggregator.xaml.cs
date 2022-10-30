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
        private List<Aggregator> aggregators = new List<Aggregator>();
        private bool start = false;
        private string topic;
        private string path = Environment.CurrentDirectory + @"\detector\detector{0}.txt";

        public TopicAggregator(string topic)
        {
            InitializeComponent();

            nowCheckBox.IsChecked = true;
            this.topic = topic;
            selectedTopic.Content = topic;
        }

        private void AggregateTopic_Click(object sender, RoutedEventArgs e)
        {
            start = true;
            startAggregateTopic.IsEnabled = false;
            cancelAggregateTopic.IsEnabled = true;

            aggregators = new List<Aggregator>();

            if ((bool)maximumChkBox.IsChecked) addAggregator(AggregatorType.MAXIMUM);
            if ((bool)minimumChkBox.IsChecked) addAggregator(AggregatorType.MINIMUM);
            if ((bool)averageChkBox.IsChecked) addAggregator(AggregatorType.AVERAGE);
            if ((bool)medianChkBox.IsChecked) addAggregator(AggregatorType.MEDIAN);
        }

        private void addAggregator(AggregatorType type)
        {
            try
            {
                aggregators.Add(new Aggregator(new MqttApi(topic), type, selectedTopic.Content.ToString(), DateTime.Parse(startDatePicker.Text), DateTime.Parse(endDatePicker.Text), TimeSpan.Parse(intervalTimeSpan.Text), (bool)nowCheckBox.IsChecked));

                if (aggregators.Last().realTime)
                    aggregators.Last().aggregatorThread = new Thread(realTimeAggregate);
                else aggregators.Last().aggregatorThread = new Thread(pastAggregate);

                aggregators.Last().aggregatorThread.Start(aggregators.Last());
            }
            catch (ArgumentNullException argumentNullException)
            {
                Console.Error.WriteLine(argumentNullException);
                start = false;
                startAggregateTopic.IsEnabled = true;
                cancelAggregateTopic.IsEnabled = false;
                MessageBox.Show("The dates can't be empty.\nPlease enter new dates and try again.", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void removeAggregator(AggregatorType type)
        {
            Aggregator aggregator = aggregators.Find(x => x.type == type);
            if (aggregator != null)
            {
                aggregatorOutput.Items.Insert(0, new Output(DateTime.Now, aggregator.topic, aggregator.type + " Aggregator was cancelled."));
                aggregator.aggregatorThread.Abort();
                aggregators.Remove(aggregator);
            }
        }

        private void isActive(object param)
        {
            Aggregator aggregator = (Aggregator)param;
            while (aggregator.aggregatorThread.IsAlive)
            {
                if (aggregator.mqttApi.client.IsConnected)
                {
                    if (aggregator.timeout)
                        Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            aggregatorOutput.Items.Insert(0, new Output(DateTime.Now, aggregator.topic, aggregator.type + " Aggregator is experiencing some delays. Please wait..."));
                        });
                    else
                        Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            aggregatorOutput.Items.Insert(0, new Output(DateTime.Now, aggregator.topic, aggregator.type + " Aggregator is alive..."));
                        });
                }
                else
                {
                    Application.Current.Dispatcher.InvokeAsync(() =>
                    {
                        aggregatorOutput.Items.Insert(0, new Output(DateTime.Now, aggregator.topic, aggregator.type + " Aggregator is trying to reconnect..."));
                    });
                }
                Thread.Sleep(2000);
            }
        }

        private void pastAggregate(object param)
        {
            //TODO Add Interval
            try
            {
                Aggregator aggregator = (Aggregator)param;

                DateTime startDate = aggregator.startDate;
                DateTime endDate = aggregator.endDate;
                TimeSpan interval = aggregator.interval;

                if (DateTime.Compare(startDate, endDate) < 0)
                {
                    new Thread(isActive).Start(aggregator);

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
                start = false;
                startAggregateTopic.IsEnabled = true;
                cancelAggregateTopic.IsEnabled = false;
            });
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

        private void realTimeAggregate(object param)
        {
            Aggregator aggregator = (Aggregator)param;

            try
            {
                DateTime startDate = aggregator.startDate;
                DateTime endDate = aggregator.endDate;
                TimeSpan interval = aggregator.interval;

                if (DateTime.Compare(startDate, endDate) < 0)
                {
                    Thread active = new Thread(isActive);
                    active.Start(aggregator);

                    aggregator.mqttApi.aggregatorOutput = new List<Output>();

                    DateTime start = startDate;
                    while (DateTime.Compare(DateTime.Now, endDate) <= 0 && aggregator.mqttApi.client.IsConnected)
                    {
                        /*if (aggregator.timeout)
                        {
                            Thread.Sleep(15000);
                            aggregator.timeout = false;
                        }*/

                        DateTime now = DateTime.Now;

                        if (TimeSpan.Compare(now.Subtract(start), interval) >= 0)
                        {
                            if (aggregator.mqttApi.aggregatorOutput == null)
                                aggregator.timeout = true;
                            else aggregator.timeout = false;

                            if (!aggregator.timeout)
                                new Thread(realTimeAggregateExport).Start(new object[2] { aggregator.mqttApi.aggregatorOutput, aggregator });

                            aggregator.mqttApi.aggregatorOutput = new List<Output>();
                            start = now;
                        }
                    }

                    if (DateTime.Compare(DateTime.Now, endDate) > 0)
                    {
                        Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            aggregatorOutput.Items.Insert(0, new Output(DateTime.Now, aggregator.topic, aggregator.type + " Aggregator is done."));
                        });
                    }
                    else if (!aggregator.mqttApi.client.IsConnected)
                    {
                        Application.Current.Dispatcher.InvokeAsync(() =>
                        {
                            aggregatorOutput.Items.Insert(0, new Output(DateTime.Now, aggregator.topic, aggregator.type + " Aggregator was disconnected."));
                        });

                        SpinWait.SpinUntil(delegate
                        {
                            return aggregator.mqttApi.client.IsConnected;
                        });
                        active.Abort();
                        realTimeAggregate(aggregator);
                    }
                }
                else
                {
                    MessageBox.Show("The end date has to be after the start date.", "Warning!", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (ThreadAbortException ex)
            {
                Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    if (aggregator.type == AggregatorType.MAXIMUM) maximumChkBox.IsChecked = false;
                    else if (aggregator.type == AggregatorType.MINIMUM) minimumChkBox.IsChecked = false;
                    else if (aggregator.type == AggregatorType.AVERAGE) averageChkBox.IsChecked = false;
                    else if (aggregator.type == AggregatorType.MEDIAN) medianChkBox.IsChecked = false;
                });
            }

            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                start = false;
                startAggregateTopic.IsEnabled = true;
                cancelAggregateTopic.IsEnabled = false;
            });
        }

        private void realTimeAggregateExport(object param)
        {
            Array args = (Array)param;
            List<double> values = new List<double>();
            List<Output> aggregatorOutput = (List<Output>)args.GetValue(0);
            Aggregator aggregator = (Aggregator)args.GetValue(1);

            foreach (Output output in aggregatorOutput)
            {
                if (String.Equals(output.topic, aggregator.topic, StringComparison.OrdinalIgnoreCase))
                {
                    dynamic data = (JObject)JsonConvert.DeserializeObject(output.msg);
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

        private async void calculate(List<double> values, Aggregator aggregator)
        {
            if (values.Count > 0)
            {
                double result = 0;
                if (aggregator.type == AggregatorType.MAXIMUM)
                    result = values.Max();
                else if (aggregator.type == AggregatorType.MINIMUM)
                    result = values.Min();
                else if (aggregator.type == AggregatorType.AVERAGE)
                    result = values.Average();
                else if (aggregator.type == AggregatorType.MEDIAN)
                {
                    values = values.OrderBy(a => a).ToList();
                    int count = values.Count();
                    if (count % 2 == 0)
                    {
                        var mindex = count / 2;
                        result = (values[mindex - 1] + values[mindex]) / 2;
                    }
                    else
                    {
                        var mindex = (count + 1) / 2;
                        result = values[mindex - 1];
                    }
                }

                aggregator.result = result;
                await ExportToCSV.ExportToCSVAsync(aggregator);

                isDone(aggregator);
            }
            else
            {
                await Application.Current.Dispatcher.InvokeAsync(() =>
                {
                    aggregatorOutput.Items.Insert(0, new Output(DateTime.Now, aggregator.topic, aggregator.type + " Aggregator was cancelled. No values found."));
                });
            }
        }

        private void isDone(Aggregator aggregator)
        {
            Application.Current.Dispatcher.InvokeAsync(() =>
            {
                aggregatorOutput.Items.Insert(0, new Output(DateTime.Now, aggregator.topic, aggregator.type + " Aggregator is completed. The result was exported to vi_db.csv!"));
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
            start = false;
            startAggregateTopic.IsEnabled = true;
            cancelAggregateTopic.IsEnabled = false;

            if (aggregators.Count > 0)
            {
                foreach (Aggregator aggregator in aggregators)
                {
                    aggregatorOutput.Items.Insert(0, new Output(DateTime.Now, aggregator.topic, aggregator.type + " Aggregator was cancelled."));
                    aggregator.aggregatorThread.Abort();
                }
            }
        }

        #region Checkboxes
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

        private void MaximumChkBox_Click(object sender, RoutedEventArgs e)
        {
            if (start)
            {
                if ((bool)maximumChkBox.IsChecked)
                {
                    addAggregator(AggregatorType.MAXIMUM);
                }
                else
                {
                    removeAggregator(AggregatorType.MAXIMUM);
                }
            }
        }

        private void MinimumChkBox_Click(object sender, RoutedEventArgs e)
        {
            if (start)
            {
                if ((bool)minimumChkBox.IsChecked)
                {
                    addAggregator(AggregatorType.MINIMUM);
                }
                else
                {
                    removeAggregator(AggregatorType.MINIMUM);
                }
            }
        }

        private void AverageChkBox_Click(object sender, RoutedEventArgs e)
        {
            if (start)
            {
                if ((bool)averageChkBox.IsChecked)
                {
                    addAggregator(AggregatorType.AVERAGE);
                }
                else
                {
                    removeAggregator(AggregatorType.AVERAGE);
                }
            }
        }

        private void MedianChkBox_Click(object sender, RoutedEventArgs e)
        {
            if (start)
            {
                if ((bool)medianChkBox.IsChecked)
                {
                    addAggregator(AggregatorType.MEDIAN);
                }
                else
                {
                    removeAggregator(AggregatorType.MEDIAN);
                }
            }
        }
        #endregion

        private void DisconnectMaximum_Click(object sender, RoutedEventArgs e)
        {
            if (start)
            {
                Aggregator aggregator = aggregators.Find(x => x.type == AggregatorType.MAXIMUM);
                if (aggregator != null)
                {
                    aggregator.mqttApi.setForceDisconnectAggregator();
                    aggregator.mqttApi.DisconnectClient();
                }
            }
        }

        private void TimeoutAverage_Click(object sender, RoutedEventArgs e)
        {
            if (start)
            {
                Aggregator aggregator = aggregators.Find(x => x.type == AggregatorType.AVERAGE);
                if (aggregator != null)
                {
                    //aggregator.timeout = true;
                    aggregator.mqttApi.setForceTimeoutAggregator();
                }
            }
        }
    }
}
