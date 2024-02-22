using AuthenticationService.Dto;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    /// <summary>
    /// AuthController for authentication and authorization
    /// </summary>

    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Constructor for AuthController
        /// </summary>
        public AuthController()
        {

        }

        /// <summary>
        /// takes in user credentials and issues a token for later validation
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("loginUser")]
        public IActionResult LoginUser(LoginDto loginDto)
        {
            return Ok("User logged in successfully");
        }

        /// <summary>
        /// removes the token from the active tokens pool
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("logoutUser")]
        public IActionResult LogoutUser(string token)
        {
            return Ok("User logged out successfully");
        }

        /// <summary>
        /// method to be called to validate the user's token
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("validateUser")]
        public IActionResult validateUser(string token)
        {
            return token != null ? Ok("User is valid") : Ok("User is not valid");
        }

    }
}
