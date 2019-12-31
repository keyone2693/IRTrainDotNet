using System;

namespace IRTrainDotNet.Models
{
    public class StationTimeLineResult
    {
        public DateTime TimeStop { get; set; }
        public int StationCode { get; set; }
        public string StationName { get; set; }
        public DateTime EnterTime { get; set; }
        public string EnterTimeF { get; set; }
        public DateTime ExitTime { get; set; }
        public int DayAfter { get; set; }
        public int Row { get; set; }
        public DateTime ExitTimeProgram { get; set; }
        public string Width { get; set; }
    }

}
