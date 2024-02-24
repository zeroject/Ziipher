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
        public Login GetUserByToken(string token);

        /// <summary>
        /// retrieves a user by their username to validate their password
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>        
        public Login GetUsersByUsername(string username);

        /// <summary>
        /// adds a token to the database
        /// </summary>
        /// <param name="token"></param>
        public void AddTokenToLogin(Token token);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public Login AddLogin(Login login);

        public void Rebuild();
    }
}
