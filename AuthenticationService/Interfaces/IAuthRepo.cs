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
        public LoginBe GetUserByToken(string token);

        /// <summary>
        /// retrieves a user by their username to validate their password
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>        
        public LoginBe GetUsersByUsername(string username);

        /// <summary>
        /// adds a token to the database
        /// </summary>
        /// <param name="token"></param>
        public void AddTokenToLogin(TokenBe token);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public LoginBe AddLogin(LoginBe login);

        public void Rebuild();
    }
}
