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
        private readonly UsersDB _UsersDB;

        public UserController(UsersDB UserDb)
        {
            _UsersDB = UserDb;
        }

        public List<User> HentAlle()
        {
            List<User> alleUsers = _UsersDB.users.ToList();
            return alleUsers;
        }
    }

}