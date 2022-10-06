using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace stock_market.Model
{
    public class UsersDB : DbContext
    {
        public UsersDB (DbContextOptions<UsersDB> options) : base(options)
        {
            Database.EnsureCreated();
        }

        
        public DbSet<User> users { get; set; }
    }
}