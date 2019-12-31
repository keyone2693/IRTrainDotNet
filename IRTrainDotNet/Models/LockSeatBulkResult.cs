using IRTrainDotNet.Helpers;
using System.Collections.Generic;

namespace IRTrainDotNet.Models
{
   public class LockSeatBulkResult
    {
     public   int SellMasterId { get; set; }
        public string WagonNumbers { get; set; }
        public string CompartmentNumbers { get; set; }
        public int SaleId { get; set; }
        public Dictionary<TarrifCodes, int> Prices { get; set; }
        public IEnumerable<GetOptionalServicesResult> OptionalServices { get; set; }
        public IEnumerable<PriceInOtherCurrency> PricesInOtherCurrencies { get; set; }

    }
}
