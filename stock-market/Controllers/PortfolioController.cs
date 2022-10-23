using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using stock_market.Model;

namespace stock_market.Controllers
{
    [Route("[controller]/[action]")]
    public class PortfolioController : ControllerBase
    {
        private readonly mainDB _db;

        public PortfolioController(mainDB db)
        {
            _db = db;
        }

        public string GetCurrentPortfolio()
        {
            
            var user = _db.Users.First();
            var timestamp = _db.timestamps.OrderByDescending(t => t.unix).First();

            var portfolio = _db.portfolios.First(p => p.user.id == user.id && p.timestamp.unix == timestamp.unix);
            string json = JsonConvert.SerializeObject(portfolio);
            return json;
        }

        public string GetHistoricalPortfolios()
        {
            var user = _db.Users.First();
            var portfolios = _db.portfolios.Where(p => p.user.id == user.id).ToList();
            //return portfolios
            string json = JsonConvert.SerializeObject(portfolios);
            return json;
        }

    }
    

}