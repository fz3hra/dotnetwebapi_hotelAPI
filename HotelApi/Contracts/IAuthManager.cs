using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelApi.ModelDtos.userDtos;
using Microsoft.AspNetCore.Identity;

namespace HotelApi.Contracts
{
    public interface IAuthManager
    {
        Task<IEnumerable<IdentityError>> Register(ApiUserDto apiUserDto);
        Task<AuthResponseDto> Login(LoginDto apiUserDto);
    }
}