namespace AuthenticationService.BE
{
    /// <summary>
    /// for validating. login. and EF purposes
    /// </summary>
    public class LoginBe
    {
        /// <summary>
        /// primary key for the user table
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// identifier for the user
        /// </summary>
        required public string Username { get; set; }

        /// <summary>
        /// 
        /// </summary>
        required public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<TokenBe>? Tokens { get; set; }
    }
}
