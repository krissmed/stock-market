using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using stock_market.DAL;
using stock_market.Model;

namespace stock_market.Controllers
{
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _db;

        public UserController(IUserRepository db)
        {
            _db = db;
        }


        public async Task<string> GetFullName(int userid)
        {
            return await _db.GetFullName(userid);
        }

        public async Task<string> GetFName(int userid)
        {
            return await _db.GetFName(userid);
        }

        public async Task<string> GetLName(int userid)
        {
            return await _db.GetLName(userid);
        }

        public async Task<int> GetUserID()
        {
            return await _db.GetUserID();

        }

        public async Task<bool> CreateUser (User innUser)
        {
            return await _db.CreateUser(innUser);
        }

        public async Task<List<User>> GetAll()
        {
            return await _db.GetAll();
        }

        public async Task<bool> DeleteUser(int id)
        {
            return await _db.DeleteUser(id);
        }

        public async Task<bool> EditUser(User editUser) //Edits User, get current vaulues with GET and send new ones with POST. This is meant for the user to be able to change his own name.
        {
            return await _db.EditUser(editUser);
        }

        public async Task<bool> EditUserBalance(User editUser) //Edits balance of user, get current calues with GET and send new ones with POST. This is meant for when user sell/buy stock
        {
            return await _db.EditUserBalance(editUser);
        }

        public async Task<ActionResult> LogIn(LoginUser user)
        {
            if (ModelState.IsValid)
            {
                bool returnOK = await _db.LogIn(user);
                if (!returnOK)
                {
                    return Ok(false);
                }
                return Ok(true);
            }
            return BadRequest("Feil i inputvalidering på server");
        }

        public async Task<ActionResult> Register(RegisterUser user)
        {
                if (ModelState.IsValid)
                {
                    bool returnOK = await _db.Register(user);
                    if (!returnOK)
                    {
                        return Ok(false);
                    }
                    return Ok(true);
                }
                return BadRequest("Feil i inputvalidering på server");
            }
        
    }

}
