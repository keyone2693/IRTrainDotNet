using IRTrainDotNet.Helpers;
using System.Collections.Generic;

namespace IRTrainDotNet.Models
{
    public class PriceInOtherCurrency
    {
        public TarrifCodes Tarrif { get; set; }
        public Dictionary<string, decimal> PriceInCurrencies { get; set; }
    }
}
