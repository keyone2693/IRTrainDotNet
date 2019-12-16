using System;
using System.Collections.Generic;
using System.Text;

namespace IrTrainDotNet.Models
{
    public class GetTrainWagonMessageParams
    {
        public int CircularPeriod { get; set; }
        public int CircularNumberSerial { get; set; }
        public int TrainNumber { get; set; }
        public int WagonTaype { get; set; }
        public string MoveDate { get; set; }
    }



}
