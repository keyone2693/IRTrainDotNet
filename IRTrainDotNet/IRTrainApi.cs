using Flurl.Http;
using IRTrainDotNet.Helpers;
using IRTrainDotNet.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace IRTrainDotNet
{
    public class IRTrainApi : IIRTrainApi
    {
        private string error = "";

        #region ctor
        #endregion

        #region Synchronous
        //-------------------------------------------
        #region Auth
        public ServiceResult<String> Login(LoginModel loginModel, Company company)
        {
            var result = new ServiceResult<string>();


            var response = company.ToBaseUrl(ApiUrl.Login)
                .WithHeader("Content-Type", "application/json")
                .PostJsonAsync(loginModel).Result;

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = response.GetJsonAsync<IrTrainResult<String>>().Result;
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
            var response = company.ToBaseUrl(ApiUrl.GetLastVersion)
                .WithHeader("Content-Type", "application/json")
                .WithHeader("Authorization", Constants.PreToken + token)
                .GetAsync().Result;

            if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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

            var response = company.ToBaseUrl(ApiUrl.Stations)
               .WithHeader("Content-Type", "application/json")
               .WithHeader("Authorization", Constants.PreToken + authToken)
               .GetAsync().Result;

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = response.GetJsonAsync<IrTrainResult<IEnumerable<Station>>>().Result;
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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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

            var response = company.ToBaseUrl(ApiUrl.Station + "/" + stationId)
               .WithHeader("Content-Type", "application/json")
               .WithHeader("Authorization", Constants.PreToken + authToken)
               .GetAsync().Result;


            var result = new ServiceResult<Station>();

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = response.GetJsonAsync<IrTrainResult<Station>>().Result;
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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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

            var response = company.ToBaseUrl(ApiUrl.GetLastVersion)
               .WithHeader("Content-Type", "application/json")
               .WithHeader("Authorization", Constants.PreToken + authToken)
               .GetAsync().Result;

            var result = new ServiceResult<string>();


            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = response.GetJsonAsync<IrTrainResult<string>>().Result;
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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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
            var response = company.ToBaseUrl(ApiUrl.GetWagonAvailableSeatCount)
              .WithHeader("Content-Type", "application/json")
              .WithHeader("Authorization", Constants.PreToken + authToken)
              .PostJsonAsync(getWagonAvailableSeatCountParams).Result;

            var result = new ServiceResult<GetWagonAvailableSeatCountResult>();


            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = response.GetJsonAsync<IrTrainResult<GetWagonAvailableSeatCountResult>>().Result;
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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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
            var response = company.ToBaseUrl(ApiUrl.LockSeat)
            .WithHeader("Content-Type", "application/json")
            .WithHeader("Authorization", Constants.PreToken + authToken)
            .PostJsonAsync(lockSeatParams).Result;

            var result = new ServiceResult<LockSeatResult>();

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = response.GetJsonAsync<IrTrainResult<LockSeatResult>>().Result;
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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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

            var response = company.ToBaseUrl(ApiUrl.LockSeatBulk)
         .WithHeader("Content-Type", "application/json")
         .WithHeader("Authorization", Constants.PreToken + authToken)
         .PostJsonAsync(lockSeatBulkParams).Result;

            var result = new ServiceResult<LockSeatBulkResult>();




            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = response.GetJsonAsync<IrTrainResult<LockSeatBulkResult>>().Result;
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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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
            var response = company.ToBaseUrl(ApiUrl.UnlockSeat)
.WithHeader("Content-Type", "application/json")
.WithHeader("Authorization", Constants.PreToken + authToken)
.PostJsonAsync(unlockSeatParams).Result;

            var result = new ServiceResult<EmptyResult>();


            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = response.GetJsonAsync<IrTrainResult<EmptyResult>>().Result;

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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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
            var response = company.ToBaseUrl(ApiUrl.SaveTicketsInfo)
.WithHeader("Content-Type", "application/json")
.WithHeader("Authorization", Constants.PreToken + authToken)
.PostJsonAsync(saveTicketsInfoParams).Result;

            var result = new ServiceResult<int>();

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = response.GetJsonAsync<IrTrainResult<int>>().Result;

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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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
            var response = company.ToBaseUrl(ApiUrl.RegisterTickets)
.WithHeader("Content-Type", "application/json")
.WithHeader("Authorization", Constants.PreToken + authToken)
.PostJsonAsync(registerTicketParams).Result;

            var result = new ServiceResult<EmptyResult>();


            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = response.GetJsonAsync<IrTrainResult<EmptyResult>>().Result;

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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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
            var response = company.ToBaseUrl(ApiUrl.TicketReportA)
.WithHeader("Content-Type", "application/json")
.WithHeader("Authorization", Constants.PreToken + authToken)
.PostJsonAsync(ticketReportAParams).Result;

            var result = new ServiceResult<TicketReportAResult>();




            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = response.GetJsonAsync<IrTrainResult<TicketReportAResult>>().Result;

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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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
            var response = company.ToBaseUrl(ApiUrl.RefundTicketInfo)
.WithHeader("Content-Type", "application/json")
.WithHeader("Authorization", Constants.PreToken + authToken)
.PostJsonAsync(refundTicketInfoParams).Result;

            var result = new ServiceResult<RefundTicketInfoResult>();


            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = response.GetJsonAsync<IrTrainResult<RefundTicketInfoResult>>().Result;

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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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
            var response = company.ToBaseUrl(ApiUrl.RefundTicket)
.WithHeader("Content-Type", "application/json")
.WithHeader("Authorization", Constants.PreToken + authToken)
.PostJsonAsync(refundTicketParams).Result;

            var result = new ServiceResult<int>();


            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = response.GetJsonAsync<IrTrainResult<int>>().Result;

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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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
            var response = company.ToBaseUrl(ApiUrl.UserSales)
.WithHeader("Content-Type", "application/json")
.WithHeader("Authorization", Constants.PreToken + authToken)
.PostJsonAsync(authToken).Result;

            var result = new ServiceResult<IEnumerable<UserSaleMetadata>>();


            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = response.GetJsonAsync<IrTrainResult<IEnumerable<UserSaleMetadata>>>().Result;

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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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

            var response = company.ToBaseUrl(ApiUrl.AgentCredit)
.WithHeader("Content-Type", "application/json")
.WithHeader("Authorization", Constants.PreToken + authToken)
.GetAsync().Result;

            var result = new ServiceResult<long>();


            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = response.GetJsonAsync<IrTrainResult<long>>().Result;

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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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


            var response = await company.ToBaseUrl(ApiUrl.Login)
                .WithHeader("Content-Type", "application/json")
                .PostJsonAsync(loginModel);

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = await response.GetJsonAsync<IrTrainResult<String>>();
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
            var response = await company.ToBaseUrl(ApiUrl.GetLastVersion)
                .WithHeader("Content-Type", "application/json")
                .WithHeader("Authorization", Constants.PreToken + token)
                .GetAsync();

            if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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

            var response = await company.ToBaseUrl(ApiUrl.Stations)
               .WithHeader("Content-Type", "application/json")
               .WithHeader("Authorization", Constants.PreToken + authToken)
               .GetAsync();

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = await response.GetJsonAsync<IrTrainResult<IEnumerable<Station>>>();
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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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

            var response = await company.ToBaseUrl(ApiUrl.Station + "/" + stationId)
               .WithHeader("Content-Type", "application/json")
               .WithHeader("Authorization", Constants.PreToken + authToken)
               .GetAsync();


            var result = new ServiceResult<Station>();

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = await response.GetJsonAsync<IrTrainResult<Station>>();
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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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

            var response = await company.ToBaseUrl(ApiUrl.GetLastVersion)
               .WithHeader("Content-Type", "application/json")
               .WithHeader("Authorization", Constants.PreToken + authToken)
               .GetAsync();

            var result = new ServiceResult<string>();


            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = await response.GetJsonAsync<IrTrainResult<string>>();
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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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
            var response = await company.ToBaseUrl(ApiUrl.GetWagonAvailableSeatCount)
              .WithHeader("Content-Type", "application/json")
              .WithHeader("Authorization", Constants.PreToken + authToken)
              .PostJsonAsync(getWagonAvailableSeatCountParams);

            var result = new ServiceResult<GetWagonAvailableSeatCountResult>();


            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = await response.GetJsonAsync<IrTrainResult<GetWagonAvailableSeatCountResult>>();
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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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
            var response = await company.ToBaseUrl(ApiUrl.LockSeat)
            .WithHeader("Content-Type", "application/json")
            .WithHeader("Authorization", Constants.PreToken + authToken)
            .PostJsonAsync(lockSeatParams);

            var result = new ServiceResult<LockSeatResult>();

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = await response.GetJsonAsync<IrTrainResult<LockSeatResult>>();
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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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

            var response = await company.ToBaseUrl(ApiUrl.LockSeatBulk)
         .WithHeader("Content-Type", "application/json")
         .WithHeader("Authorization", Constants.PreToken + authToken)
         .PostJsonAsync(lockSeatBulkParams);

            var result = new ServiceResult<LockSeatBulkResult>();




            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = await response.GetJsonAsync<IrTrainResult<LockSeatBulkResult>>();
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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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
            var response = await company.ToBaseUrl(ApiUrl.UnlockSeat)
.WithHeader("Content-Type", "application/json")
.WithHeader("Authorization", Constants.PreToken + authToken)
.PostJsonAsync(unlockSeatParams);

            var result = new ServiceResult<EmptyResult>();


            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = await response.GetJsonAsync<IrTrainResult<EmptyResult>>();

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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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
            var response = await company.ToBaseUrl(ApiUrl.SaveTicketsInfo)
.WithHeader("Content-Type", "application/json")
.WithHeader("Authorization", Constants.PreToken + authToken)
.PostJsonAsync(saveTicketsInfoParams);

            var result = new ServiceResult<int>();

            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = await response.GetJsonAsync<IrTrainResult<int>>();

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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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
            var response = await company.ToBaseUrl(ApiUrl.RegisterTickets)
.WithHeader("Content-Type", "application/json")
.WithHeader("Authorization", Constants.PreToken + authToken)
.PostJsonAsync(registerTicketParams);

            var result = new ServiceResult<EmptyResult>();


            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = await response.GetJsonAsync<IrTrainResult<EmptyResult>>();

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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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
            var response = await company.ToBaseUrl(ApiUrl.TicketReportA)
.WithHeader("Content-Type", "application/json")
.WithHeader("Authorization", Constants.PreToken + authToken)
.PostJsonAsync(ticketReportAParams);

            var result = new ServiceResult<TicketReportAResult>();




            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = await response.GetJsonAsync<IrTrainResult<TicketReportAResult>>();

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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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
            var response = await company.ToBaseUrl(ApiUrl.RefundTicketInfo)
.WithHeader("Content-Type", "application/json")
.WithHeader("Authorization", Constants.PreToken + authToken)
.PostJsonAsync(refundTicketInfoParams);

            var result = new ServiceResult<RefundTicketInfoResult>();


            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = await response.GetJsonAsync<IrTrainResult<RefundTicketInfoResult>>();

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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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
            var response = await company.ToBaseUrl(ApiUrl.RefundTicket)
.WithHeader("Content-Type", "application/json")
.WithHeader("Authorization", Constants.PreToken + authToken)
.PostJsonAsync(refundTicketParams);

            var result = new ServiceResult<int>();


            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = await response.GetJsonAsync<IrTrainResult<int>>();

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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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
            var response = await company.ToBaseUrl(ApiUrl.UserSales)
.WithHeader("Content-Type", "application/json")
.WithHeader("Authorization", Constants.PreToken + authToken)
.PostJsonAsync(authToken);

            var result = new ServiceResult<IEnumerable<UserSaleMetadata>>();


            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = await response.GetJsonAsync<IrTrainResult<IEnumerable<UserSaleMetadata>>>();

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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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

            var response = await company.ToBaseUrl(ApiUrl.AgentCredit)
.WithHeader("Content-Type", "application/json")
.WithHeader("Authorization", Constants.PreToken + authToken)
.GetAsync();

            var result = new ServiceResult<long>();


            if (response.StatusCode == (int)HttpStatusCode.OK)
            {
                var res = await response.GetJsonAsync<IrTrainResult<long>>();

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
            else if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
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
