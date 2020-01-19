using IRTrainDotNet.Helpers;
using IRTrainDotNet.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IRTrainDotNet
{
    public class IRTrainApi : IIRTrainApi
    {
        private readonly HttpClient _http;
        private StringContent _content;
        private HttpResponseMessage _response;

        private string error = "";

        #region ctor
        public IRTrainApi()
        {
            _http = new HttpClient();
        }
        #endregion

        #region Synchronous
        //-------------------------------------------
        #region Auth
        public ServiceResult<String> Login(LoginModel loginModel, Company company)
        {
            var result = new ServiceResult<string>();
            _http.DefaultRequestHeaders.Clear();
            _content = new StringContent(
                       JsonConvert.SerializeObject(loginModel), UTF8Encoding.UTF8, "application/json");

            _response = _http.PostAsync(company.ToBaseUrl(ApiUrl.Login), _content).Result;

            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<String>>(_response.Content.ReadAsStringAsync().Result);
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
        public bool ValidateTokenWithRequest(string authtoken, Company company)
        {


            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authtoken);
            _response = _http.GetAsync(company.ToBaseUrl(ApiUrl.GetLastVersion)).Result;

            if (_response.StatusCode == HttpStatusCode.Unauthorized)
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
            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _response = _http.GetAsync(company.ToBaseUrl(ApiUrl.Stations)).Result;

            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<IEnumerable<Station>>>(_response.Content.ReadAsStringAsync().Result);
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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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



            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _response = _http.GetAsync(company.ToBaseUrl(ApiUrl.Station + stationId)).Result;



            var result = new ServiceResult<Station>();

            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<Station>>(_response.Content.ReadAsStringAsync().Result);
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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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

            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _response = _http.GetAsync(company.ToBaseUrl(ApiUrl.GetLastVersion)).Result;

            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<string>>(_response.Content.ReadAsStringAsync().Result);
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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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


            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _content = new StringContent(
                       JsonConvert.SerializeObject(getWagonAvailableSeatCountParams), UTF8Encoding.UTF8, "application/json");
            _response = _http.PostAsync(company.ToBaseUrl(ApiUrl.GetWagonAvailableSeatCount), _content).Result;


            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<GetWagonAvailableSeatCountResult>>(_response.Content.ReadAsStringAsync().Result);
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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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

            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _content = new StringContent(
                       JsonConvert.SerializeObject(lockSeatParams), UTF8Encoding.UTF8, "application/json");
            _response = _http.PostAsync(company.ToBaseUrl(ApiUrl.LockSeat), _content).Result;

            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<LockSeatResult>>(_response.Content.ReadAsStringAsync().Result);
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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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

            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _content = new StringContent(
                       JsonConvert.SerializeObject(lockSeatBulkParams), UTF8Encoding.UTF8, "application/json");
            _response = _http.PostAsync(company.ToBaseUrl(ApiUrl.LockSeatBulk), _content).Result;


            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<LockSeatBulkResult>>(_response.Content.ReadAsStringAsync().Result);
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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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

            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _content = new StringContent(
                       JsonConvert.SerializeObject(unlockSeatParams), UTF8Encoding.UTF8, "application/json");
            _response = _http.PostAsync(company.ToBaseUrl(ApiUrl.UnlockSeat), _content).Result;
            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<EmptyResult>>(_response.Content.ReadAsStringAsync().Result);

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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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

            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _content = new StringContent(
                       JsonConvert.SerializeObject(saveTicketsInfoParams), UTF8Encoding.UTF8, "application/json");
            _response = _http.PostAsync(company.ToBaseUrl(ApiUrl.SaveTicketsInfo), _content).Result;

            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<int>>(_response.Content.ReadAsStringAsync().Result);

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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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

            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _content = new StringContent(
                       JsonConvert.SerializeObject(registerTicketParams), UTF8Encoding.UTF8, "application/json");
            _response = _http.PostAsync(company.ToBaseUrl(ApiUrl.RegisterTickets), _content).Result;

            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<EmptyResult>>(_response.Content.ReadAsStringAsync().Result);

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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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


            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _content = new StringContent(
                       JsonConvert.SerializeObject(ticketReportAParams), UTF8Encoding.UTF8, "application/json");
            _response = _http.PostAsync(company.ToBaseUrl(ApiUrl.TicketReportA), _content).Result;


            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<TicketReportAResult>>(_response.Content.ReadAsStringAsync().Result);

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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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

            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _content = new StringContent(
                       JsonConvert.SerializeObject(refundTicketInfoParams), UTF8Encoding.UTF8, "application/json");
            _response = _http.PostAsync(company.ToBaseUrl(ApiUrl.RefundTicketInfo), _content).Result;

            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<RefundTicketInfoResult>>(_response.Content.ReadAsStringAsync().Result);

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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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

            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _content = new StringContent(
                       JsonConvert.SerializeObject(refundTicketParams), UTF8Encoding.UTF8, "application/json");
            _response = _http.PostAsync(company.ToBaseUrl(ApiUrl.RefundTicket), _content).Result;

            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<int>>(_response.Content.ReadAsStringAsync().Result);

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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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

            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);

            _response = _http.GetAsync(company.ToBaseUrl(ApiUrl.UserSales)).Result;

            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<IEnumerable<UserSaleMetadata>>>(_response.Content.ReadAsStringAsync().Result);

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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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

            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _response = _http.GetAsync(company.ToBaseUrl(ApiUrl.AgentCredit)).Result;

            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<long>>(_response.Content.ReadAsStringAsync().Result);

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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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
            _http.DefaultRequestHeaders.Clear();

            _content = new StringContent(
                       JsonConvert.SerializeObject(loginModel), UTF8Encoding.UTF8, "application/json");

            _response = await _http.PostAsync(company.ToBaseUrl(ApiUrl.Login), _content);
            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<String>>(await _response.Content.ReadAsStringAsync());
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

            return result;
        }
        public async Task<bool> ValidateTokenWithRequestAsync(string authtoken, Company company)
        {


            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authtoken);
            _response = await _http.GetAsync(company.ToBaseUrl(ApiUrl.GetLastVersion));

            if (_response.StatusCode == HttpStatusCode.Unauthorized)
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
            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _response = await _http.GetAsync(company.ToBaseUrl(ApiUrl.Stations));

            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<IEnumerable<Station>>>(await _response.Content.ReadAsStringAsync());
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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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



            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _response = await _http.GetAsync(company.ToBaseUrl(ApiUrl.Station + stationId));



            var result = new ServiceResult<Station>();

            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<Station>>(await _response.Content.ReadAsStringAsync());
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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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

            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _response = await _http.GetAsync(company.ToBaseUrl(ApiUrl.GetLastVersion));

            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<string>>(await _response.Content.ReadAsStringAsync());
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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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


            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _content = new StringContent(
                       JsonConvert.SerializeObject(getWagonAvailableSeatCountParams), UTF8Encoding.UTF8, "application/json");
            _response = await _http.PostAsync(company.ToBaseUrl(ApiUrl.GetWagonAvailableSeatCount), _content);


            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<GetWagonAvailableSeatCountResult>>(await _response.Content.ReadAsStringAsync());
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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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

            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _content = new StringContent(
                       JsonConvert.SerializeObject(lockSeatParams), UTF8Encoding.UTF8, "application/json");
            _response = await _http.PostAsync(company.ToBaseUrl(ApiUrl.LockSeat), _content);

            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<LockSeatResult>>(await _response.Content.ReadAsStringAsync());
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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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

            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _content = new StringContent(
                       JsonConvert.SerializeObject(lockSeatBulkParams), UTF8Encoding.UTF8, "application/json");

            _response = await _http.PostAsync(company.ToBaseUrl(ApiUrl.LockSeatBulk), _content);


            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<LockSeatBulkResult>>(await _response.Content.ReadAsStringAsync());
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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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

            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _content = new StringContent(
                       JsonConvert.SerializeObject(unlockSeatParams), UTF8Encoding.UTF8, "application/json");

            _response = await _http.PostAsync(company.ToBaseUrl(ApiUrl.UnlockSeat), _content);
            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<EmptyResult>>(await _response.Content.ReadAsStringAsync());

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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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

            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _content = new StringContent(
                       JsonConvert.SerializeObject(saveTicketsInfoParams), UTF8Encoding.UTF8, "application/json");

            _response = await _http.PostAsync(company.ToBaseUrl(ApiUrl.SaveTicketsInfo), _content);

            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<int>>(await _response.Content.ReadAsStringAsync());

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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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

            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _content = new StringContent(
                       JsonConvert.SerializeObject(registerTicketParams), UTF8Encoding.UTF8, "application/json");

            _response = await _http.PostAsync(company.ToBaseUrl(ApiUrl.RegisterTickets), _content);

            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<EmptyResult>>(await _response.Content.ReadAsStringAsync());

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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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


            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _content = new StringContent(
                       JsonConvert.SerializeObject(ticketReportAParams), UTF8Encoding.UTF8, "application/json");

            _response = await _http.PostAsync(company.ToBaseUrl(ApiUrl.TicketReportA), _content);


            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<TicketReportAResult>>(await _response.Content.ReadAsStringAsync());

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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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

            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _content = new StringContent(
                       JsonConvert.SerializeObject(refundTicketInfoParams), UTF8Encoding.UTF8, "application/json");
            _response = await _http.PostAsync(company.ToBaseUrl(ApiUrl.RefundTicketInfo), _content);

            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<RefundTicketInfoResult>>(await _response.Content.ReadAsStringAsync());

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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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

            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _content = new StringContent(
                       JsonConvert.SerializeObject(refundTicketParams), UTF8Encoding.UTF8, "application/json");
            _response = await _http.PostAsync(company.ToBaseUrl(ApiUrl.RefundTicket), _content);

            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<int>>(await _response.Content.ReadAsStringAsync());

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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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

            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _response = await _http.GetAsync(company.ToBaseUrl(ApiUrl.UserSales));

            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<IEnumerable<UserSaleMetadata>>>(await _response.Content.ReadAsStringAsync());

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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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

            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _response = await _http.GetAsync(company.ToBaseUrl(ApiUrl.AgentCredit));

            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<long>>(await _response.Content.ReadAsStringAsync());

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
            else if (_response.StatusCode == HttpStatusCode.Unauthorized)
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


        #region dispose
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _http.Dispose();
                    _content.Dispose();
                    _response.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~IRTrainApi()
        {
            Dispose(true);
        }
        #endregion
    }
}
