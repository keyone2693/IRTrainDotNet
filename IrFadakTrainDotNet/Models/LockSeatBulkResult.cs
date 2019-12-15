using IrFadakTrainDotNet.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace IrFadakTrainDotNet.Models
{
   public class LockSeatBulkResult
    {
     public   int SellMasterId { get; set; }
        public string WagonNumbers { get; set; }
        public string CompartmentNumbers { get; set; }
        public int SaleId { get; set; }
        public Dictionary<TarrifCodes, int> Prices { get; set; }
        public List<GetOptionalServicesResult> OptionalServices { get; set; }
        public List<PriceInOtherCurrency> PricesInOtherCurrencies { get; set; }

    }
}
