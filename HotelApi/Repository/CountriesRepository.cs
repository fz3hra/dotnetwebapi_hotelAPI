using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelApi.Contracts;
using HotelApi.Data;
using Microsoft.EntityFrameworkCore;

namespace HotelApi.Repository
{
    public class CountriesRepository : GenericRepository<Country>, ICountriesRepository
    {
        private readonly HotelDbContext _context;

        public CountriesRepository(HotelDbContext context) : base(context)
        {
            this._context = context;
        }

        public async Task<Country> GetDetails(int id)
        {
            return await _context.countries.Include(q => q.hotels).FirstOrDefaultAsync(q => q.Id == id);
        }
    }
}