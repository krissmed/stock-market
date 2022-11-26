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

namespace stock_market.Controllers
{
    [Route("[controller]/[action]")]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioRepository _db;
        private readonly ILogger<PortfolioController> _log;

        public PortfolioController(IPortfolioRepository db, ILogger<PortfolioController> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<string> GetCurrentPortfolio()
        {
            _log.LogInformation("PortfolioController: Got portfolio");
            return await _db.GetCurrentPortfolio();
        }

        public async Task<string> GetHistoricalPortfolios()
        {
            _log.LogInformation("PortfolioController: Got Historicalportfolio");
            return await _db.GetHistoricalPortfolios();
        }

    }
    

}