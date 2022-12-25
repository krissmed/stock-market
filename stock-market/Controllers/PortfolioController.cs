using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using stock_market.Model;
using stock_market.DAL;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace stock_market.Controllers
{
    [Route("[controller]/[action]")]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioRepository _db;
        private readonly ILogger<PortfolioController> _log;
        private const string _loggetInn = "loggetInn";
        private int userid = -1;

        public PortfolioController(IPortfolioRepository db, ILogger<PortfolioController> log)
        {
            _db = db;
            _log = log;
        }
        
        public async Task<ActionResult> GetCurrentPortfolio()
        {
            if (HttpContext.Session.GetString(_loggetInn) == "loggetInn")
            {
                userid = 1;
            }
                else
                {
                if (HttpContext.Session.GetInt32(_loggetInn) == null || HttpContext.Session.GetInt32(_loggetInn) == -1)
                {
                    _log.LogError("PortfolioController: User is not logged in, tried to get current portfolio");
                    return Unauthorized("User is not logged in");
                }
                    userid = HttpContext.Session.GetInt32(_loggetInn).Value;
                }
            _log.LogInformation("PortfolioController: Got portfolio");
            return Ok(await _db.GetCurrentPortfolio(userid));
        }

        public async Task<ActionResult> GetHistoricalPortfolios()
        {
            if (HttpContext.Session.GetString(_loggetInn) == "loggetInn")
            {
                userid = 1;
            }
            else
            {
                if (HttpContext.Session.GetInt32(_loggetInn) == null || HttpContext.Session.GetInt32(_loggetInn) == -1)
                {
                    _log.LogError("PortfolioController: User is not logged in, tried to get historical portfolios");
                    return Unauthorized("User is not logged in");
                }
                userid = HttpContext.Session.GetInt32(_loggetInn).Value;
            }
            _log.LogInformation("PortfolioController: Got Historicalportfolio");
            return Ok(await _db.GetHistoricalPortfolios(userid));
        }
    }
}