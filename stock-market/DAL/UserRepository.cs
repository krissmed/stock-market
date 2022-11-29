using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.EntityFrameworkCore;
using stock_market.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace stock_market.DAL
{
    [ExcludeFromCodeCoverage]
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

        public static byte[] MakeHash(string password, byte[] salt)
        {
            return KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 1000,
                numBytesRequested: 32
                );
        }

        public static byte[] MakeSalt()
        {
            var csp = new RNGCryptoServiceProvider();
            var salt = new byte[32];
            csp.GetBytes(salt);
            return salt;
        }


        public async Task<bool> LogIn(LoginUser user)
        {
            try
            {
                User foundUser = await _db.Users.FirstOrDefaultAsync(u => u.username == user.username);

                byte[] hash = MakeHash(user.password, foundUser.salt);

                Console.WriteLine("Password: " + user.password);

                bool ok = hash.SequenceEqual(foundUser.password);
                if (ok)
                {
                    Console.WriteLine("like");
                    return true;
                }
                Console.WriteLine("Ikke like");
                return false;
            }
            catch (Exception e)
            {
                return false;
            }    
        }
        
        public async Task<bool> Register(RegisterUser user)
        {
            try
            {
                User foundUser = await _db.Users.FirstOrDefaultAsync(u => u.username == user.username);
                if (foundUser != null)
                {
                    return false;
                }
                byte[] salt = MakeSalt();
                byte[] hash = MakeHash(user.password, salt);
                User newUser = new User
                {
                    username = user.username,
                    first_name = user.firstname,
                    last_name = user.lastname,
                    password = hash,
                    salt = salt,
                    curr_balance = 100_000,
                    curr_balance_liquid = 100_000,
                    curr_balance_stock = 0,
                };
                await _db.Users.AddAsync(newUser);
                await _db.SaveChangesAsync();
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }
        
        public async Task<int> GetUserIDForUsername(string username)
        {
            User user = await _db.Users.FirstOrDefaultAsync(u => u.username == username);
            return user.id;
        }
        



        public async Task<List<User>> GetAll(int userid)
        {
            return await _db.Users.Where(u => u.id == userid).ToListAsync();
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

