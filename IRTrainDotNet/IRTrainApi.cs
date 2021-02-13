using IRTrainDotNet.Helpers;
using IRTrainDotNet.Models;
using IRTrainDotNet.Models.StationTrainInfo;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IRTrainDotNet
{
    public class IRTrainApi : IIRTrainApi, IDisposable
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

        #region Asynchronous 
        //-------------------------------------------
        #region Auth
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginModel"></param>
        /// <param name="company"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authtoken"></param>
        /// <param name="company"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="company"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="stationId"></param>
        /// <param name="company"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="company"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="getWagonAvailableSeatCountParams"></param>
        /// <param name="company"></param>
        /// <returns></returns>
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

                    res.Result.GoingResults.OrEmpty().ToList().ForEach(p =>
                    {
                        p.MoveDate = new DateTime(p.MoveDate.Year, p.MoveDate.Month, p.MoveDate.Day,
                           Convert.ToInt32(p.ExitTime.Split(':')[0]),
                            Convert.ToInt32(p.ExitTime.Split(':')[1]),
                           Convert.ToInt32(p.ExitTime.Split(':')[2])).ToUniversalTime();
                    });
                    res.Result.ReturnResults.OrEmpty().ToList().ForEach(p =>
                    {
                        p.MoveDate = new DateTime(p.MoveDate.Year, p.MoveDate.Month, p.MoveDate.Day,
                           Convert.ToInt32(p.ExitTime.Split(':')[0]),
                            Convert.ToInt32(p.ExitTime.Split(':')[1]),
                           Convert.ToInt32(p.ExitTime.Split(':')[2])).ToUniversalTime();
                    });
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="lockSeatParams"></param>
        /// <param name="company"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="lockSeatBulkParams"></param>
        /// <param name="company"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="unlockSeatParams"></param>
        /// <param name="company"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="saveTicketsInfoParams"></param>
        /// <param name="company"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="registerTicketParams"></param>
        /// <param name="company"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="ticketReportAParams"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<TicketReportAResult>>> TicketReportAAsync(string authToken, TicketReportAParams ticketReportAParams, Company company)
        {

            var result = new ServiceResult<IEnumerable<TicketReportAResult>>();


            _http.DefaultRequestHeaders.Clear();

            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);
            _content = new StringContent(
                       JsonConvert.SerializeObject(ticketReportAParams), UTF8Encoding.UTF8, "application/json");

            _response = await _http.PostAsync(company.ToBaseUrl(ApiUrl.TicketReportA), _content);


            if (_response.IsSuccessStatusCode)
            {
                var res = JsonConvert.DeserializeObject<IrTrainResult<IEnumerable<TicketReportAResult>>>(await _response.Content.ReadAsStringAsync());

                error = res.ExceptionId.GetSystemErrorMessage(res.ExceptionMessage);
                if (res.ExceptionId == 0 && res.ExceptionMessage == null)
                {
                    res.Result.OrEmpty().ToList().ForEach(p =>
                    {
                        p.MoveDate = new DateTime(p.MoveDate.Year, p.MoveDate.Month, p.MoveDate.Day,
                           Convert.ToInt32(p.MoveTime.Split(':')[0]),
                            Convert.ToInt32(p.MoveTime.Split(':')[1]),
                           Convert.ToInt32(p.MoveTime.Split(':')[2])).ToUniversalTime();
                        p.RegisterDate = p.RegisterDate.ToUniversalTime();
                    });
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="refundTicketInfoParams"></param>
        /// <param name="company"></param>
        /// <returns></returns>
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
                    res.Result.MoveDateTime = res.Result.MoveDateTime.ToUniversalTime();
                    res.Result.MoveDateTrain = res.Result.MoveDateTrain.ToUniversalTime();
                    res.Result.RegisteredAt = res.Result.RegisteredAt.ToUniversalTime();
                    //
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="refundTicketParams"></param>
        /// <param name="company"></param>
        /// <returns></returns>
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
        #region StationTrainInfo
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="getStationTimeLineParams"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        public async Task<ServiceResult<IEnumerable<StationTimeLine>>> GetStationTimeLineAsync(string authToken, GetStationTimeLineParams getStationTimeLineParams, Company company)
        {
            var result = new ServiceResult<IEnumerable<StationTimeLine>>();
            _http.DefaultRequestHeaders.Clear();
            _http.DefaultRequestHeaders.Add("Authorization", Constants.PreToken + authToken);

            _content = new StringContent(
           JsonConvert.SerializeObject(getStationTimeLineParams), UTF8Encoding.UTF8, "application/json");

            _response = await _http.PostAsync(company.ToBaseUrl(ApiUrl.StationTimeLine), _content);

            if (_response.IsSuccessStatusCode)
            {
                try
                {
                    var res = JsonConvert.DeserializeObject<IEnumerable<StationTimeLine>>(await _response.Content.ReadAsStringAsync());
                    short ind = 1;
                    foreach (var item in res)
                    {
                        item.Index = 1;
                        ind++;
                    }
                    result.Status = true;
                    result.Result = res;
                }
                catch (Exception ex)
                {

                    result.Status = false;
                    result.Message = ex.Message;
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="company"></param>
        /// <returns></returns>
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
                    res.Result.OrEmpty().ToList().ForEach(p =>
                    {
                        p.MoveDate = p.MoveDate.ToUniversalTime();
                        p.RegisteredAt = p.RegisteredAt?.ToUniversalTime();
                    });
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="authToken"></param>
        /// <param name="company"></param>
        /// <returns></returns>
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
                    if (_content != null)
                        _content.Dispose();
                    if (_response != null)
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
