using Microsoft.EntityFrameworkCore;

namespace HotelApi.Data
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string shortName { get; set; }
        // one to many => one country can have many hotels
        // association:
        public virtual IList<Hotel> hotels { get; set; }


    }
}