using System;

namespace IRTrainDotNet.Models
{
   public class UnlockSeatParams
    {
        public long SaleId { get; set; }
        public int TrainNumber { get; set; }
        public DateTime MoveDate { get; set; }
    }
}
