using System;
using System.Collections.Generic;
using System.Text;

namespace IrFadakTrainDotNet.Models
{
    public class UserSaleMetadata
    {
        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public long SellMasterId { get; set; }
        public int SaleId { get; set; }
        public int PathCode { get; set; }
        public int FromStation { get; set; }
        public string FromStationName { get; set; }
        public int ToStation { get; set; }
        public string ToStationName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public int TrainNumber { get; set; }
        public DateTime MoveDate { get; set; }
        public int WagonType { get; set; }
        public long? Amount { get; set; }
        public DateTime? RegisteredAt { get; set; }
    }
}
