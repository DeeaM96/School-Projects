using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOG430_VI
{
    public class MqttOutput
    {
        public string topic { get; set; }
        public string msg { get; set; }

        public MqttOutput(string topic, string msg)
        {
            this.topic = topic;
            this.msg = msg;
        }

        public override string ToString()
        {
            return "Received : " + msg + " on topic " + topic;
        }
    }
}
