

namespace IRTrainDotNet.Models
{
   public class RefundTicketParams
    {
        public int SaleId { get; set; }
        public string TicketSeries { get; set; }
        public int SaleCenterCode { get; set; }
        public int WagonNumber { get; set; }
        public int SeatNumber { get; set; }
    }
}
