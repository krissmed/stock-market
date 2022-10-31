using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using stock_market.Model;
using stock_market.DAL;

namespace stock_market.Controllers
{
    [Route("[controller]/[action]")]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _db;
        

        public StockController(IStockRepository db)
        {
            _db = db;
        }

        public async Task<bool> AddStock(string ticker)
        {
            return await _db.AddStock(ticker);
        }

        public async Task<List<BaseStock>> GetStocks()
        {
            return await _db.GetStocks();
        }

        public async Task<bool> DeleteStock(string ticker)
        {
            return await _db.DeleteStock(ticker);
        }

    }
}