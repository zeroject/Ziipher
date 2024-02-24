using AuthenticationService.BE;

namespace AuthenticationService.Interfaces
{
    ///
    public interface IAuthRepo
    {
        /// <summary>
        /// retrieves a user by their token
        /// </summary>
        /// <returns></returns>
        public LoginBe getUserByToken(string token);

        /// <summary>
        /// retrieves a user by their username to validate their password
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>        
        public LoginBe getUsersByUsername(string username);

        /// <summary>
        /// adds a token to the database
        /// </summary>
        /// <param name="token"></param>
        public void addTokenToLogin(TokenBe token);

        public void addLogin(LoginBe login);
    }
}
