using System;
using System.Collections.Generic;
using System.Text;

namespace IRTrainDotNet.Models
{
  public  class SaveTicketsInfoParams
    {
        public List<PassengerInfo> PassengersInfo { get; set; }
        public long SaleId { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
    }
}
