using Domain;
using Domain.DTO_s;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UserApplication;

namespace UserService.Controllers
{
    /// <summary>
    /// Controller for user management
    /// </summary>
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserCrud _userCrud;
        private readonly ILogger<UserController> _logger;
        public UserController(IUserCrud userCrud, ILogger<UserController> logger)
        {
            _userCrud = userCrud;
            _logger = logger;
        }

        /// <summary>
        /// Adds a user to the system
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUser")]
        public async Task<IActionResult> AddUserAsync(UserDTO user)
        {
            try
            {
                int userID = _userCrud.AddUser(user);
                //TODO: Add user to AuthWanabe
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5000");
                    var response = await client.PostAsJsonAsync("/AddUser", userID);
                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogError("Error adding user to AuthWanabe");
                        return StatusCode(500, "An internal Error has occurred");
                    }
                }
                _logger.LogInformation("User added to the system");
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An internal error has occurred");
                return StatusCode(500, "An internal Error has occurred");
            }
        }

        /// <summary>
        /// Deletes a user from the system
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteUser")]
        public IActionResult DeleteUser(int userID)
        {
            try
            {
                _userCrud.DeleteUser(userID);
                _logger.LogInformation("User deleted from the system");
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An internal error has occurred");
                return StatusCode(500, "An internal Error has occurred");
            }
        }

        /// <summary>
        /// Returns a user from the system
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUser")]
        public IActionResult GetUser(int userID)
        {
            try
            {
                _logger.LogInformation("Getting user with ID: " + userID);
                return Ok(_userCrud.GetUser(userID));
            } catch (Exception e)
            {
                _logger.LogError(e, "An internal error has occurred");
                return StatusCode(500, "An internal Error has occurred");
            }
        }

        /// <summary>
        /// Updates a user in the system
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateUser")]
        public IActionResult UpdateUser(User user)
        {
            try
            {
                _userCrud.UpdateUser(user);
                _logger.LogInformation("User updated in the system");
                return Ok();
            } catch (Exception e)
            {
                _logger.LogError(e, "An internal error has occurred");
                return StatusCode(500, "An internal Error has occurred");
            }
        }
    }
}
