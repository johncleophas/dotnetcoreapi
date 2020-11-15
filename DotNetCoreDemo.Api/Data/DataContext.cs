using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotNetCoreDemo.Api.Data
{
    public class DataContext : DbContext
    {
        protected readonly AppSettings appSettings;

        public DataContext(AppSettings appSettings)
        {
            this.appSettings = appSettings;
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
               => options.UseSqlite($"Data Source={this.appSettings.DatabaseLocation}dotnetcoredemo.db");

    }
}
