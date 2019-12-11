using System;
using System.Collections.Generic;
using System.Text;

namespace IrFadakTrainDotNet.Models
{
    public class GetOptionalServicesResult
    {
        public int ServiceTypeCode { get; set; }
        public string ServiceTypeName { get; set; }
        public string Description { get; set; }
        public int ShowMoney { get; set; }

    }
}
