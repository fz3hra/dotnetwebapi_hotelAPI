using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelApi.Data.Configurations
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            // set up configuration with tables and data types.
            builder.HasData(
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