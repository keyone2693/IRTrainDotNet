using System;
using System.Collections.Generic;
using System.Text;

namespace IRTrainDotNet.Helpers
{
    public static class ApiUrl
    {
        public const string FadakBaseUrl = "https://api.fadaktrains.com/rpTicketing";
        //Raja Still Under Dev
        public const string RajaBaseUrl = "https://hostservice.raja.ir/Api/WebSiteService";
        //Safir Completely Under Dev
        public const string SafirBaseUrl = "http://www.safirrail.ir";

        public const string Login = "/Login";
        public const string Stations = "/Station";
        public const string Station = "/Station/"; // + "{StationId}"
        public const string GetLastVersion = "/GetLastVersion";
        public const string GetWagonAvailableSeatCount = "/GetWagonAvailableSeatCount";
        public const string LockSeat = "/LockSeat";
        public const string LockSeatBulk = "/LockSeatBulk";
        public const string UnlockSeat = "/UnlockSeat";
        public const string SaveTicketsInfo = "/SaveTicketsInfo";
        public const string RegisterTickets = "/RegisterTickets";
        public const string TicketReportA = "/TicketReportA";
        public const string RefundTicketInfo = "/RefundTicketInfo";
        public const string RefundTicket = "/RefundTicket";
        public const string UserSales = "/UserSales";
        public const string AgentCredit = "/AgentCredit";

        public const string GetTrainWagonMessage = "/GetTrainWagonMessage";
        public const string StationTimeLine = "/StationTimeLine";
        public const string GetWagonImage = "/GetWagonImage";
    }
}
