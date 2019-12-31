using System.Collections.Generic;

namespace IRTrainDotNet.Models
{
  public  class SaveTicketsInfoParams
    {
        public IEnumerable<PassengerInfo> PassengersInfo { get; set; }
        public long SaleId { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
    }
}
