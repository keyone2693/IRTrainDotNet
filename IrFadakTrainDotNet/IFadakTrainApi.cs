using IrFadakTrainDotNet.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IrFadakTrainDotNet
{
    public interface IFadakTrainApi
    {
        #region Synchronous
        #region Auth
            ServiceResult<String> Login(LoginModel loginModel);
        bool ValidateTokenWithTime(DateTime addDate);
        bool ValidateTokenWithRequest(string token);
        #endregion
        #region Stations
        ServiceResult<List<Station>> GetStations(string authToken);
            ServiceResult<Station> GetStationById(string authToken,int stationId);
        #endregion
        #region Raja
        ServiceResult<string> GetLastVersion(string authToken);
        #endregion
        #region Wagon
        ServiceResult<GetWagonAvailableSeatCountResult> GetWagonAvailableSeatCount(string authToken, GetWagonAvailableSeatCountParams getWagonAvailableSeatCountParams);
        #endregion
        #endregion

        #region Asynchronous 
        #region Auth
        Task<ServiceResult<String>> LoginAsync(LoginModel loginModel);
        Task<bool> ValidateTokenWithRequestAsync(string token);
        #endregion
        #region Stations
        Task<ServiceResult<List<Station>>> GetStationsAsync(string authToken);
            Task<ServiceResult<Station>> GetStationByIdAsync( string authToken,int stationId);
        #endregion
        #region Raja
        Task<ServiceResult<string>> GetLastVersionAsync(string authToken);
        #endregion
        #region Wagon
        Task<ServiceResult<GetWagonAvailableSeatCountResult>> GetWagonAvailableSeatCountAsync(string authToken, GetWagonAvailableSeatCountParams getWagonAvailableSeatCountParams);
        #endregion
        #endregion
    }
}
