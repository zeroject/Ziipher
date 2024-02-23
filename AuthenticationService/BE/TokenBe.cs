namespace AuthenticationService.BE
{
    /// <summary>
    /// EF Core object to hold token data linked to a user
    /// </summary>
    public class TokenBe
    {
        /// <summary>
        /// primary key for the token table
        /// </summary>
        public int Id { get; set; }
                      
        /// <summary>
        /// the token string
        /// </summary>
        public required string Token { get; set; }
           
        /// <summary>
        /// the user id linked to the token
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// time the token was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// constructor for TokenBe to set the time the token was created
        /// </summary>
        public TokenBe()
        {
            CreatedAt = DateTime.Now;
        }

        /// <summary>
        /// ef magic to link the token to the user
        /// </summary> 
        public LoginBe User { get; set; }
    }
}
