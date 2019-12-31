using System;

namespace IRTrainDotNet.Models
{
   public class PassengerInfo
    {
       public string Name { get; set; }
        public string Family { get; set; }
        public DateTime BirthDate { get; set; }
        public string NationalCode { get; set; }
        public int Tariff { get; set; }
        public int OptionalServiceId { get; set; }
        public string PromotionCode { get; set; }
    }
}
