using System;
using System.Linq;
using MonitoringStations.DB.Context;
using MonitoringStations.Domain.Models;
using MonitoringStations.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using MonitoringStations.Domain.Dto;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace MonitoringStations.Core.Services
{
    public class ApiTokenService : IApiTokenService
    {
        private readonly DataContext _context;
        private readonly IPasswordHasher<IdentityUser> _passwordHasher;
        private readonly AuthenticationJwt _authenticationJwt;

        public ApiTokenService(DataContext context, AuthenticationJwt authenticationJwt)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<IdentityUser>();
            _authenticationJwt = authenticationJwt;
        }

        public string GenerateJwt(LoginDto loginDto)
        {
            var result = string.Empty;
            var user = _context.Users.FirstOrDefault(x => x.Email == loginDto.Email);

            if (user is null) throw new Exception("Invalid user name or password !");

            var resultPass = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);
            if(resultPass == PasswordVerificationResult.Failed) throw new Exception("Invalid user name or password !");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationJwt.JwtKey));
            var cerd = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(_authenticationJwt.JwtExpireMinutes);

            var token = new JwtSecurityToken(_authenticationJwt.JwtIssuer, expires: expires, signingCredentials: cerd);
            var tokenHandler = new JwtSecurityTokenHandler();
            result = tokenHandler.WriteToken(token);

            return result;
        }

        public bool IsTokenExist(string token)
        {
            var result = false;

            if (string.IsNullOrEmpty(token)) return result;

            result = _context.ApiToken.Any(x => x.Token.Equals(token));

            return result;
        }

        public void SaveApiToken(ApiToken token)
        {
            try
            {
                if (token != null)
                {
                    _context.ApiToken.Add(token);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
