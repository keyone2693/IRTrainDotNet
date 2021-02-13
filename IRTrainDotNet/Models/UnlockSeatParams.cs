using System;

namespace IRTrainDotNet.Models
{
   public class UnlockSeatParams
    {
        public long SaleId { get; set; }
        public int TrainNumber { get; set; }
        /// <summary>
        /// IT SHOULD BE LOCAL TIME IRAN
        /// </summary>
        public DateTime MoveDate { get; set; }
    }
}
