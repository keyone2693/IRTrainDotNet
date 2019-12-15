using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using TestExample.Data.Models;

namespace MadPay724.Data.DatabaseContext
{
    public class MainDbContext : DbContext
    {
        public MainDbContext()
        {
        }
        public MainDbContext(DbContextOptions<MainDbContext> opt) : base(opt)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionBuilder)
        {
            optionBuilder.UseSqlite("Filename=MainDatabase.db");
        }

        public DbSet<AuthToken> AuthTokens { get; set; }
    }
}
