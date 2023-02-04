using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelApi.Data;

namespace HotelApi.Contracts
{
    public interface IHotelsRepository : IGenericRepository<Hotel>
    {
    }
}