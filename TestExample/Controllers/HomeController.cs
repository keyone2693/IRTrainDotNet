using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IRTrainDotNet;
using IRTrainDotNet.Helpers;
using IRTrainDotNet.Models;
using MadPay724.Data.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestExample.Data.Models;

namespace TestExample.Controllers
{
    //
    [ApiController]
    public class HomeController : ControllerBase
    {

        private readonly IIRTrainApi _fadakTrainApi;
        private readonly MainDbContext _db;
        public HomeController(IIRTrainApi fadakTrainApi, MainDbContext db)
        {
            _fadakTrainApi = fadakTrainApi;
            _db = db;
        }
        [HttpGet("api/home/login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var res = await _fadakTrainApi.LoginAsync(loginModel,Company.Fadak);
            if (res.Status)
            {
                var token = (await _db.AuthTokens.ToListAsync()).SingleOrDefault();
                if (token == null)
                {
                    await _db.AuthTokens.AddAsync(new AuthToken { Value = res.Result });
                }
                else
                {
                    token.Value = res.Result;
                    _db.AuthTokens.Update(token);
                }
                await _db.SaveChangesAsync();
                return Ok(res);
            }
            else
            {
                return BadRequest(res.Message);
            }


        }
        [HttpGet("api/home/stations")]
        public async Task<IActionResult> GetStations()
        {
            var token = (await _db.AuthTokens.ToListAsync()).SingleOrDefault();

            if (await _fadakTrainApi.ValidateTokenWithRequestAsync(token.Value, Company.Fadak))
            {
                var res = await _fadakTrainApi.GetStationsAsync(token.Value, Company.Fadak);
                if (res.Status)
                {
                    return Ok(res);
                }
                else
                {
                    if (res.Unauthorized)
                    {
                        //login again
                        return Unauthorized(res);
                    }
                    else
                    {
                        return BadRequest(res);
                    }
                }
            }
            else
            {
                return Unauthorized("دوباره لاگین کنید"); ;
            }
        }
        [HttpGet("api/home/stations/{stationId}")]
        public async Task<IActionResult> GetStationById(int stationId)
        {
            var token = (await _db.AuthTokens.ToListAsync()).SingleOrDefault();

            if (await _fadakTrainApi.ValidateTokenWithRequestAsync(token.Value, Company.Fadak))
            {
                var res = await _fadakTrainApi.GetStationByIdAsync(token.Value, stationId, Company.Fadak);
                if (res.Status)
                {
                    return Ok(res);
                }
                else
                {
                    if (res.Unauthorized)
                    {
                        //login again
                        return Unauthorized(res);
                    }
                    else
                    {
                        return BadRequest(res);
                    }
                }
            }
            else
            {
                return Unauthorized("دوباره لاگین کنید");
            }
        }
        [HttpGet("api/home/GetWagonAvailableSeatCountAsync")]
        public async Task<IActionResult> GetWagonAvailableSeatCountAsync()
        {
            var getWagonAvailableSeatCountParams = new GetWagonAvailableSeatCountParams
            {
                FromStation = 1,
                ToStation = 191,
                GoingDate = DateTime.Now.AddDays(3),
                ReturnDate = DateTime.Now.AddDays(5),
                Gender = (int)Gender.Family,
                //کوپه دربست
                ExclusiveCompartment = false,
                AdultsCount = 1,
                ChildrenCount = 0,
                InfantsCount = 0
            };
            var token = (await _db.AuthTokens.ToListAsync()).SingleOrDefault();

            if (await _fadakTrainApi.ValidateTokenWithRequestAsync(token.Value, Company.Fadak))
            {
                var res = await _fadakTrainApi.GetWagonAvailableSeatCountAsync(token.Value, getWagonAvailableSeatCountParams, Company.Fadak);
                if (res.Status)
                {
                    return Ok(res);
                }
                else
                {
                    if (res.Unauthorized)
                    {
                        //login again
                        return Unauthorized(res);
                    }
                    else
                    {
                        return BadRequest(res);
                    }
                }
            }
            else
            {
                return Unauthorized("دوباره لاگین کنید"); ;
            }
        }
        [HttpPost("api/home/LockSeat")]
        public async Task<IActionResult> LockSeat()
        {
            var lockSeatParams = new LockSeatParams
            {
                SelectedWagon = new WagonAvailableSeatCount
                {
                    SelectionHint = null,
                    WagonName = "5 ستاره فدك با عصرانه",
                    CircularNumberSerial = 356,
                    RateCode = 89,
                    PathCode = 1,
                    WagonType = 604,
                    TrainNumber = 346,
                    CircularPeriod = 980301,
                    ExitTime = "18:50:00",
                    MoveDate = new DateTime(2019, 12, 19, 00, 00, 00),
                    IsCompartment = true,
                    CompartmentCapicity = 4,
                    Degree = 1,
                    Capacity = 16.0M,
                    Cost = 1400000,
                    CountingAll = 16,
                    HasAirConditioning = true,
                    HasMedia = true,
                    FullPrice = 1899500,
                    TimeOfArrival = "06:00",
                    RetStatus = 1,
                    DisabledReason = "",
                    SoldCount = 0,
                    RationCode = 2,
                    MinutesToExitDateTime = 4769
                },
                FromStation = 1,
                ToStation = 191,
                Gender = (int)Gender.Family,
                SeatCount = 1,
                SellMaster = 1,
                IsExclusiveCompartment = false
            };
            var token = (await _db.AuthTokens.ToListAsync()).SingleOrDefault();

            if (await _fadakTrainApi.ValidateTokenWithRequestAsync(token.Value, Company.Fadak))
            {
                var res = await _fadakTrainApi.LockSeatAsync(token.Value, lockSeatParams, Company.Fadak);
                if (res.Status)
                {
                    return Ok(res);
                }
                else
                {
                    if (res.Unauthorized)
                    {
                        //login again
                        return Unauthorized(res);
                    }
                    else
                    {
                        return BadRequest(res);
                    }
                }
            }
            else
            {
                return Unauthorized("دوباره لاگین کنید"); ;
            }
        }
        [HttpPost("api/home/LockSeatBulk")]
        public async Task<IActionResult> LockSeatBulk()
        {
            var lockSeatBulkParams = new LockSeatBulkParams
            {
                SelectedWagon = new WagonAvailableSeatCount
                {
                    SelectionHint = null,
                    WagonName = "5 ستاره فدك با عصرانه",
                    CircularNumberSerial = 356,
                    RateCode = 89,
                    PathCode = 1,
                    WagonType = 604,
                    TrainNumber = 346,
                    CircularPeriod = 980301,
                    ExitTime = "18:50:00",
                    MoveDate = new DateTime(2019, 12, 19, 00, 00, 00),
                    IsCompartment = true,
                    CompartmentCapicity = 4,
                    Degree = 1,
                    Capacity = 16.0M,
                    Cost = 1400000,
                    CountingAll = 16,
                    HasAirConditioning = true,
                    HasMedia = true,
                    FullPrice = 1899500,
                    TimeOfArrival = "06:00",
                    RetStatus = 1,
                    DisabledReason = "",
                    SoldCount = 0,
                    RationCode = 2,
                    MinutesToExitDateTime = 4769
                },
                FromStation = 1,
                ToStation = 191,
                Gender = (int)Gender.Family,
                SeatCount = 1,
                SellMaster = 1,
                IsExclusiveCompartment = false
            };
            var token = (await _db.AuthTokens.ToListAsync()).SingleOrDefault();

            if (await _fadakTrainApi.ValidateTokenWithRequestAsync(token.Value, Company.Fadak))
            {
                var res = await _fadakTrainApi.LockSeatBulkAsync(token.Value, lockSeatBulkParams, Company.Fadak);
                if (res.Status)
                {
                    return Ok(res);
                }
                else
                {
                    if (res.Unauthorized)
                    {
                        //login again
                        return Unauthorized(res);
                    }
                    else
                    {
                        return BadRequest(res);
                    }
                }
            }
            else
            {
                return Unauthorized("دوباره لاگین کنید"); ;
            }
        }
        [HttpPost("api/home/UnlockSeat")]
        public async Task<IActionResult> UnlockSeat()
        {
            var unlockSeatParams = new UnlockSeatParams
            {
                SaleId = 1,
                TrainNumber = 1,
                MoveDate = new DateTime(2019, 12, 19, 00, 00, 00)
            };
            var token = (await _db.AuthTokens.ToListAsync()).SingleOrDefault();

            if (await _fadakTrainApi.ValidateTokenWithRequestAsync(token.Value, Company.Fadak))
            {
                var res = await _fadakTrainApi.UnlockSeatAsync(token.Value, unlockSeatParams, Company.Fadak);
                if (res.Status)
                {
                    return Ok(res);
                }
                else
                {
                    if (res.Unauthorized)
                    {
                        //login again
                        return Unauthorized(res);
                    }
                    else
                    {
                        return BadRequest(res);
                    }
                }
            }
            else
            {
                return Unauthorized("دوباره لاگین کنید"); ;
            }
        }
        [HttpPost("api/home/SaveTicketsInfo")]
        public async Task<IActionResult> SaveTicketsInfo()
        {
            var saveTicketsInfoParams = new SaveTicketsInfoParams
            {
                PassengersInfo = new List<PassengerInfo>
                {
                    new PassengerInfo
                    {
                          Name = "",
                          Family = "",
                          BirthDate = DateTime.Now.AddYears(-25),
                          NationalCode = "",
                          Tariff = 1,
                          OptionalServiceId = 1,
                          PromotionCode = "",
                    }
                },
                SaleId = 1,
                Tel = "",
                Email = ""
            };
            var token = (await _db.AuthTokens.ToListAsync()).SingleOrDefault();

            if (await _fadakTrainApi.ValidateTokenWithRequestAsync(token.Value, Company.Fadak))
            {
                var res = await _fadakTrainApi.SaveTicketsInfoAsync(token.Value, saveTicketsInfoParams, Company.Fadak);
                if (res.Status)
                {
                    return Ok(res);
                }
                else
                {
                    if (res.Unauthorized)
                    {
                        //login again
                        return Unauthorized(res);
                    }
                    else
                    {
                        return BadRequest(res);
                    }
                }
            }
            else
            {
                return Unauthorized("دوباره لاگین کنید"); ;
            }
        }
        [HttpPost("api/home/RegisterTickets")]
        public async Task<IActionResult> RegisterTickets()
        {
            var registerTicketParams = new RegisterTicketParams
            {
                SellMasterId = 5
            };
            var token = (await _db.AuthTokens.ToListAsync()).SingleOrDefault();

            if (await _fadakTrainApi.ValidateTokenWithRequestAsync(token.Value, Company.Fadak))
            {
                var res = await _fadakTrainApi.RegisterTicketsAsync(token.Value, registerTicketParams, Company.Fadak);
                if (res.Status)
                {
                    return Ok(res);
                }
                else
                {
                    if (res.Unauthorized)
                    {
                        //login again
                        return Unauthorized(res);
                    }
                    else
                    {
                        return BadRequest(res);
                    }
                }
            }
            else
            {
                return Unauthorized("دوباره لاگین کنید"); ;
            }
        }
        [HttpPost("api/home/TicketReportA")]
        public async Task<IActionResult> TicketReportA()
        {
            var ticketReportAParams = new TicketReportAParams
            {
                Tel = "",
                SaleId = 1
            };
            var token = (await _db.AuthTokens.ToListAsync()).SingleOrDefault();

            if (await _fadakTrainApi.ValidateTokenWithRequestAsync(token.Value, Company.Fadak))
            {
                var res = await _fadakTrainApi.TicketReportAAsync(token.Value, ticketReportAParams, Company.Fadak);
                if (res.Status)
                {
                    return Ok(res);
                }
                else
                {
                    if (res.Unauthorized)
                    {
                        //login again
                        return Unauthorized(res);
                    }
                    else
                    {
                        return BadRequest(res);
                    }
                }
            }
            else
            {
                return Unauthorized("دوباره لاگین کنید"); ;
            }
        }
        [HttpPost("api/home/RefundTicketInfo")]
        public async Task<IActionResult> RefundTicketInfo()
        {
            var refundTicketInfoParams = new RefundTicketInfoParams
            {
                SaleId =1,
                TicketSeries ="",
                SaleCenterCode =1,
                WagonNumber =1,
                SeatNumber =1,
            };
            var token = (await _db.AuthTokens.ToListAsync()).SingleOrDefault();

            if (await _fadakTrainApi.ValidateTokenWithRequestAsync(token.Value, Company.Fadak))
            {
                var res = await _fadakTrainApi.RefundTicketInfoAsync(token.Value, refundTicketInfoParams, Company.Fadak);
                if (res.Status)
                {
                    return Ok(res);
                }
                else
                {
                    if (res.Unauthorized)
                    {
                        //login again
                        return Unauthorized(res);
                    }
                    else
                    {
                        return BadRequest(res);
                    }
                }
            }
            else
            {
                return Unauthorized("دوباره لاگین کنید"); ;
            }
        }
        [HttpPost("api/home/RefundTicket")]
        public async Task<IActionResult> RefundTicket()
        {
            var refundTicketParams = new RefundTicketParams
            {
                SaleId = 1,
                TicketSeries = "",
                SaleCenterCode = 1,
                WagonNumber = 1,
                SeatNumber = 1,
            };
            var token = (await _db.AuthTokens.ToListAsync()).SingleOrDefault();

            if (await _fadakTrainApi.ValidateTokenWithRequestAsync(token.Value, Company.Fadak))
            {
                var res = await _fadakTrainApi.RefundTicketAsync(token.Value, refundTicketParams, Company.Fadak);
                if (res.Status)
                {
                    return Ok(res);
                }
                else
                {
                    if (res.Unauthorized)
                    {
                        //login again
                        return Unauthorized(res);
                    }
                    else
                    {
                        return BadRequest(res);
                    }
                }
            }
            else
            {
                return Unauthorized("دوباره لاگین کنید"); ;
            }
        }
        [HttpGet("api/home/UserSales")]
        public async Task<IActionResult> UserSales()
        {
            var token = (await _db.AuthTokens.ToListAsync()).SingleOrDefault();

            if (await _fadakTrainApi.ValidateTokenWithRequestAsync(token.Value, Company.Fadak))
            {
                var res = await _fadakTrainApi.UserSalesAsync(token.Value, Company.Fadak);
                if (res.Status)
                {
                    return Ok(res);
                }
                else
                {
                    if (res.Unauthorized)
                    {
                        //login again
                        return Unauthorized(res);
                    }
                    else
                    {
                        return BadRequest(res);
                    }
                }
            }
            else
            {
                return Unauthorized("دوباره لاگین کنید"); ;
            }
        }
        [HttpGet("api/home/AgentCredit")]
        public async Task<IActionResult> AgentCredit()
        {
            var token = (await _db.AuthTokens.ToListAsync()).SingleOrDefault();

            if (await _fadakTrainApi.ValidateTokenWithRequestAsync(token.Value, Company.Fadak))
            {
                var res = await _fadakTrainApi.AgentCreditAsync(token.Value, Company.Fadak);
                if (res.Status)
                {
                    return Ok(res);
                }
                else
                {
                    if (res.Unauthorized)
                    {
                        //login again
                        return Unauthorized(res);
                    }
                    else
                    {
                        return BadRequest(res);
                    }
                }
            }
            else
            {
                return Unauthorized("دوباره لاگین کنید"); ;
            }
        }
    }
}
