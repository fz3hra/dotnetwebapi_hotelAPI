using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.Data
{
    public class HotelDbContext : DbContext
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
            // when entity framework is building the model: i want some default data:
            modelBuilder.Entity<Country>().HasData(
                new Country
                {
                    Id = 1,
                    Name = "Jamaica",
                    shortName = "JM"
                },
                new Country
                {
                    Id = 2,
                    Name = "Bahamas",
                    shortName = "BS"
                }
            );
            modelBuilder.Entity<Hotel>().HasData(
                new Hotel
                {
                    Id = 1,
                    Name = "Sandals Resort and Spa",
                    Address = "Negril",
                    CountryId = 1,
                    Rating = 4.5
                },
                new Hotel
                {
                    Id = 2,
                    Name = "Comfort Suites",
                    Address = "Geroge Town",
                    CountryId = 2,
                    Rating = 4.3
                }
            );
        }
    }
}