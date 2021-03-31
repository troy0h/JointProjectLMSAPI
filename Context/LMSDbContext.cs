using JointProjectLMSAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace JointProjectLMSAPI.Context
{
    public class LMSDbContext : DbContext
    {
        public LMSDbContext(DbContextOptions<LMSDbContext> options) : base()
        {}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
            optionsBuilder.UseSqlServer(connString);
        }

        public DbSet<User> Users { get; set; }
    }
}
