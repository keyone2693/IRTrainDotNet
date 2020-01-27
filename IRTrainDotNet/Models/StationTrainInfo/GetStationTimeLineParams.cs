using System;
using System.Collections.Generic;
using System.Text;

namespace IRTrainDotNet.Models.StationTrainInfo
{
    public class GetStationTimeLineParams
    {

        public int CircularPeriod { get; set; }
        public int CircularNumberSerial { get; set; }
        public int pTrainNumber { get; set; }
        public DateTime MoveDate { get; set; }
    }

}
