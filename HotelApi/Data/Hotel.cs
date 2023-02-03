using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApi.Data
{
    // hotel is an entity
    public class Hotel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }

        // foreign key
        [ForeignKey(nameof(CountryId))]
        public int CountryId { get; set; }
        // one hotel can have one country
        public Country country { get; set; }
    }
}