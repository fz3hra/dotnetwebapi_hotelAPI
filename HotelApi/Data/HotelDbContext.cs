using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelApi.Data.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.Data
{
    public class HotelDbContext : IdentityDbContext<ApiUser>
    {
        public HotelDbContext(DbContextOptions options) : base(options)
        {
            // tables and table configuration
            // contract between app and database
        }
        public DbSet<Hotel> hotels { get; set; }
        public DbSet<Country> countries { get; set; }

        // giving some data already found in database :: seeding database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            // when entity framework is building the model: i want some default data:
            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new HotelConfiguration());
        }
    }
}