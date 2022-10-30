using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace stock_market.Model
{


    public class mainDB : DbContext
    {
        public mainDB (DbContextOptions<mainDB> options) : base(options)
        {
            Database.EnsureCreated();
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Transaction> transactions { get; set; }
        public DbSet<HistoricalStock> historicalStocks { get; set; }
        public DbSet<Timestamp> timestamps { get; set; }
        public DbSet<BaseStock> baseStocks { get; set; }
        public DbSet<Portfolio> portfolios { get; set; }
        public DbSet<BaseStockCounter> stockCounter { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }


    }
}

