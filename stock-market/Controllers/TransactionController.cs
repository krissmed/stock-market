using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using stock_market.Model;
using stock_market.DAL;
using Microsoft.Extensions.Logging;
using Serilog;
using Microsoft.AspNetCore.Http;

namespace stock_market.Controllers
{
    [Route("[controller]/[action]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _db;
        private readonly ILogger<TransactionController> _log;
        private const string _loggetInn = "loggetInn";


        public TransactionController(ITransactionRepository db, ILogger<TransactionController> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<ActionResult> SellStock(string ticker, int amount)
        {
            if (HttpContext.Session.GetInt32(_loggetInn) == null || HttpContext.Session.GetInt32(_loggetInn) == -1)
            {
                _log.LogError("TransactionController: User is not logged in, tried to sell a stock");
                return Unauthorized("User is not logged in");
            }
            int userid = HttpContext.Session.GetInt32(_loggetInn).Value;

            if (ModelState.IsValid)
            {
                bool ok = await _db.SellStock(ticker, amount, userid);
                if (!ok)
                {
                    _log.LogError("TransactionController: Could not sell stock");
                    return BadRequest("Could not sell stock");
                }
                _log.LogInformation("TransactionController: User sold stock");
                return Ok("User sold stock");
            }
            _log.LogError("TransactionController: Fault in InputVal ");
            return BadRequest("Fault in InputVal");

        }

        public async Task<ActionResult> BuyStock(string ticker, int amount)
        {
            if (HttpContext.Session.GetInt32(_loggetInn) == null || HttpContext.Session.GetInt32(_loggetInn) == -1)
            {
                _log.LogError("TransactionController: User is not logged in, tried to buy a stock");
                return Unauthorized("User is not logged in");
            }
            int userid = HttpContext.Session.GetInt32(_loggetInn).Value;

            if (ModelState.IsValid)
            {
                bool ok = await _db.BuyStock(ticker, amount, userid);
                if (!ok)
                {
                    _log.LogError("TransactionController: Could not buy stock");
                    return BadRequest("Could not buy stock");
                }
                _log.LogInformation("User bought stock");
                return Ok("User bought stock");
            }
            _log.LogError("TransactionController: Fault in InputVal ");
            return BadRequest("Fault in InputVal");
        }

        public async Task<ActionResult> ListAll()
        {
            if (HttpContext.Session.GetInt32(_loggetInn) == null || HttpContext.Session.GetInt32(_loggetInn) == -1)
            {
                _log.LogError("TransactionController: User is not logged in, tried to list all stocks");
                return Unauthorized("User is not logged in");
            }
            int userid = HttpContext.Session.GetInt32(_loggetInn).Value;

            _log.LogInformation("Listed all transactions");
            return Ok(await _db.ListAll(userid));
        }


    }

}