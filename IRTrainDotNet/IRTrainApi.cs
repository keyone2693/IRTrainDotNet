using IRTrainDotNet.Helpers;
using IRTrainDotNet.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace IRTrainDotNet
{
    public class IRTrainApi : IIRTrainApi
    {
        private readonly HttpClient _http;
        private string error;

        #region ctor
        private readonly CancellationTokenSource _cancellationTokenSource;
        public IRTrainApi()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            _http = new HttpClient();
            error = "";
        }
        #endregion

        #region Synchronous
        //-------------------------------------------
        #region Auth
        public ServiceResult<String> Login(LoginModel loginModel, Company company)
        {
            var result = new ServiceResult<string>();


            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            var response = _http.PostAsJsonAsync<LoginModel>(ApiUrl.Login, loginModel).Result;

            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<String>>( response.Content.ReadAsStringAsync().Result);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
                }
            }
            return result;
        }
        public bool ValidateTokenWithRequest(string token, Company company)
        {


            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + token);
            var response =  _http.GetAsync(ApiUrl.GetLastVersion).Result;

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return false;
            }
            else
            {
                return true;
            }
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
        #endregion
        #region Stations
        public ServiceResult<IEnumerable<Station>> GetStations(string authToken, Company company)
        {



            var result = new ServiceResult<IEnumerable<Station>>();
            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response =  _http.GetAsync(ApiUrl.Stations).Result;

            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<IEnumerable<Station>>>( response.Content.ReadAsStringAsync().Result);
                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }


            return result;
        }
        public ServiceResult<Station> GetStationById(string authToken, int stationId, Company company)
        {



            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response =  _http.GetAsync(ApiUrl.Station.ToUri(stationId)).Result;


            var result = new ServiceResult<Station>();

            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<Station>>( response.Content.ReadAsStringAsync().Result);
                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }
            return result;
        }
        #endregion
        #region Raja
        public ServiceResult<string> GetLastVersion(string authToken, Company company)
        {



            var result = new ServiceResult<string>();

            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response =  _http.GetAsync(ApiUrl.GetLastVersion).Result;

            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<string>>( response.Content.ReadAsStringAsync().Result);
                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }
            return result;
        }
        #endregion
        #region Wagon
        public ServiceResult<GetWagonAvailableSeatCountResult> GetWagonAvailableSeatCount(string authToken, GetWagonAvailableSeatCountParams getWagonAvailableSeatCountParams, Company company)
        {


            var result = new ServiceResult<GetWagonAvailableSeatCountResult>();


            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response =  _http.PostAsJsonAsync<GetWagonAvailableSeatCountParams>(ApiUrl.GetWagonAvailableSeatCount, getWagonAvailableSeatCountParams).Result;


            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<GetWagonAvailableSeatCountResult>>( response.Content.ReadAsStringAsync().Result);
                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }
            return result;
        }
        #endregion
        #region Seat
        public ServiceResult<LockSeatResult> LockSeat(string authToken, LockSeatParams lockSeatParams, Company company)
        {


            var result = new ServiceResult<LockSeatResult>();

            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response =  _http.PostAsJsonAsync<LockSeatParams>(ApiUrl.LockSeat, lockSeatParams).Result;

            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<LockSeatResult>>( response.Content.ReadAsStringAsync().Result);
                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }
            return result;
        }
        public ServiceResult<LockSeatBulkResult> LockSeatBulk(string authToken, LockSeatBulkParams lockSeatBulkParams, Company company)
        {



            var result = new ServiceResult<LockSeatBulkResult>();

            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response =  _http.PostAsJsonAsync<LockSeatBulkParams>(ApiUrl.LockSeatBulk, lockSeatBulkParams).Result;


            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<LockSeatBulkResult>>( response.Content.ReadAsStringAsync().Result);
                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }
            return result;
        }
        public ServiceResult<EmptyResult> UnlockSeat(string authToken, UnlockSeatParams unlockSeatParams, Company company)
        {


            var result = new ServiceResult<EmptyResult>();

            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response =  _http.PostAsJsonAsync<UnlockSeatParams>(ApiUrl.UnlockSeat, unlockSeatParams).Result;
            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<EmptyResult>>( response.Content.ReadAsStringAsync().Result);

                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }
            return result;
        }
        #endregion
        #region Ticket
        public ServiceResult<int> SaveTicketsInfo(string authToken, SaveTicketsInfoParams saveTicketsInfoParams, Company company)
        {
            var result = new ServiceResult<int>();

            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response =  _http.PostAsJsonAsync<SaveTicketsInfoParams>(ApiUrl.SaveTicketsInfo, saveTicketsInfoParams).Result;

            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<int>>( response.Content.ReadAsStringAsync().Result);

                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }
            return result;
        }
        public ServiceResult<EmptyResult> RegisterTickets(string authToken, RegisterTicketParams registerTicketParams, Company company)
        {
            var result = new ServiceResult<EmptyResult>();

            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response =  _http.PostAsJsonAsync<RegisterTicketParams>(ApiUrl.RegisterTickets, registerTicketParams).Result;

            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<EmptyResult>>( response.Content.ReadAsStringAsync().Result);

                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }
            return result;
        }
        public ServiceResult<TicketReportAResult> TicketReportA(string authToken, TicketReportAParams ticketReportAParams, Company company)
        {

            var result = new ServiceResult<TicketReportAResult>();


            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response =  _http.PostAsJsonAsync<TicketReportAParams>(ApiUrl.TicketReportA, ticketReportAParams).Result;


            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<TicketReportAResult>>( response.Content.ReadAsStringAsync().Result);

                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }
            return result;
        }
        public ServiceResult<RefundTicketInfoResult> RefundTicketInfo(string authToken, RefundTicketInfoParams refundTicketInfoParams, Company company)
        {

            var result = new ServiceResult<RefundTicketInfoResult>();

            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response =  _http.PostAsJsonAsync<RefundTicketInfoParams>(ApiUrl.RefundTicketInfo, refundTicketInfoParams).Result;

            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<RefundTicketInfoResult>>( response.Content.ReadAsStringAsync().Result);

                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }
            return result;
        }
        public ServiceResult<int> RefundTicket(string authToken, RefundTicketParams refundTicketParams, Company company)
        {

            var result = new ServiceResult<int>();

            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response =  _http.PostAsJsonAsync<RefundTicketParams>(ApiUrl.RefundTicket, refundTicketParams).Result;

            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<int>>( response.Content.ReadAsStringAsync().Result);

                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }
            return result;
        }

        #endregion
        #region Agent
        public ServiceResult<IEnumerable<UserSaleMetadata>> UserSales(string authToken, Company company)
        {

            var result = new ServiceResult<IEnumerable<UserSaleMetadata>>();

            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response =  _http.GetAsync(ApiUrl.UserSales).Result;

            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<IEnumerable<UserSaleMetadata>>>( response.Content.ReadAsStringAsync().Result);

                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }
            return result;
        }
        public ServiceResult<long> AgentCredit(string authToken, Company company)
        {


            var result = new ServiceResult<long>();

            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response =  _http.GetAsync(ApiUrl.AgentCredit).Result;

            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<long>>( response.Content.ReadAsStringAsync().Result);

                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
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
            var result = new ServiceResult<string>();


            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            var response = await _http.PostAsJsonAsync<LoginModel>(ApiUrl.Login, loginModel);

            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<String>>(await response.Content.ReadAsStringAsync());
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
                }
            }
            return result;
        }
        public async Task<bool> ValidateTokenWithRequestAsync(string token, Company company)
        {


            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + token);
            var response = await _http.GetAsync(ApiUrl.GetLastVersion);

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
        public async Task<ServiceResult<IEnumerable<Station>>> GetStationsAsync(string authToken, Company company)
        {



            var result = new ServiceResult<IEnumerable<Station>>();
            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response = await _http.GetAsync(ApiUrl.Stations);

            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<IEnumerable<Station>>>(await response.Content.ReadAsStringAsync());
                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }


            return result;
        }
        public async Task<ServiceResult<Station>> GetStationByIdAsync(string authToken, int stationId, Company company)
        {



            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response = await _http.GetAsync(ApiUrl.Station.ToUri(stationId));


            var result = new ServiceResult<Station>();

            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<Station>>(await response.Content.ReadAsStringAsync());
                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }
            return result;
        }
        #endregion
        #region Raja
        public async Task<ServiceResult<string>> GetLastVersionAsync(string authToken, Company company)
        {



            var result = new ServiceResult<string>();

            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response = await _http.GetAsync(ApiUrl.GetLastVersion);

            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<string>>(await response.Content.ReadAsStringAsync());
                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }
            return result;
        }
        #endregion
        #region Wagon
        public async Task<ServiceResult<GetWagonAvailableSeatCountResult>> GetWagonAvailableSeatCountAsync(string authToken, GetWagonAvailableSeatCountParams getWagonAvailableSeatCountParams, Company company)
        {


            var result = new ServiceResult<GetWagonAvailableSeatCountResult>();


            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response = await _http.PostAsJsonAsync<GetWagonAvailableSeatCountParams>(ApiUrl.GetWagonAvailableSeatCount, getWagonAvailableSeatCountParams);


            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<GetWagonAvailableSeatCountResult>>(await response.Content.ReadAsStringAsync());
                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }
            return result;
        }
        #endregion
        #region Seat
        public async Task<ServiceResult<LockSeatResult>> LockSeatAsync(string authToken, LockSeatParams lockSeatParams, Company company)
        {


            var result = new ServiceResult<LockSeatResult>();

            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response = await _http.PostAsJsonAsync<LockSeatParams>(ApiUrl.LockSeat, lockSeatParams);

            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<LockSeatResult>>(await response.Content.ReadAsStringAsync());
                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }
            return result;
        }
        public async Task<ServiceResult<LockSeatBulkResult>> LockSeatBulkAsync(string authToken, LockSeatBulkParams lockSeatBulkParams, Company company)
        {



            var result = new ServiceResult<LockSeatBulkResult>();

            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response = await _http.PostAsJsonAsync<LockSeatBulkParams>(ApiUrl.LockSeatBulk, lockSeatBulkParams);


            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<LockSeatBulkResult>>(await response.Content.ReadAsStringAsync());
                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }
            return result;
        }
        public async Task<ServiceResult<EmptyResult>> UnlockSeatAsync(string authToken, UnlockSeatParams unlockSeatParams, Company company)
        {


            var result = new ServiceResult<EmptyResult>();

            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response = await _http.PostAsJsonAsync<UnlockSeatParams>(ApiUrl.UnlockSeat, unlockSeatParams);
            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<EmptyResult>>(await response.Content.ReadAsStringAsync());

                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }
            return result;
        }
        #endregion
        #region Ticket
        public async Task<ServiceResult<int>> SaveTicketsInfoAsync(string authToken, SaveTicketsInfoParams saveTicketsInfoParams, Company company)
        {
            var result = new ServiceResult<int>();

            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response = await _http.PostAsJsonAsync<SaveTicketsInfoParams>(ApiUrl.SaveTicketsInfo, saveTicketsInfoParams);

            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<int>>(await response.Content.ReadAsStringAsync());

                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }
            return result;
        }
        public async Task<ServiceResult<EmptyResult>> RegisterTicketsAsync(string authToken, RegisterTicketParams registerTicketParams, Company company)
        {
            var result = new ServiceResult<EmptyResult>();

            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response = await _http.PostAsJsonAsync<RegisterTicketParams>(ApiUrl.RegisterTickets, registerTicketParams);

            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<EmptyResult>>(await response.Content.ReadAsStringAsync());

                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }
            return result;
        }
        public async Task<ServiceResult<TicketReportAResult>> TicketReportAAsync(string authToken, TicketReportAParams ticketReportAParams, Company company)
        {

            var result = new ServiceResult<TicketReportAResult>();


            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response = await _http.PostAsJsonAsync<TicketReportAParams>(ApiUrl.TicketReportA, ticketReportAParams);


            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<TicketReportAResult>>(await response.Content.ReadAsStringAsync());

                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }
            return result;
        }
        public async Task<ServiceResult<RefundTicketInfoResult>> RefundTicketInfoAsync(string authToken, RefundTicketInfoParams refundTicketInfoParams, Company company)
        {

            var result = new ServiceResult<RefundTicketInfoResult>();

            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response = await _http.PostAsJsonAsync<RefundTicketInfoParams>(ApiUrl.RefundTicketInfo, refundTicketInfoParams);

            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<RefundTicketInfoResult>>(await response.Content.ReadAsStringAsync());

                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }
            return result;
        }
        public async Task<ServiceResult<int>> RefundTicketAsync(string authToken, RefundTicketParams refundTicketParams, Company company)
        {

            var result = new ServiceResult<int>();

            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response = await _http.PostAsJsonAsync<RefundTicketParams>(ApiUrl.RefundTicket, refundTicketParams);

            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<int>>(await response.Content.ReadAsStringAsync());

                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }
            return result;
        }

        #endregion
        #region Agent
        public async Task<ServiceResult<IEnumerable<UserSaleMetadata>>> UserSalesAsync(string authToken, Company company)
        {

            var result = new ServiceResult<IEnumerable<UserSaleMetadata>>();

            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response = await _http.GetAsync(ApiUrl.UserSales);

            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<IEnumerable<UserSaleMetadata>>>(await response.Content.ReadAsStringAsync());

                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }
            return result;
        }
        public async Task<ServiceResult<long>> AgentCreditAsync(string authToken, Company company)
        {


            var result = new ServiceResult<long>();

            _http.BaseAddress = company.ToBaseUrl().ToUri();
            _http.DefaultRequestHeaders.Add("Content-Type", "application/json");
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            var response = await _http.GetAsync(ApiUrl.AgentCredit);

            if (response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<long>>(await response.Content.ReadAsStringAsync());

                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    result.Status = true;
                    result.Result = res.Result;
                }
                else
                {
                    result.Status = false;
                    result.Message = error;
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
                result.Message = error;
            }
            return result;
        }
        #endregion
        //-------------------------------------------
        #endregion
    }
}
