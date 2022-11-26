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


namespace stock_market.Controllers
{
    [Route("[controller]/[action]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _db;
        private readonly ILogger<TransactionController> _log;


        public TransactionController(ITransactionRepository db, ILogger<TransactionController> log)
        {
            _db = db;
            _log = log;
        }

        public async Task<ActionResult> SellStock(string ticker, int amount)
        {
            if (ModelState.IsValid)
            {
                bool ok = await _db.SellStock(ticker, amount);
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


            //bool ok = 
            //if (!ok)
            //{
            //    _log.LogInformation("kunne ikke selge stock");
            //    return BadRequest("Kunne ikke selge stock");
            //}
            //_log.LogInformation("Hei loggen!");
            //return Ok("kun");
        }

        public async Task<ActionResult> BuyStock(string ticker, int amount)
        {
            if (ModelState.IsValid)
            {
                bool ok = await _db.BuyStock(ticker, amount);
                if (!ok)
                {
                    _log.LogError("TransactionController: Could not buy stock");
                    return BadRequest("Could not buy stock");
                }
                _log.LogInformation("User bought stock");
                return Ok("User buy stock");
            }
            _log.LogError("TransactionController: Fault in InputVal ");
            return BadRequest("Fault in InputVal");
        }

        public async Task<List<Transaction>> ListAll()
        {
            _log.LogInformation("Listed all transactions");
            return await _db.ListAll();
        }


    }

}