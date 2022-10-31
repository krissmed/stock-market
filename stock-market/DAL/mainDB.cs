using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using stock_market.Model;

namespace stock_market.DAL
{
    public class mainDB : DbContext
    {
        public mainDB(DbContextOptions<mainDB> options) : base(options)
        {
            Database.EnsureCreated();
            ChangeTracker.LazyLoadingEnabled = false;
        }

        //public bool readable { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> transactions { get; set; }
        public DbSet<HistoricalStock> historicalStocks { get; set; }
        public DbSet<Timestamp> timestamps { get; set; }
        public DbSet<BaseStock> baseStocks { get; set; }
        public DbSet<Portfolio> portfolios { get; set; }
        public DbSet<BaseStockCounter> stockCounter { get; set; }
        public DbSet<Watchlist> watchlist { get; set; }
        public DbSet<WatchlistStock> wls { get; set; }


    }
}

