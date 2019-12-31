using System;

namespace IRTrainDotNet.Models
{
    public class StationTimeLineParams
    {
        public int CircularPeriod { get; set; }
        public int CircularNumberSerial { get; set; }
        public int TrainNumber { get; set; }
        public DateTime MoveDate { get; set; }
    }

}
