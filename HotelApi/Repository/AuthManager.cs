using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HotelApi.Contracts;
using HotelApi.Data;
using HotelApi.ModelDtos.userDtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace HotelApi.Repository
{
    public class AuthManager : IAuthManager
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManagerLibrary;
        private readonly IConfiguration _configuration;

        public AuthManager(IMapper mapper, UserManager<ApiUser> userManagerLibrary, IConfiguration configuration)
        {
            this._mapper = mapper;
            this._userManagerLibrary = userManagerLibrary;
            this._configuration = configuration;
        }

        public async Task<AuthResponseDto> Login(LoginDto loginDto)
        {
            bool isValidUser = false;
            var user = await _userManagerLibrary.FindByEmailAsync(loginDto.Email);
            isValidUser = await _userManagerLibrary.CheckPasswordAsync(user, loginDto.Password);
            if (user == null || isValidUser == false)
            {
                return null;
            }
            var token = await GenerateToken(user);
            return new AuthResponseDto
            {
                Token = token,
                UserId = user.Id
            };
        }

        public async Task<IEnumerable<IdentityError>> Register(ApiUserDto apiUserDto)
        {
            var user = _mapper.Map<ApiUser>(apiUserDto);
            user.UserName = apiUserDto.Email;
            // automatically hash and add salt:
            var result = await _userManagerLibrary.CreateAsync(user, apiUserDto.Password);
            if (result.Succeeded)
            {
                await _userManagerLibrary.AddToRoleAsync(user, "User");
            }
            return result.Errors;
        }

        private async Task<string> GenerateToken(ApiUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await _userManagerLibrary.GetRolesAsync(user);

            var roleClaims = roles.Select(x => new Claim(ClaimTypes.Role, x)).ToList();

            var userClaims = await _userManagerLibrary.GetClaimsAsync(user);

            // generate claims for token
            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            }.Union(userClaims).Union(roleClaims);

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(_configuration["JwtSettings:DurationInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}