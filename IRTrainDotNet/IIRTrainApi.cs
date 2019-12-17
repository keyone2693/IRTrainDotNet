using IRTrainDotNet.Helpers;
using IRTrainDotNet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IRTrainDotNet
{
    public interface IIRTrainApi
    {
        #region Synchronous
        //-------------------------------------------
        #region Auth
        ServiceResult<string> Login(LoginModel loginModel, Company company);
        bool ValidateTokenWithTime(DateTime addDate);
        bool ValidateTokenWithRequest(string token, Company company);
        #endregion
        #region Stations
        ServiceResult<List<Station>> GetStations(string authToken, Company company);
        ServiceResult<Station> GetStationById(string authToken, int stationId, Company company);
        #endregion
        #region Raja
        ServiceResult<string> GetLastVersion(string authToken, Company company);
        #endregion
        #region Wagon
        ServiceResult<GetWagonAvailableSeatCountResult> GetWagonAvailableSeatCount(string authToken, GetWagonAvailableSeatCountParams getWagonAvailableSeatCountParams, Company company);
        #endregion
        #region Seat
        ServiceResult<LockSeatResult> LockSeat(string authToken, LockSeatParams lockSeatParams, Company company);
        ServiceResult<LockSeatBulkResult> LockSeatBulk(string authToken, LockSeatBulkParams lockSeatBulkParams, Company company);
        ServiceResult<EmptyResult> UnlockSeat(string authToken, UnlockSeatParams unlockSeatParams, Company company);
        #endregion
        #region Ticket
        ServiceResult<int> SaveTicketsInfo(string authToken, SaveTicketsInfoParams saveTicketsInfoParams, Company company);
        ServiceResult<EmptyResult> RegisterTickets(string authToken, RegisterTicketParams registerTicketParams, Company company);
        ServiceResult<TicketReportAResult> TicketReportA(string authToken, TicketReportAParams ticketReportAParams, Company company);
        ServiceResult<RefundTicketInfoResult> RefundTicketInfo(string authToken, RefundTicketInfoParams refundTicketInfoParams, Company company);
        ServiceResult<int> RefundTicket(string authToken, RefundTicketParams refundTicketParams, Company company);
        #endregion
        #region Agent
        ServiceResult<List<UserSaleMetadata>> UserSales(string authToken, Company company);
        ServiceResult<long> AgentCredit(string authToken, Company company);
        #endregion
        //-------------------------------------------
        #endregion

        #region Asynchronous 
        //-------------------------------------------
        #region Auth
        Task<ServiceResult<String>> LoginAsync(LoginModel loginModel, Company company);
        Task<bool> ValidateTokenWithRequestAsync(string token, Company company);
        #endregion
        #region Stations
        Task<ServiceResult<List<Station>>> GetStationsAsync(string authToken, Company company);
        Task<ServiceResult<Station>> GetStationByIdAsync(string authToken, int stationId, Company company);
        #endregion
        #region Raja
        Task<ServiceResult<string>> GetLastVersionAsync(string authToken, Company company);
        #endregion
        #region Wagon
        Task<ServiceResult<GetWagonAvailableSeatCountResult>> GetWagonAvailableSeatCountAsync(string authToken, GetWagonAvailableSeatCountParams getWagonAvailableSeatCountParams, Company company);
        #endregion
        #region Seat
        Task<ServiceResult<LockSeatResult>> LockSeatAsync(string authToken, LockSeatParams lockSeatParams, Company company);
        Task<ServiceResult<LockSeatBulkResult>> LockSeatBulkAsync(string authToken, LockSeatBulkParams lockSeatBulkParams, Company company);
        Task<ServiceResult<EmptyResult>> UnlockSeatAsync(string authToken, UnlockSeatParams unlockSeatParams, Company company);
        #endregion
        #region Ticket
        Task<ServiceResult<int>> SaveTicketsInfoAsync(string authToken, SaveTicketsInfoParams saveTicketsInfoParams, Company company);
        Task<ServiceResult<EmptyResult>> RegisterTicketsAsync(string authToken, RegisterTicketParams registerTicketParams, Company company);
        Task<ServiceResult<TicketReportAResult>> TicketReportAAsync(string authToken, TicketReportAParams ticketReportAParams, Company company);
        Task<ServiceResult<RefundTicketInfoResult>> RefundTicketInfoAsync(string authToken, RefundTicketInfoParams refundTicketInfoParams, Company company);
        Task<ServiceResult<int>> RefundTicketAsync(string authToken, RefundTicketParams refundTicketParams, Company company);

        #endregion
        #region Agent
        Task<ServiceResult<List<UserSaleMetadata>>> UserSalesAsync(string authToken, Company company);
        Task<ServiceResult<long>> AgentCreditAsync(string authToken, Company company);
        #endregion
        //-------------------------------------------
        #endregion
    }
}
