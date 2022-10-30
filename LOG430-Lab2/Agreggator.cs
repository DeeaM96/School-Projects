using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LOG430_VI
{
    public class Aggregator
    {
        public MqttApi mqttApi { get; set; }
        public Thread aggregatorThread { get; set; }
        public string topic { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public TimeSpan interval { get; set; }
        public bool realTime { get; set; }
        public Result result { get; set; }

        public Aggregator(MqttApi mqttApi, string topic, DateTime startDate, DateTime endDate, TimeSpan interval, bool realTime)
        {
            this.mqttApi = mqttApi;
            this.topic = topic;
            this.startDate = realTime ? DateTime.Now : startDate;
            this.endDate = endDate;
            this.interval = interval;
            this.realTime = realTime;
        }

        public class Result
        {
            public double maximum { get; set; }
            public double minimum { get; set; }
            public double average { get; set; }
            public double median { get; set; }

            public Result(double maximum, double minimum, double average, double median)
            {
                this.maximum = maximum;
                this.minimum = minimum;
                this.average = average;
                this.median = median;
            }
        }
    }
}
