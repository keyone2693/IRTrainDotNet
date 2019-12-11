using System;
using System.Collections.Generic;
using System.Text;

namespace IrFadakTrainDotNet.Models
{
  public  class RefundTicketInfoParams
    {
        public int SaleId { get; set; }
        public string TicketSeries { get; set; }
        public int SaleCenterCode { get; set; }
        public int WagonNumber { get; set; }
        public int SeatNumber { get; set; }
    }
}
