using System;

namespace Common
{
    public class DataPrediction
    {
        public DateTime timeOfValue { get; set; }
        public float value { get; set; }

        public bool isIPoint { get; set; } = false;

        public DataPrediction(DateTime newTime, float newValue)
        {
            this.value = newValue;
            this.timeOfValue = newTime;
        }

        public void setValue (float newValue)
        {
            this.value = newValue;
        }

        public void setTimeOfValue (DateTime newTime)
        {
            this.timeOfValue = newTime;
        }

        public float getValue()
        {
            return this.value;
        }

        public DateTime getTime()
        {
            return this.timeOfValue;
        }
    }
}
