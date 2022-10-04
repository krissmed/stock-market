using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace stock-market.Controllers
{
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly UserContext _db;

        public UserController(UserContext db)
        {
            _db = db;
        }

        public bool Lagre(User innUser)
        {
            try
            {
                _db.Users.Add(innUser)
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }



}