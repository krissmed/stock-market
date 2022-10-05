using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stock_market.Model;

namespace stock_market.Controllers
{
    [Route("[controller]/[action]")]
    public class TransactionController : ControllerBase
    {
        private readonly mainDB _db;

        public TransactionController(mainDB db)
        {
            _db = db;
        }

        public bool CreateTransaction()
        {
            try
            {
                string ticker = "BTC";
                int price = 18_000;
                int userid = 1;

                Transaction transaction = new Transaction();
                transaction.ticker = ticker;
                transaction.price = price;

                User user = _db.Users.Find(userid);
                RegisterTransaction(transaction, user);

                _db.transactions.Add(transaction);
                _db.SaveChanges();
                return true;

            }
            catch
            {
                return false;
            }

        }

        private static void RegisterTransaction(Transaction transaction, User user)
        {
            user.transactions.Add(transaction);
        }

        public List<Transaction> ListAll()
        {
            List<Transaction> transactions = _db.transactions.ToList();
            return transactions;
        }


    }

}