﻿using stock_market.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace stock_market.DAL
{
    public interface IUserRepository
    {
        Task<string> GetFullName(int userid);
        Task<string> GetFName(int userid);
        Task<string> GetLName(int userid);
        Task<int> GetUserID();
        Task<bool> CreateUser(User innUser);
        Task<List<User>> GetAll(int userid);
        Task<bool> DeleteUser(int id);
        Task<bool> EditUser(User editUser);
        Task<bool> EditUserBalance(User editUser);
        Task<bool> LogIn(LoginUser user);
        Task<bool> Register(RegisterUser user);
        Task<int> GetUserIDForUsername(string username);
    }
}
