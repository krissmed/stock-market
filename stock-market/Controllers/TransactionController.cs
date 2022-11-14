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

namespace stock_market.Controllers
{
    [Route("[controller]/[action]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _db;

        public TransactionController(ITransactionRepository db)
        {
            _db = db;
        }

        public async Task<bool> SellStock(string ticker, int amount)
        {
            return await _db.SellStock(ticker, amount);
        }

        public async Task<bool> BuyStock(string ticker, int amount)
        {
            if (ModelState.IsValid)
            {
                return await _db.BuyStock(ticker, amount);
            }
            return true;
        }

        public async Task<List<Transaction>> ListAll()
        {
            return await _db.ListAll();
        }


    }

}