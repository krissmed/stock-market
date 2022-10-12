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
        }


        public string GetFullName(int userid)
        {
            string fname = _db.Users.Find(userid).first_name;
            string lname = _db.Users.Find(userid).last_name;
            return fname + " " + lname;
        }

        public string GetFName(int userid)
        {
            string fname = _db.Users.Find(userid).first_name;
            return fname;
        }

        public string GetLName(int userid)
        {
            string lname = _db.Users.Find(userid).last_name;
            return lname;
        }





        public int GetUserID()
        {
            //For now returns 1 as there is only 1 user in the system - John Doe.
            return 1;
        }
    }

}