using AuthenticationService.Dto;
using AuthenticationService.Interfaces;
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
        private IAuthValidationService _auth;

        /// <summary>
        /// Constructor for AuthController
        /// </summary>
        public AuthController(IAuthValidationService auth)
        {
            _auth = auth;
        }

        /// <summary>
        /// takes in user credentials and issues a token for later validation
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("loginUser")]
        public IActionResult LoginUser(LoginDto loginDto)
        {
            try
            {
                return Ok(_auth.loginUser(loginDto));
            } catch
            {
                return BadRequest("Invalid credentials");
            }
        }

        /// <summary>
        /// method to be called to validate the user's token
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("validateUser")]
        public IActionResult validateUser(string token)
        {
            try
            {
                return Ok(_auth.ValidateUserByToken(token));
            } catch
            {
                return StatusCode(401, "Unauthorized");
            }
        }


        /// <summary>
        /// when validation by token fails, call this method to validate by credentials
        /// </summary>
        [HttpPost]
        [Route("validateUserByCredentials")]
        public IActionResult validateUserByCredentials(string username, string password)
        {
            try
            {
                return Ok(_auth.ValidateUserByCredentials(username, password));
            } catch
            {
                return StatusCode(401, "Unauthorized");
            }
        }

        [HttpPost]
        [Route("registerNewLogin")]
        public IActionResult registerNewLogin(LoginDto loginDto)
        {
            return Ok(_auth.registerNewLogin(loginDto));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("rebuild")]
        public IActionResult rebuild()
        {
            try
            {
                _auth.Rebuild();
                return Ok();
            } catch
            {
                return BadRequest("Error rebuilding database");
            }
        }

    }
}
