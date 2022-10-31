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

namespace stock_market.Controllers
{
    [Route("[controller]/[action]")]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioRepository _db;

        public PortfolioController(IPortfolioRepository db)
        {
            _db = db;
        }

        public async Task<string> GetCurrentPortfolio()
        {
            return await _db.GetCurrentPortfolio();
        }

        public async Task<string> GetHistoricalPortfolios()
        {
            return await _db.GetHistoricalPortfolios();
        }

    }
    

}