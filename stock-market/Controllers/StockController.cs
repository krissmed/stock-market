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
using Microsoft.Extensions.Logging;


namespace stock_market.Controllers
{
    [Route("[controller]/[action]")]
    public class StockController : ControllerBase
    {
        private readonly IStockRepository _db;
        private readonly ILogger<StockController> _log;


        public StockController(IStockRepository db, ILogger<StockController> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<ActionResult> AddStock(string ticker)
        {
            if (ModelState.IsValid)
            {
                bool ok = await _db.AddStock(ticker);
                if (!ok)
                {
                    _log.LogError("StockController: Could not add stock");
                    return BadRequest("Could not add stock");
                }
                _log.LogInformation("StockController: User added stock");
                return Ok("User added stock");
            }
            _log.LogError("StockController::Fault in InputVal ");
            return BadRequest("Fault in InputVal");
        }

        public async Task<List<BaseStock>> GetStocks()
        {
            return await _db.GetStocks();
        }

        public async Task<ActionResult> DeleteStock(string ticker)
        {
            if (ModelState.IsValid)
            {
                bool ok = await _db.DeleteStock(ticker);
                if (!ok)
                {
                    _log.LogError("StockController: Could not delete stock");
                    return BadRequest("Could not delete stock");
                }
                _log.LogInformation("StockController: User deleted stock");
                return Ok("User deleted stock");
            }
            _log.LogError("StockController::Fault in InputVal ");
            return BadRequest("Fault in InputVal");
         }
        

    }
}