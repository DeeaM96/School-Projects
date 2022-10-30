using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOG430_VI
{
    public class Output
    {
        public DateTime time { get; set; }
        public string topic { get; set; }
        public string msg { get; set; }

        public Output(DateTime time, string topic, string msg)
        {
            this.time = time;
            this.topic = topic;
            this.msg = msg;
        }

        public override string ToString()
        {
            return time + " Received : " + msg + " on topic " + topic;
        }
    }
}
