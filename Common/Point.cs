using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Point
    {
        public int index { get; set; }
        public float value { get; set; }
        public DateTime time { get; set; }

        //Cach collect data truoc do bao lau
        public int lenghBeforeDay { get; set; } = 0;
        public Point()
        {

        }
        public Point(int newIndex,float newValue,DateTime newTime)
        {
            this.index = newIndex;
            this.value = newValue;
            this.time = newTime;
        }
    }
}
