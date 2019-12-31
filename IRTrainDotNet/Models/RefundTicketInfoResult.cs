using System;

namespace IRTrainDotNet.Models
{
  public  class RefundTicketInfoResult
    {
        public int RetStatus { get; set; }
        public int Id { get; set; }
        public int Serial { get; set; }
        public int TrainNumber { get; set; }
        public int WagonType { get; set; }
        public int WagonNumber { get; set; }
        public int CompartmentNumber { get; set; }
        public int RateCode { get; set; }
        public int SeatNumber { get; set; }
        public int RationCode { get; set; }
        public int SexCode { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string NationalCode { get; set; }
        public string Mobile { get; set; }
        public int Tariff { get; set; }
        public int Degree { get; set; }
        public int TicketType { get; set; }
        public int ReduplicateId { get; set; }
        public int Status { get; set; }
        public int Formula7 { get; set; }
        public int SaleCenterCode { get; set; }
        public DateTime RegisteredAt { get; set; }
        public int IsPrintable { get; set; }
        public DateTime MoveDateTime { get; set; }
        public int HalfPrice { get; set; }
        public int FullPrice { get; set; }
        public int PathCode { get; set; }
        public int TrainHaveService { get; set; }
        public DateTime MoveDateTrain { get; set; }
        public int OptionalServiceId { get; set; }
        public int ServicesAmount { get; set; }
        public int CircularNumberSerial { get; set; }
        public int SaleId { get; set; }
        public int TicketTypeCode { get; set; }
        public string TicketTypeName { get; set; }
        public int IsPrintMoney { get; set; }
        public int IsReduplicate { get; set; }
        public int IsRuind { get; set; }
        public int IsRuindAfterRun { get; set; }
        public int SmCount { get; set; }
        public int AppType { get; set; }
        public int RetStatusRuind { get; set; }
        public int RetCode { get; set; }
        public int OldTicketId { get; set; }
        public int AppCodeOld { get; set; }
        public int RuindType { get; set; }
        public int PenaltyPercentage { get; set; }
        public int PenaltyAmount { get; set; }
        public int TotalAmount { get; set; }
        public int CircularPeriod { get; set; }
        public int OwnerCode { get; set; }
    }
}
