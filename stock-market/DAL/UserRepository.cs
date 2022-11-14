using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using stock_market.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace stock_market.DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly mainDB _db;

        public UserRepository(mainDB db)
        {
            _db = db;
        }


        public async Task<string> GetFullName(int userid)
        {
            User user = await _db.Users.FindAsync(userid);
            return user.first_name + " " + user.last_name;
        }

        public async Task<string> GetFName(int userid)
        {
            User user = await _db.Users.FindAsync(userid);
            return user.first_name;
        }

        public async Task<string> GetLName(int userid)
        {
            User user = await _db.Users.FindAsync(userid);
            return user.last_name;
        }

        public async Task<int> GetUserID()
        {
            //For now returns 1 as there is only 1 user in the system - John Doe.
            return 1;
            //Mulig l�sning: User user = _db.Users.Find(id); return user ??

        }

        public async Task<bool> CreateUser(User innUser)
        {
            
                try
                {
                    // Front-End m� benytte seg av Post for � sende inn data, denne tar da og lagrer det mot DB.

                    await _db.Users.AddAsync(innUser);
                    await _db.SaveChangesAsync();
                    return true;
                }
                catch { return false; }
           
            
        }

        public async Task<List<User>> GetAll()
        {
            List<User> allUsers = await _db.Users.ToListAsync();
            return allUsers;
        }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                User user = await _db.Users.FindAsync(id);
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
                return true;
            }
            catch { return false; }

        }

        public async Task<bool> EditUser(User editUser) //Edits User, get current vaulues with GET and send new ones with POST. This is meant for the user to be able to change his own name.
        {
            try
            {
                var edit = await _db.Users.FindAsync(editUser.id);
                edit.first_name = editUser.first_name;
                edit.last_name = editUser.last_name;
                await _db.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<bool> EditUserBalance(User editUser) //Edits balance of user, get current calues with GET and send new ones with POST. This is meant for when user sell/buy stock
        {
            try
            {
                var edit = await _db.Users.FindAsync(editUser.id);
                edit.curr_balance = editUser.curr_balance;
                edit.curr_balance_liquid = editUser.curr_balance_liquid;
                edit.curr_balance_stock = editUser.curr_balance_stock;
                await _db.SaveChangesAsync();
                return true;
            }
            catch { return false; }
        }
    }
}

