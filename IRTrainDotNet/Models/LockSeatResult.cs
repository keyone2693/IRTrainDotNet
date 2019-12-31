using IRTrainDotNet.Helpers;
using System.Collections.Generic;

namespace IRTrainDotNet.Models
{
  public  class LockSeatResult
    {
        public int SellMasterId { get; set; }
        public int MoneyFood { get; set; }
        public long SalonNo { get; set; }
        public long CompartmentNo { get; set; }
        public int SaleId { get; set; }
        public Dictionary<TarrifCodes, int> Prices { get; set; }
        public List<GetOptionalServicesResult> OptionalServices { get; set; }
    }
}
