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
using Microsoft.AspNetCore.Http;

namespace stock_market.Controllers
{
    [Route("[controller]/[action]")]
    public class WatchlistController : ControllerBase
    {
        private readonly IWatchlistRepository _db;
        private readonly ILogger<WatchlistController> _log;
        private const string _loggetInn = "loggetInn";
        private int userid = -1;

        public WatchlistController(IWatchlistRepository db, ILogger<WatchlistController> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<ActionResult> GetFullWatchlist()
        {
            if (HttpContext.Session.GetInt32(_loggetInn) == null || HttpContext.Session.GetInt32(_loggetInn) == -1)
            {
                _log.LogError("WatchlistController: User is not logged in, tried to get full watchlist");
                return Unauthorized("User is not logged in");
            }
            return Ok(await _db.GetFullWatchlist(HttpContext.Session.GetInt32(_loggetInn).Value));
        }

        public async Task<ActionResult> AddStock(string ticker, int amount, double target_price)
        {
            if (HttpContext.Session.GetString(_loggetInn) == "loggetInn")
            {
                userid = 1;
            }
            else
            {
                userid = HttpContext.Session.GetInt32(_loggetInn).Value;
            }
            if (HttpContext.Session.GetInt32(_loggetInn) == null || HttpContext.Session.GetInt32(_loggetInn) == -1)
            {
                _log.LogError("WatchlistController: User is not logged in, tried to get full watchlist");
                return Unauthorized("User is not logged in");
            }

            if (ModelState.IsValid)
            {
                bool ok = await _db.AddStock(ticker, amount, target_price, userid);
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
            if (HttpContext.Session.GetString(_loggetInn) == "loggetInn")
            {
                userid = 1;
            }
            else
            {
                userid = HttpContext.Session.GetInt32(_loggetInn).Value;
            }

            if (HttpContext.Session.GetInt32(_loggetInn) == null || HttpContext.Session.GetInt32(_loggetInn) == -1)
            {
                _log.LogError("WatchlistController: User is not logged in, tried to delete a stock");
                return Unauthorized("User is not logged in");
            }
            
            bool ok = await _db.DeleteStock(id, userid);
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
            if (HttpContext.Session.GetInt32(_loggetInn) == null || HttpContext.Session.GetInt32(_loggetInn) == -1)
            {
                _log.LogError("WatchlistController: User is not logged in, tried to update a stock");
                return Unauthorized("User is not logged in");
            }
            int userid = HttpContext.Session.GetInt32(_loggetInn).Value;


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