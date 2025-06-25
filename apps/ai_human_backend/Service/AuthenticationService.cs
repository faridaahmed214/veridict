using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomanLayer.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DTO.Auth;

namespace Service
{
    public class AuthenticationService(UserManager<IdentityUser> _userManager,IConfiguration _configuration, IMapper _mapper) : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email)??throw new UserNotFoundException(loginDto.Email);
            var IsPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (IsPasswordValid)
            {
                return new UserDto()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = await CreateTokenAsync(user)
                };
            }
            else {
                throw new UnAthorizedException();
            }
        }

       
        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
           
            var user = new IdentityUser()
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (result.Succeeded)
            {
                return new UserDto()
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = await CreateTokenAsync(user)
                };
            }
            else
            {
                var Errors = result.Errors.Select(E=>E.Description).ToList();
                throw new BadRequestdException(Errors);
            }

        }



        private  async Task<string> CreateTokenAsync(IdentityUser user)
        {
            var Claims = new List<Claim>()
            {
                new(type: ClaimTypes.Email ,  user.Email!),
                new(type: ClaimTypes.Name ,  user.UserName!),
                new(type: ClaimTypes.NameIdentifier , user.Id!)
            };
            var Roles = await _userManager.GetRolesAsync(user);

            foreach (var role in Roles)
                Claims.Add(item: new Claim(type: ClaimTypes.Role, value: role));
            var Secretkey = _configuration.GetSection("JWTOptions")["Secretkey"];
            var Key = new SymmetricSecurityKey(key: Encoding.UTF8.GetBytes(Secretkey));
            var Creds = new SigningCredentials(key: Key, algorithm: SecurityAlgorithms.HmacSha256);

            var Token = new JwtSecurityToken(
                issuer: _configuration["JWTOptions:Issuer"],
                audience: _configuration["JWTOptions:Audience"],
                claims: Claims,
                expires: DateTime.Now.AddHours(value: 1),
                signingCredentials: Creds);
            return new JwtSecurityTokenHandler().WriteToken(token: Token);
        }

    }
}
