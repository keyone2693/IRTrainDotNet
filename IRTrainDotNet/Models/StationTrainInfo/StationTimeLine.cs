using System;
using System.Collections.Generic;
using System.Text;

namespace IRTrainDotNet.Models.StationTrainInfo
{
    public class StationTimeLine
    {
        public short StationStatus { get; set; } = 0;
        public int Index { get; set; } = 0;
        public string? TimeStop { get; set; }
        public int StationCode { get; set; }
        public string StationName { get; set; }
        public string EnterTime { get; set; }
        public string EnterTimeF { get; set; }
        public DateTime? ExitTime { get; set; }
        public DateTime? DayAfter { get; set; }
        public string? Row { get; set; }
        public string? ExitTimeProgram { get; set; }
        public string? Width { get; set; }
    }

}
