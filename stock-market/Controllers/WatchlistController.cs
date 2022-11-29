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
using Microsoft.AspNetCore.Authentication;
using stock_market.DAL;
using Microsoft.Extensions.Logging;


namespace stock_market.Controllers
{
    [Route("[controller]/[action]")]
    public class WatchlistController : ControllerBase
    {
        private readonly IWatchlistRepository _db;
        private readonly ILogger<WatchlistController> _log;

        public WatchlistController(IWatchlistRepository db, ILogger<WatchlistController> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<string> GetFullWatchlist()
        {
            return await _db.GetFullWatchlist();
        }

        public async Task<ActionResult> AddStock(string ticker, int amount, double target_price)
        {

            if (ModelState.IsValid)
            {
                bool ok = await _db.AddStock(ticker, amount, target_price);
                if (!ok)
                {
                    _log.LogError("TransactionController: Could not add to watchlist");
                    return BadRequest("Could not add to watchlist");
                }
                _log.LogInformation("TransactionController: User added to watchlist");
                return Ok("User added to watchlist");
            }
            _log.LogError("TransactionController: Fault in InputVal ");
            return BadRequest("Fault in InputVal");
        }

        public async Task<ActionResult> DeleteStock(int id)
        {
            bool ok = await _db.DeleteStock(id);
            if (!ok)
            {
                _log.LogError("TransactionController: Could not delete from watchlist");
                return BadRequest("Could not delete from watchlist");
            }
            _log.LogInformation("TransactionController: User deleted from watchlist");
            return Ok("User deleted from watchlist");
        }

        public async Task<ActionResult> UpdateStock(int id, int amount, double target_price)
        {

            if (ModelState.IsValid)
            {
                bool ok = await _db.UpdateStock(id, amount, target_price);
                if (!ok)
                {
                    _log.LogError("TransactionController: Could not update watchlist");
                    return BadRequest("Could not update watchlist");
                }
                _log.LogInformation("TransactionController: User updated to watchlist");
                return Ok("User updated to watchlist");
            }
            _log.LogError("TransactionController: Fault in InputVal ");
            return BadRequest("Fault in InputVal");
        }
    }
}