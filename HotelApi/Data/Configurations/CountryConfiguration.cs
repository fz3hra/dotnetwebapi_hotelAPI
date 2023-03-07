using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelApi.Data.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            // set up configuration with tables and data types.
            builder.HasData(
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
        }
    }
}