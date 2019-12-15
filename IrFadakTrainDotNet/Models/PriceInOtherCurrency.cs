using IrFadakTrainDotNet.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace IrFadakTrainDotNet.Models
{
    public class PriceInOtherCurrency
    {
        public TarrifCodes Tarrif { get; set; }
        public Dictionary<string, decimal> PriceInCurrencies { get; set; }
    }
}
