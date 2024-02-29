using AuthenticationService.BE;
using AuthenticationService.Interfaces;
using AuthenticationService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Repos
{
    public class AuthRepo : IAuthRepo
    {
        private readonly AuthContext _context;
        private readonly ILogger<AuthRepo> _logger;

        public AuthRepo(AuthContext context, ILogger<AuthRepo> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <exception cref="Exception"></exception>
        public void AddTokenToLogin(Token token)
        {
            try
            {
                _context.tokens.Add(token);
                _context.SaveChanges();
            } catch (Exception e)
            {
                throw new Exception("Error adding token to login: " + e.Message);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Login GetUserByToken(string token)
        {
            try
            {
                return _context.logins
                    .Include(e => e.Tokens)
                    .FirstOrDefault(e => e.Tokens.Any(t => t.JwtToken == token)) ?? throw new Exception("User not found");
            } catch (Exception e)
            {
                throw new Exception("Error getting user by token: " + e.Message);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Login GetUsersByUsername(string username)
        {
            try
            {
                return _context.logins.FirstOrDefault(e => e.Username == username) ?? throw new Exception("User not found");
            } catch (Exception e)
            {
                throw new Exception("Error getting user by username: " + e.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public Login AddLogin(Login login)
        {
            try
            {
                _logger.LogInformation($"{login.Id} : {login.Username} : {login.Password}");
                _context.logins.Add(login);
                _context.SaveChanges();
                return login;
            } catch (Exception e)
            {
                throw new Exception("Error adding login: " + e.Message);
            }
        }


        /// <summary>
        /// to be called when the database needs to be rebuilt or initialized
        /// </summary>
        public void Rebuild()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
        }
    }
}
