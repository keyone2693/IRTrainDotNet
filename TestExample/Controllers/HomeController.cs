using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IrFadakTrainDotNet;
using IrFadakTrainDotNet.Helpers;
using IrFadakTrainDotNet.Models;
using MadPay724.Data.DatabaseContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestExample.Data.Models;

namespace TestExample.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {

        private readonly IFadakTrainApi _fadakTrainApi;
        private readonly MainDbContext _db;
        public HomeController(IFadakTrainApi fadakTrainApi, MainDbContext db)
        {
            _fadakTrainApi = fadakTrainApi;
            _db = db;
        }

        [HttpGet("api/home/login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var res = await _fadakTrainApi.LoginAsync(loginModel);
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

            if (await _fadakTrainApi.ValidateTokenWithRequestAsync(token.Value))
            {
                var res = await _fadakTrainApi.GetStationsAsync(token.Value);
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

            if (await _fadakTrainApi.ValidateTokenWithRequestAsync(token.Value))
            {
                var res = await _fadakTrainApi.GetStationByIdAsync(token.Value, stationId);
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

            if (await _fadakTrainApi.ValidateTokenWithRequestAsync(token.Value))
            {
                var res = await _fadakTrainApi.GetWagonAvailableSeatCountAsync(token.Value, getWagonAvailableSeatCountParams);
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
