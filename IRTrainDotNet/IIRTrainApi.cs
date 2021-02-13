using IRTrainDotNet.Helpers;
using IRTrainDotNet.Models;
using IRTrainDotNet.Models.StationTrainInfo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IRTrainDotNet
{
    public interface IIRTrainApi
    {
        #region Asynchronous 
        //-------------------------------------------
        #region Auth
        Task<ServiceResult<String>> LoginAsync(LoginModel loginModel, Company company);
        Task<bool> ValidateTokenWithRequestAsync(string token, Company company);
        #endregion
        #region Stations
        Task<ServiceResult<IEnumerable<Station>>> GetStationsAsync(string authToken, Company company);
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
        Task<ServiceResult<IEnumerable<TicketReportAResult>>> TicketReportAAsync(string authToken, TicketReportAParams ticketReportAParams, Company company);
        Task<ServiceResult<RefundTicketInfoResult>> RefundTicketInfoAsync(string authToken, RefundTicketInfoParams refundTicketInfoParams, Company company);
        Task<ServiceResult<int>> RefundTicketAsync(string authToken, RefundTicketParams refundTicketParams, Company company);

        #endregion

        #region StationTrainInfo
        Task<ServiceResult<IEnumerable<StationTimeLine>>> GetStationTimeLineAsync(string authToken, GetStationTimeLineParams getStationTimeLineParams, Company company);

        #endregion
        #region Agent
        Task<ServiceResult<IEnumerable<UserSaleMetadata>>> UserSalesAsync(string authToken, Company company);
        Task<ServiceResult<long>> AgentCreditAsync(string authToken, Company company);
        #endregion
        //-------------------------------------------
        #endregion
    }
}
