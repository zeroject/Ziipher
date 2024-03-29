﻿using AuthenticationService.BE;
using AuthenticationService.Dto;

namespace AuthenticationService.Interfaces
{
    public interface IAuthValidationService
    {
        /// <summary>
        /// validates a user by their username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>bool True if user is valid, false if user is not</returns>
        public bool ValidateUserByCredentials(string username, string password);

        /// <summary>
        /// validates a user by their token
        /// </summary>
        public bool ValidateUserByToken(string token);

        /// <summary>
        /// sets a token for a user in the database and returns the token
        /// </summary>
        public string LoginUser(LoginDto loginDto);

        /// <summary>
        /// registers a new user in the database
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        public LoginDto RegisterNewLogin(LoginDto loginDto);

        /// <summary>
        /// rebuilds the database
        /// </summary>
        public void Rebuild();


    }
}
