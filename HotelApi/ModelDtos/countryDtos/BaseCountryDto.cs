using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApi.ModelDtos.countryDtos
{
    public abstract class BaseCountryDto
    {
        [Required]
        public string Name { get; set; }
        public string ShortName { get; set; }
    }
}