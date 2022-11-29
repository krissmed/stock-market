using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using stock_market.DAL;
using stock_market.Model;

namespace stock_market.Controllers
{
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _db;
        private readonly ILogger<UserController> _log;

        public UserController(IUserRepository db, ILogger<UserController> log)
        {
            _db = db;
            _log = log;
        }


        public async Task<string> GetFullName(int userid)
        {
            _log.LogInformation("UserController: User got Fullname");
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

        public async Task<ActionResult> CreateUser (User innUser)
        {
            if (ModelState.IsValid)
            {
                bool ok = await _db.CreateUser(innUser);
                if (!ok)
                {
                    _log.LogError("UserController: Could not create user");
                    return BadRequest("Could not create user");
                }
                _log.LogInformation("UserController: User created user");
                return Ok("User created user");
            }
            _log.LogError("UserController: Fault in InputVal ");
            return BadRequest("Fault in InputVal");
        }

        public async Task<List<User>> GetAll()
        {
            return await _db.GetAll();
        }

        public async Task<ActionResult> DeleteUser(int id)
        {
            if (ModelState.IsValid)
            {
                bool ok = await _db.DeleteUser(id);
                if (!ok)
                {
                    _log.LogError("UserController: Could not delete user");
                    return BadRequest("Could not delete user");
                }
                _log.LogInformation("UserController: User deleted user");
                return Ok("User deleted user");
            }
            _log.LogError("UserController: Fault in InputVal ");
            return BadRequest("Fault in InputVal");
        }

        public async Task<ActionResult> EditUser(User editUser) //Edits User, get current vaulues with GET and send new ones with POST. This is meant for the user to be able to change his own name.
        {

            if (ModelState.IsValid)
            {
                bool ok = await _db.EditUser(editUser);
                if (!ok)
                {
                    _log.LogError("UserController: Could not edit user");
                    return BadRequest("Could not edit user");
                }
                _log.LogInformation("UserController: User edit user");
                return Ok("User edit user");
            }
            _log.LogError("UserController: Fault in InputVal ");
            return BadRequest("Fault in InputVal");
        }



        public async Task<ActionResult> LogIn([FromBody] LoginUser user)
        {
            if (ModelState.IsValid)
            {
                bool returnOK = await _db.LogIn(user);
                if (!returnOK)
                {
                    return Unauthorized("Feil brukernavn eller passord");
                }
                return Ok(true);
            }
            return BadRequest("Feil i inputvalidering på server");
        }
        public async Task<ActionResult> EditUserBalance(User editUser) //Edits balance of user, get current calues with GET and send new ones with POST. This is meant for when user sell/buy stock
        {
            if (ModelState.IsValid)
            {
                bool ok = await _db.EditUserBalance(editUser);
                if (!ok)
                {
                    _log.LogError("UserController: Could not edit userbal");
                    return BadRequest("Could not edit userbal");
                }
                _log.LogInformation("UserController: User edited userbal");
                return Ok("User edited userbal");
            }
            _log.LogError("UserController: Fault in InputVal ");
            return BadRequest("Fault in InputVal");
        }

        public async Task<ActionResult> Register([FromBody] RegisterUser user)
        {
                if (ModelState.IsValid)
                {
                    bool returnOK = await _db.Register(user);
                    if (!returnOK)
                    {
                        return Conflict("User already exists");
                    }
                    return Ok(true);
                }
                return BadRequest("Feil i inputvalidering på server");
            }
        }
    }

