using AuthenticationService.BE;
using AuthenticationService.Interfaces;
using AuthenticationService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationService.Repos
{
    public class AuthRepo : IAuthRepo
    {
        private readonly AuthContext _context;

        public AuthRepo(AuthContext context)
        {
            _context = context;
        }
        public void addTokenToLogin(TokenBe token)
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
        public LoginBe getUserByToken(string token)
        {
            try
            {
                return _context.logins
                    .Include(e => e.Tokens)
                    .FirstOrDefault(e => e.Tokens.Any(t => t.Token == token)) ?? throw new Exception("User not found");
            } catch (Exception e)
            {
                throw new Exception("Error getting user by token: " + e.Message);
            }
        }

        public LoginBe getUsersByUsername(string username)
        {
            try
            {
                return _context.logins.FirstOrDefault(e => e.Username == username) ?? throw new Exception("User not found");
            } catch (Exception e)
            {
                throw new Exception("Error getting user by username: " + e.Message);
            }
        }

        public LoginBe addLogin(LoginBe login)
        {
            try
            {
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
