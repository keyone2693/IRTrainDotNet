using System;
using System.Collections.Generic;
using System.Text;

namespace IrFadakTrainDotNet.Models
{
   public class LockSeatBulkParams
    {
        public WagonAvailableSeatCount SelectedWagon { get; set; }
        public int FromStation { get; set; }
        public int ToStation { get; set; }
        public int Gender { get; set; }
        public int SeatCount { get; set; }
        public long SellMaster { get; set; }
        public bool IsExclusiveCompartment { get; set; }

    }
}
