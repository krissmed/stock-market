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