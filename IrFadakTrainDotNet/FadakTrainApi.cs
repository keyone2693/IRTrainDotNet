using IrFadakTrainDotNet.Helpers;
using IrFadakTrainDotNet.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IrFadakTrainDotNet
{
    public class FadakTrainApi : IFadakTrainApi
    {
        #region ctor
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly string authToken;
        public FadakTrainApi()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }
        #endregion

        #region Synchronous
        //-------------------------------------------
        #region Auth
        public ServiceResult<String> Login(LoginModel loginModel)
        {
            var _client = new RestClient(ApiUrl.Login.ToUri());
            var _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddJsonBody(loginModel);

            var result = new ServiceResult<string>();
            var response = _client.Execute(_request);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                var res = JsonConvert
                    .DeserializeObject<FadakResult<String>>(response.Content);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                }
            }

            return result;
        }
        public bool ValidateTokenWithTime(DateTime addDate)
        {
            if (addDate.AddMinutes(20) > DateTime.Now)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool ValidateTokenWithRequest(string token)
        {
            var _client = new RestClient(ApiUrl.GetLastVersion.ToUri());
            var _request = new RestRequest(Method.GET);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + token);
            var response = _client.Execute(_request);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion
        #region Stations
        public ServiceResult<List<Station>> GetStations(string authToken)
        {
            var _client = new RestClient(ApiUrl.Stations.ToUri());
            var _request = new RestRequest(Method.GET);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);

            var result = new ServiceResult<List<Station>>();

            var response = _client.Execute(_request);
            var res = JsonConvert.DeserializeObject<FadakResult<List<Station>>>(response.Content);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {

                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                }
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                result.Status = false;
                result.Unauthorized = true;
                result.Message = "مشکل در اعتبار سنجی دوباره لاگین کنید";
            }
            else
            {
                result.Status = false;
                result.Message = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
            }
            return result;
        }
        public ServiceResult<Station> GetStationById(string authToken, int stationId)
        {

            var _client = new RestClient(ApiUrl.Station.ToUri(stationId));
            var _request = new RestRequest(Method.GET);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);

            var result = new ServiceResult<Station>();

            var response = _client.Execute(_request);
            var res = JsonConvert.DeserializeObject<FadakResult<Station>>(response.Content);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {

                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                }
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                result.Status = false;
                result.Unauthorized = true;
                result.Message = "مشکل در اعتبار سنجی دوباره لاگین کنید";
            }
            else
            {
                result.Status = false;
                result.Message = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
            }
            return result;
        }
        #endregion
        #region Raja
        public ServiceResult<string> GetLastVersion(string authToken)
        {
            var _client = new RestClient(ApiUrl.GetLastVersion.ToUri());
            var _request = new RestRequest(Method.GET);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);

            var result = new ServiceResult<string>();

            var response = _client.Execute(_request);
            var res = JsonConvert.DeserializeObject<FadakResult<string>>(response.Content);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                }
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                result.Status = false;
                result.Unauthorized = true;
                result.Message = "مشکل در اعتبار سنجی دوباره لاگین کنید";
            }
            else
            {
                result.Status = false;
                result.Message = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
            }
            return result;
        }
        #endregion
        #region Wagon
        public ServiceResult<GetWagonAvailableSeatCountResult> GetWagonAvailableSeatCount(string authToken, GetWagonAvailableSeatCountParams getWagonAvailableSeatCountParams)
        {
            var _client = new RestClient(ApiUrl.GetWagonAvailableSeatCount.ToUri());
            var _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);
            _request.AddJsonBody(getWagonAvailableSeatCountParams);
            var result = new ServiceResult<GetWagonAvailableSeatCountResult>();
            var response = _client.Execute(_request);
            var res = JsonConvert.DeserializeObject<FadakResult<GetWagonAvailableSeatCountResult>>(response.Content);
            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                }
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                result.Status = false;
                result.Unauthorized = true;
                result.Message = "مشکل در اعتبار سنجی دوباره لاگین کنید";
            }
            else
            {
                result.Status = false;
                result.Message = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
            }
            return result;
        }
        #endregion
        //-------------------------------------------
        #endregion

        #region Asynchronous 
        //-------------------------------------------
        #region Auth
        public async Task<ServiceResult<String>> LoginAsync(LoginModel loginModel)
        {
            var _client = new RestClient(ApiUrl.Login.ToUri());
            var _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddJsonBody(loginModel);

            var result = new ServiceResult<string>();
            var cancellationTokenSource = new CancellationTokenSource();
            var response = await _client.ExecuteTaskAsync(_request, cancellationTokenSource.Token);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                var res = JsonConvert
                    .DeserializeObject<FadakResult<String>>(response.Content);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                }
            }

            return result;
        }
        public async Task<bool> ValidateTokenWithRequestAsync(string token)
        {
            var _client = new RestClient(ApiUrl.GetLastVersion.ToUri());
            var _request = new RestRequest(Method.GET);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + token);
            var response = await _client.ExecuteTaskAsync(_request);
            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return false;
            }
            else {
                return true;
            }
        }
        #endregion
        #region Stations
        public async Task<ServiceResult<List<Station>>> GetStationsAsync(string authToken)
        {
            var _client = new RestClient(ApiUrl.Stations.ToUri());
            var _request = new RestRequest(Method.GET);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);

            var result = new ServiceResult<List<Station>>();

            var response = await _client.ExecuteTaskAsync(_request, _cancellationTokenSource.Token);

            var res = JsonConvert.DeserializeObject<FadakResult<List<Station>>>(response.Content);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {

                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                }
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                result.Status = false;
                result.Unauthorized = true;
                result.Message = "مشکل در اعتبار سنجی دوباره لاگین کنید";
            }
            else
            {
                result.Status = false;
                result.Message = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
            }

            return result;
        }
        public async Task<ServiceResult<Station>> GetStationByIdAsync(string authToken, int stationId)
        {
            var _client = new RestClient(ApiUrl.Station.ToUri(stationId));
            var _request = new RestRequest(Method.GET);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);

            var result = new ServiceResult<Station>();

            var response = await _client.ExecuteTaskAsync(_request, _cancellationTokenSource.Token);
            var res = JsonConvert.DeserializeObject<FadakResult<Station>>(response.Content);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {

                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                }
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                result.Status = false;
                result.Unauthorized = true;
                result.Message = "مشکل در اعتبار سنجی دوباره لاگین کنید";
            }
            else
            {
                result.Status = false;
                result.Message = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
            }
            return result;
        }
        #endregion
        #region Raja
        public async Task<ServiceResult<string>> GetLastVersionAsync(string authToken)
        {
            var _client = new RestClient(ApiUrl.GetLastVersion.ToUri());
            var _request = new RestRequest(Method.GET);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);

            var result = new ServiceResult<string>();

            var response = await _client.ExecuteTaskAsync(_request, _cancellationTokenSource.Token);
            var res = JsonConvert.DeserializeObject<FadakResult<string>>(response.Content);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {

                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                }
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                result.Status = false;
                result.Unauthorized = true;
                result.Message = "مشکل در اعتبار سنجی دوباره لاگین کنید";
            }
            else
            {
                result.Status = false;
                result.Message = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
            }
            return result;
        }
        #endregion
        #region Wagon
        public async Task<ServiceResult<GetWagonAvailableSeatCountResult>> GetWagonAvailableSeatCountAsync(string authToken, GetWagonAvailableSeatCountParams getWagonAvailableSeatCountParams)
        {
            var _client = new RestClient(ApiUrl.GetWagonAvailableSeatCount.ToUri());
            var _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);
            _request.AddJsonBody(getWagonAvailableSeatCountParams);
            var result = new ServiceResult<GetWagonAvailableSeatCountResult>();

            var response = await _client.ExecuteTaskAsync(_request, _cancellationTokenSource.Token);
            var res = JsonConvert.DeserializeObject<FadakResult<GetWagonAvailableSeatCountResult>>(response.Content);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                }
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                result.Status = false;
                result.Unauthorized = true;
                result.Message = "مشکل در اعتبار سنجی دوباره لاگین کنید";
            }
            else
            {
                result.Status = false;
                result.Message = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
            }
            return result;
        }
        #endregion
        //-------------------------------------------
        #endregion
    }
}
