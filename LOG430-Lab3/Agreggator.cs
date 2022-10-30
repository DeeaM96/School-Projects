using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LOG430_VI
{
    public enum AggregatorType
    {
        MAXIMUM = 1,
        MINIMUM = 2,
        AVERAGE = 3,
        MEDIAN = 4
    }

    public class Aggregator
    {
        public bool timeout { get; set; } = false;

        public MqttApi mqttApi { get; set; }
        public Thread aggregatorThread { get; set; }
        public string topic { get; set; }
        public AggregatorType type { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public TimeSpan interval { get; set; }
        public bool realTime { get; set; }

        public double result { get; set; }

        public Aggregator(MqttApi mqttApi, AggregatorType type, string topic, DateTime startDate, DateTime endDate, TimeSpan interval, bool realTime)
        {
            this.mqttApi = mqttApi;
            this.type = type;
            this.topic = topic;
            this.startDate = realTime ? DateTime.Now : startDate;
            this.endDate = endDate;
            this.interval = interval;
            this.realTime = realTime;
        }
    }
}
