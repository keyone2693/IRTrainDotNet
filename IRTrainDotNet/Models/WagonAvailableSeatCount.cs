using System;

namespace IRTrainDotNet.Models
{
    public class WagonAvailableSeatCount
    {
        public string SelectionHint { get; set; }
        public string WagonName { get; set; }
        public int CircularNumberSerial { get; set; }
        public int RateCode { get; set; }
        public int PathCode { get; set; }
        public int WagonType { get; set; }
        public int TrainNumber { get; set; }
        public int CircularPeriod { get; set; }
        public string ExitTime { get; set; }
        public DateTime MoveDate { get; set; }
        public bool IsCompartment { get; set; }
        public int CompartmentCapicity { get; set; }
        public int Degree { get; set; }
        public decimal Capacity { get; set; }
        public int Cost { get; set; }
        public int CountingAll { get; set; }
        public bool HasAirConditioning { get; set; }
        public bool HasMedia { get; set; }
        public int FullPrice { get; set; }
        public string TimeOfArrival { get; set; }
        public int RetStatus { get; set; }
        public string DisabledReason { get; set; }
        public int SoldCount { get; set; }
        public int RationCode { get; set; }
        public int MinutesToExitDateTime { get; set; }
    }
}
