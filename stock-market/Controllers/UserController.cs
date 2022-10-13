using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
            //Mulig løsning: User user = _db.Users.Find(id); return user ??
        }

        public bool CreateUser (User innUser)
        {
            try
            {
                // Front-End må benytte seg av Post for å sende inn data, denne tar da og lagrer det mot DB.
                _db.Users.Add(innUser);
                _db.SaveChanges();
                return true;
            } catch { return false; }
        }

        public bool DeleteUser(int id)
        {
            try
            {
                User user = _db.Users.Find(id);
                _db.Users.Remove(user);
                _db.SaveChanges();
                return true;
            } catch { return false; }
            
        }

        public bool EditUser(User editUser) //Edits User, get current vaulues with GET and send new ones with POST. This is meant for the user to be able to change his own name.
        {
            try
            {
                User user = _db.Users.Find(editUser.id);
                user.first_name=editUser.first_name;
                user.last_name=editUser.last_name;
                _db.SaveChanges();
                return true;
            } catch { return false; }
        }

        public bool EditUserBalance(User editUser) //Edits balance of user, get current calues with GET and send new ones with POST. This is meant for when user sell/buy stock
        {
            try
            {
                User user = _db.Users.Find(editUser.id);
                user.curr_balance =editUser.curr_balance;
                user.curr_balance_liquid = editUser.curr_balance_liquid;
                user.curr_balance_stock = editUser.curr_balance_stock;
                _db.SaveChanges();
                return true;
            } catch { return false; }
        }
    }

}