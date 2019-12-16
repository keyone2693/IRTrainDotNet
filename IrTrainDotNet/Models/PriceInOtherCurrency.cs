using IrTrainDotNet.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace IrTrainDotNet.Models
{
    public class PriceInOtherCurrency
    {
        public TarrifCodes Tarrif { get; set; }
        public Dictionary<string, decimal> PriceInCurrencies { get; set; }
    }
}
