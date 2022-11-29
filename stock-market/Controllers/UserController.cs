using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        private const string _loggetInn = "loggetInn";
        private readonly IUserRepository _db;
        private readonly ILogger<UserController> _log;

        public UserController(IUserRepository db, ILogger<UserController> log)
        {
            _db = db;
            _log = log;
        }


        public async Task<ActionResult> GetFullName()
        {
            //check if user is logged in by checking HttpContext.Session and checking if its -1 or null
            if (HttpContext.Session.GetInt32(_loggetInn) == null || HttpContext.Session.GetInt32(_loggetInn) == -1)
            {
                _log.LogError("UserController: User is not logged in");
                return Unauthorized("User is not logged in");
            }

            int userid = HttpContext.Session.GetInt32(_loggetInn).Value;

            _log.LogInformation("UserController: Logged in userid " + userid + " got Fullname");
            string fullname = await _db.GetFullName(userid);
            return Ok(fullname);
        }

        public async Task<ActionResult> GetFName()
        {
            if (HttpContext.Session.GetInt32(_loggetInn) == null || HttpContext.Session.GetInt32(_loggetInn) == -1)
            {
                _log.LogError("UserController: User is not logged in");
                return Unauthorized("User is not logged in");
            }
            int userid = HttpContext.Session.GetInt32(_loggetInn).Value;
            _log.LogInformation("UserController: Logged in userid " + userid + " got firstname");

            string fname = await _db.GetFName(userid);
            return Ok(fname);
        }

        public async Task<ActionResult> GetLName()
        {
            if (HttpContext.Session.GetInt32(_loggetInn) == null || HttpContext.Session.GetInt32(_loggetInn) == -1)
            {
                _log.LogError("UserController: User is not logged in");
                return Unauthorized("User is not logged in");
            }
            int userid = HttpContext.Session.GetInt32(_loggetInn).Value;
            _log.LogInformation("UserController: Logged in userid " + userid + " got lastname");

            string lname = await _db.GetLName(userid);

            return Ok(lname);
        }

        public async Task<ActionResult> GetUserID()
        {
            if (HttpContext.Session.GetInt32(_loggetInn) == null || HttpContext.Session.GetInt32(_loggetInn) == -1)
            {
                _log.LogError("UserController: User is not logged in");
                return Unauthorized("User is not logged in");
            }
            int userid = HttpContext.Session.GetInt32(_loggetInn).Value;
            _log.LogInformation("UserController: Logged in userid " + userid + " got userid");
            return Ok(userid);
        }

        public async Task<ActionResult> CreateUser(User innUser)
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


        public async Task<ActionResult> DeleteUser()
        {
            if (HttpContext.Session.GetInt32(_loggetInn) == null || HttpContext.Session.GetInt32(_loggetInn) == -1)
            {
                _log.LogError("UserController: User is not logged in");
                return Unauthorized("User is not logged in");
            }
            int userid = HttpContext.Session.GetInt32(_loggetInn).Value;

            bool ok = await _db.DeleteUser(userid);
            if (!ok)
            {
                _log.LogError("UserController: Could not delete user");
                return BadRequest("Could not delete user");
            }
            _log.LogInformation("UserController: User deleted user");
            return Ok("User deleted");
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

        public async Task<ActionResult> GetAll()
        {
            {
                if (HttpContext.Session.GetInt32(_loggetInn) == null || HttpContext.Session.GetInt32(_loggetInn) == -1)
                {
                    _log.LogError("UserController: User is not logged in");
                    return Unauthorized("User is not logged in");
                }
                int userid = HttpContext.Session.GetInt32(_loggetInn).Value;

                List<User> users = await _db.GetAll(userid);
                if (users == null)
                {
                    _log.LogError("UserController: Could not get all users");
                    return BadRequest("Could not get user");
                }
                _log.LogInformation("UserController: User got all users");
                return Ok(users);
            }
        }
        
        public async Task<ActionResult> LogIn([FromBody] LoginUser user)
        {
            if (ModelState.IsValid)
            {
                bool returnOK = await _db.LogIn(user);
                if (!returnOK)
                {
                    HttpContext.Session.SetInt32(_loggetInn, -1);
                    return Unauthorized("Feil brukernavn eller passord");
                }
                int id = await _db.GetUserIDForUsername(user.username);
                string firstname = await _db.GetFName(id);
                HttpContext.Session.SetInt32(_loggetInn, id);
                return Ok(firstname);
            }
            HttpContext.Session.SetInt32(_loggetInn, -1);
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

        public void LogOut()
        {
            HttpContext.Session.SetInt32(_loggetInn, -1);
        }
    }
}

