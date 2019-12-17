using IRTrainDotNet.Helpers;
using IRTrainDotNet.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace IRTrainDotNet
{
    public class IRTrainApi : IIRTrainApi
    {
        #region ctor
        private readonly CancellationTokenSource _cancellationTokenSource;
        public IRTrainApi()
        {
            _cancellationTokenSource = new CancellationTokenSource();
        }
        #endregion

        #region Synchronous
        //-------------------------------------------
        #region Auth
        public ServiceResult<string> Login(LoginModel loginModel, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;
            var _client = new RestClient(path + ApiUrl.Login);
            var _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddJsonBody(loginModel);

            var result = new ServiceResult<string>();
            var response = _client.Execute(_request);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                var res = JsonConvert
                    .DeserializeObject<IrTrainResult<String>>(response.Content);
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
        public bool ValidateTokenWithRequest(string token, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.GetLastVersion);
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
        public ServiceResult<List<Station>> GetStations(string authToken, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.Stations);
            var _request = new RestRequest(Method.GET);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);

            var result = new ServiceResult<List<Station>>();

            var response = _client.Execute(_request);
            var res = JsonConvert.DeserializeObject<IrTrainResult<List<Station>>>(response.Content);

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
        public ServiceResult<Station> GetStationById(string authToken, int stationId, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;


            var _client = new RestClient(path + ApiUrl.Station.ToUri(stationId));
            var _request = new RestRequest(Method.GET);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);

            var result = new ServiceResult<Station>();

            var response = _client.Execute(_request);
            var res = JsonConvert.DeserializeObject<IrTrainResult<Station>>(response.Content);

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
        public ServiceResult<string> GetLastVersion(string authToken, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.GetLastVersion);
            var _request = new RestRequest(Method.GET);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);

            var result = new ServiceResult<string>();

            var response = _client.Execute(_request);
            var res = JsonConvert.DeserializeObject<IrTrainResult<string>>(response.Content);

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
        public ServiceResult<GetWagonAvailableSeatCountResult> GetWagonAvailableSeatCount(string authToken, GetWagonAvailableSeatCountParams getWagonAvailableSeatCountParams, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.GetWagonAvailableSeatCount);
            var _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);
            _request.AddJsonBody(getWagonAvailableSeatCountParams);
            var result = new ServiceResult<GetWagonAvailableSeatCountResult>();
            var response = _client.Execute(_request);
            var res = JsonConvert.DeserializeObject<IrTrainResult<GetWagonAvailableSeatCountResult>>(response.Content);
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
        #region Seat
        public ServiceResult<LockSeatResult> LockSeat(string authToken, LockSeatParams lockSeatParams, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.LockSeat);
            var _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);
            _request.AddJsonBody(lockSeatParams);
            var result = new ServiceResult<LockSeatResult>();

            var response = _client.Execute(_request);
            var res = JsonConvert.DeserializeObject<IrTrainResult<LockSeatResult>>(response.Content);

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
        public ServiceResult<LockSeatBulkResult> LockSeatBulk(string authToken, LockSeatBulkParams lockSeatBulkParams, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.LockSeatBulk);
            var _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);
            _request.AddJsonBody(lockSeatBulkParams);
            var result = new ServiceResult<LockSeatBulkResult>();

            var response = _client.Execute(_request);
            var res = JsonConvert.DeserializeObject<IrTrainResult<LockSeatBulkResult>>(response.Content);

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
        public ServiceResult<EmptyResult> UnlockSeat(string authToken, UnlockSeatParams unlockSeatParams, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.UnlockSeat);
            var _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);
            _request.AddJsonBody(unlockSeatParams);
            var result = new ServiceResult<EmptyResult>();

            var response = _client.Execute(_request);
            var res = JsonConvert.DeserializeObject<IrTrainResult<EmptyResult>>(response.Content);

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
        #region Ticket
        public ServiceResult<int> SaveTicketsInfo(string authToken, SaveTicketsInfoParams saveTicketsInfoParams, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.SaveTicketsInfo);
            var _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);
            _request.AddJsonBody(saveTicketsInfoParams);
            var result = new ServiceResult<int>();

            var response = _client.Execute(_request);
            var res = JsonConvert.DeserializeObject<IrTrainResult<int>>(response.Content);

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
        public ServiceResult<EmptyResult> RegisterTickets(string authToken, RegisterTicketParams registerTicketParams, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.RegisterTickets);
            var _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);
            _request.AddJsonBody(registerTicketParams);
            var result = new ServiceResult<EmptyResult>();

            var response =  _client.Execute(_request);
            var res = JsonConvert.DeserializeObject<IrTrainResult<EmptyResult>>(response.Content);

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
        public ServiceResult<TicketReportAResult> TicketReportA(string authToken, TicketReportAParams ticketReportAParams, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.TicketReportA);
            var _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);
            _request.AddJsonBody(ticketReportAParams);
            var result = new ServiceResult<TicketReportAResult>();

            var response =  _client.Execute(_request);
            var res = JsonConvert.DeserializeObject<IrTrainResult<TicketReportAResult>>(response.Content);

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
        public ServiceResult<RefundTicketInfoResult> RefundTicketInfo(string authToken, RefundTicketInfoParams refundTicketInfoParams, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.RefundTicketInfo);
            var _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);
            _request.AddJsonBody(refundTicketInfoParams);
            var result = new ServiceResult<RefundTicketInfoResult>();

            var response = _client.Execute(_request);
            var res = JsonConvert.DeserializeObject<IrTrainResult<RefundTicketInfoResult>>(response.Content);

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
        public ServiceResult<int> RefundTicket(string authToken, RefundTicketParams refundTicketParams, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.RefundTicket);
            var _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);
            _request.AddJsonBody(refundTicketParams);
            var result = new ServiceResult<int>();

            var response = _client.Execute(_request);
            var res = JsonConvert.DeserializeObject<IrTrainResult<int>>(response.Content);

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
        #region Agent
        public ServiceResult<List<UserSaleMetadata>> UserSales(string authToken, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.UserSales);
            var _request = new RestRequest(Method.GET);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);

            var result = new ServiceResult<List<UserSaleMetadata>>();

            var response = _client.Execute(_request);
            var res = JsonConvert.DeserializeObject<IrTrainResult<List<UserSaleMetadata>>>(response.Content);

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
        public ServiceResult<long> AgentCredit(string authToken, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.AgentCredit);
            var _request = new RestRequest(Method.GET);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);

            var result = new ServiceResult<long>();

            var response = _client.Execute(_request);
            var res = JsonConvert.DeserializeObject<IrTrainResult<long>>(response.Content);

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
        public async Task<ServiceResult<String>> LoginAsync(LoginModel loginModel, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient((path + ApiUrl.Login).ToUri());
            var _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddJsonBody(loginModel);

            var result = new ServiceResult<string>();
            var cancellationTokenSource = new CancellationTokenSource();
            var response = await _client.ExecuteTaskAsync(_request, cancellationTokenSource.Token);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                var res = JsonConvert
                    .DeserializeObject<IrTrainResult<String>>(response.Content);
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
        public async Task<bool> ValidateTokenWithRequestAsync(string token, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.GetLastVersion);
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
        public async Task<ServiceResult<List<Station>>> GetStationsAsync(string authToken, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.Stations);
            var _request = new RestRequest(Method.GET);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);

            var result = new ServiceResult<List<Station>>();

            var response = await _client.ExecuteTaskAsync(_request, _cancellationTokenSource.Token);

            var res = JsonConvert.DeserializeObject<IrTrainResult<List<Station>>>(response.Content);

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
        public async Task<ServiceResult<Station>> GetStationByIdAsync(string authToken, int stationId, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.Station.ToUri(stationId));
            var _request = new RestRequest(Method.GET);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);

            var result = new ServiceResult<Station>();

            var response = await _client.ExecuteTaskAsync(_request, _cancellationTokenSource.Token);
            var res = JsonConvert.DeserializeObject<IrTrainResult<Station>>(response.Content);

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
        public async Task<ServiceResult<string>> GetLastVersionAsync(string authToken, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.GetLastVersion);
            var _request = new RestRequest(Method.GET);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);

            var result = new ServiceResult<string>();

            var response = await _client.ExecuteTaskAsync(_request, _cancellationTokenSource.Token);
            var res = JsonConvert.DeserializeObject<IrTrainResult<string>>(response.Content);

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
        public async Task<ServiceResult<GetWagonAvailableSeatCountResult>> GetWagonAvailableSeatCountAsync(string authToken, GetWagonAvailableSeatCountParams getWagonAvailableSeatCountParams, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.GetWagonAvailableSeatCount);
            var _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);
            _request.AddJsonBody(getWagonAvailableSeatCountParams);
            var result = new ServiceResult<GetWagonAvailableSeatCountResult>();

            var response = await _client.ExecuteTaskAsync(_request, _cancellationTokenSource.Token);
            var res = JsonConvert.DeserializeObject<IrTrainResult<GetWagonAvailableSeatCountResult>>(response.Content);

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
        #region Seat
        public async Task<ServiceResult<LockSeatResult>> LockSeatAsync(string authToken, LockSeatParams lockSeatParams, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.LockSeat);
            var _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);
            _request.AddJsonBody(lockSeatParams);
            var result = new ServiceResult<LockSeatResult>();

            var response = await _client.ExecuteTaskAsync(_request, _cancellationTokenSource.Token);
            var res = JsonConvert.DeserializeObject<IrTrainResult<LockSeatResult>>(response.Content);

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
        public async Task<ServiceResult<LockSeatBulkResult>> LockSeatBulkAsync(string authToken, LockSeatBulkParams lockSeatBulkParams, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.LockSeatBulk);
            var _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);
            _request.AddJsonBody(lockSeatBulkParams);
            var result = new ServiceResult<LockSeatBulkResult>();

            var response = await _client.ExecuteTaskAsync(_request, _cancellationTokenSource.Token);
            var res = JsonConvert.DeserializeObject<IrTrainResult<LockSeatBulkResult>>(response.Content);

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
        public async Task<ServiceResult<EmptyResult>> UnlockSeatAsync(string authToken, UnlockSeatParams unlockSeatParams, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.UnlockSeat);
            var _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);
            _request.AddJsonBody(unlockSeatParams);
            var result = new ServiceResult<EmptyResult>();

            var response = await _client.ExecuteTaskAsync(_request, _cancellationTokenSource.Token);
            var res = JsonConvert.DeserializeObject<IrTrainResult<EmptyResult>>(response.Content);

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
        #region Ticket
        public async Task<ServiceResult<int>> SaveTicketsInfoAsync(string authToken, SaveTicketsInfoParams saveTicketsInfoParams, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.SaveTicketsInfo);
            var _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);
            _request.AddJsonBody(saveTicketsInfoParams);
            var result = new ServiceResult<int>();

            var response = await _client.ExecuteTaskAsync(_request, _cancellationTokenSource.Token);
            var res = JsonConvert.DeserializeObject<IrTrainResult<int>>(response.Content);

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
        public async Task<ServiceResult<EmptyResult>> RegisterTicketsAsync(string authToken, RegisterTicketParams registerTicketParams, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.RegisterTickets);
            var _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);
            _request.AddJsonBody(registerTicketParams);
            var result = new ServiceResult<EmptyResult>();

            var response = await _client.ExecuteTaskAsync(_request, _cancellationTokenSource.Token);
            var res = JsonConvert.DeserializeObject<IrTrainResult<EmptyResult>>(response.Content);

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
        public async Task<ServiceResult<TicketReportAResult>> TicketReportAAsync(string authToken, TicketReportAParams ticketReportAParams, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.TicketReportA);
            var _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);
            _request.AddJsonBody(ticketReportAParams);
            var result = new ServiceResult<TicketReportAResult>();

            var response = await _client.ExecuteTaskAsync(_request, _cancellationTokenSource.Token);
            var res = JsonConvert.DeserializeObject<IrTrainResult<TicketReportAResult>>(response.Content);

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
        public async Task<ServiceResult<RefundTicketInfoResult>> RefundTicketInfoAsync(string authToken, RefundTicketInfoParams refundTicketInfoParams, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.RefundTicketInfo);
            var _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);
            _request.AddJsonBody(refundTicketInfoParams);
            var result = new ServiceResult<RefundTicketInfoResult>();

            var response = await _client.ExecuteTaskAsync(_request, _cancellationTokenSource.Token);
            var res = JsonConvert.DeserializeObject<IrTrainResult<RefundTicketInfoResult>>(response.Content);

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
        public async Task<ServiceResult<int>> RefundTicketAsync(string authToken, RefundTicketParams refundTicketParams, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.RefundTicket);
            var _request = new RestRequest(Method.POST);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);
            _request.AddJsonBody(refundTicketParams);
            var result = new ServiceResult<int>();

            var response = await _client.ExecuteTaskAsync(_request, _cancellationTokenSource.Token);
            var res = JsonConvert.DeserializeObject<IrTrainResult<int>>(response.Content);

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
        #region Agent
        public async Task<ServiceResult<List<UserSaleMetadata>>> UserSalesAsync(string authToken, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.UserSales);
            var _request = new RestRequest(Method.GET);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);

            var result = new ServiceResult<List<UserSaleMetadata>>();

            var response = await _client.ExecuteTaskAsync(_request, _cancellationTokenSource.Token);
            var res = JsonConvert.DeserializeObject<IrTrainResult<List<UserSaleMetadata>>>(response.Content);

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
        public async Task<ServiceResult<long>> AgentCreditAsync(string authToken, Company company)
        {
            var path = company == Company.Raja ? ApiUrl.RajaBaseUrl : company == Company.Fadak ? ApiUrl.FadakBaseUrl : ApiUrl.SafirBaseUrl;

            var _client = new RestClient(path + ApiUrl.AgentCredit);
            var _request = new RestRequest(Method.GET);
            _request.AddHeader("Content-Type", "application/json");
            _request.AddHeader("Authorization", Constants.PreToken + authToken);

            var result = new ServiceResult<long>();

            var response = await _client.ExecuteTaskAsync(_request, _cancellationTokenSource.Token);
            var res = JsonConvert.DeserializeObject<IrTrainResult<long>>(response.Content);

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
