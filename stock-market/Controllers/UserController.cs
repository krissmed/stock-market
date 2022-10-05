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
    public class UserController : ControllerBase
    {
        private readonly mainDB _db;

        public UserController(mainDB db)
        {
            _db = db;
            InitGuest();
        }

        public void InitGuest()
        {
            User guest = new User();
            guest.first_name = "John";
            guest.last_name = "Doe";
            guest.curr_balance = 10_000;
            guest.curr_balance_liquid = 10_000;
            guest.curr_balance_stock = 0;

            Transaction trans1 = new Transaction();
            trans1.price = 1_000;
            trans1.ticker = "AAPL";

            Transaction trans2 = new Transaction();
            trans2.price = 2_500;
            trans2.ticker = "TSLA";

            List<Transaction> translist = new List<Transaction>();
            translist.Add(trans1);
            translist.Add(trans2);

            guest.transactions = translist;
            

            _db.Users.Add(guest);
            _db.SaveChanges();
        }

        public List<User> ListAll()
        {
            List<User> Users = _db.Users.ToList();
            return Users;
        }

        public int GetUserID()
        {
            //For now returns 1 as there is only 1 user in the system - John Doe.
            return 1;
        }
    }

}