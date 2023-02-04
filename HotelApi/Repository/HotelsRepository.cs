using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelApi.Contracts;
using HotelApi.Data;

namespace HotelApi.Repository
{
    public class HotelsRepository : GenericRepository<Hotel>, IHotelsRepository
    {
        private readonly HotelDbContext _context;

        public HotelsRepository(HotelDbContext context) : base(context)
        {
            this._context = context;
        }
    }
}