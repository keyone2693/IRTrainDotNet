using System;
using System.Collections.Generic;
using System.Text;

namespace IrFadakTrainDotNet.Models
{
 public   class TicketReportAResult
    {
        public string CompanyName { get; set; }
        public string StartStationName { get; set; }
        public string EndStationName { get; set; }
        public DateTime MoveDate { get; set; }
        public string MoveTime { get; set; }
        public int WagonNumber { get; set; }
        public int SeatNumber { get; set; }
        public int SexCode { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string TariffName { get; set; }
        public string RationName { get; set; }
        public DateTime RegisterDate { get; set; }
        public int TrainNumber { get; set; }
        public string WagonTypeName { get; set; }
        public int Degree { get; set; }
        public string TimeOfArrival { get; set; }
        public string TrainMessage { get; set; }
        public int TicketNumber { get; set; }
        public int TicketSeries { get; set; }
        public int SaleCenterCode { get; set; }
        public int Formula10 { get; set; }
        public int Formula12 { get; set; }
        public int Formula18 { get; set; }
        public int SellerCode { get; set; }
        public int SecurityNumber { get; set; }
        public string Base64BarcodeImage { get; set; }
        public string Tel { get; set; }
        public int OwnerCode { get; set; }

    }
}
