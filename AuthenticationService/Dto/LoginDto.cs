namespace AuthenticationService.Dto
{
    /// <summary>
    /// object to hold user credentials
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// for validating. login. and other purposes
        /// </summary>
        public required string Username { get; set; }

        /// <summary>
        /// for validating. login. and other purposes
        /// </summary>
        public required string Password { get; set; }
    }
}
