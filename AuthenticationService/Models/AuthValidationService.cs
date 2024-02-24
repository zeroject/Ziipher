﻿using AuthenticationService.BE;
using AuthenticationService.Dto;
using AuthenticationService.helpers;
using AuthenticationService.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationService.Models
{
    public class AuthValidationService : IAuthValidationService
    {
        private double expiriationTime;
        
        private readonly AppSettings _appSettings;
        private IAuthRepo _authRepo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appSettings"></param>
        /// <param name="authRepo"></param>
        public AuthValidationService(AppSettings appSettings, IAuthRepo authRepo)
        {
            _appSettings = appSettings;
            _authRepo = authRepo;
            expiriationTime = 6;
        }
        
        //TODO refactor for cleaner code
        public string loginUser(LoginDto loginDto)
        {
            bool isValid = validate(loginDto.Username, loginDto.Password);
            if (isValid)
            {
                var token = GenerateJSONWebToken(loginDto);
                LoginBe login = _authRepo.getUsersByUsername(loginDto.Username);
                _authRepo.addTokenToLogin(new TokenBe 
                { 
                    Token = token,
                    UserId = login.Id
                });
                return token;
            }
            throw new Exception("Invalid credentials");
        }

        public bool ValidateUserByCredentials(string username, string password)
        {
            return validate(username, password);
        }

        public bool ValidateUserByToken(string token)
        {
            return _authRepo.getUserByToken(token) != null;
        }

        private string GenerateJSONWebToken(LoginDto userInfo)
        {
            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("username", userInfo.Username) }),
                Expires = DateTime.UtcNow.AddHours(expiriationTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

        private bool validate(string username, string password)
        {
            LoginBe login = _authRepo.getUsersByUsername(username);
            return login.Password == password; 
        }
    }
}
