using DirectMessageApplication;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DMService;

/// <summary>
/// Controller for user management
/// </summary>
[ApiController]
public class DirectMessageController : ControllerBase
{
    private readonly IDMService dmService;
    private readonly ILogger<DirectMessageController> logger;
    public DirectMessageController(ILogger<DirectMessageController> _logger, IDMService _dmService)
    {
        logger = _logger;
        dmService = _dmService;
    }

    [HttpGet]
    [Route("GetDMs")]
    [ProducesResponseType(typeof(List<DM>), 200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(500)]
    public IActionResult GetDMs(int senderID, int receiverID)
    {
        string accessToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (string.IsNullOrEmpty(accessToken))
        {
            logger.LogError("Unauthorized access");
            return Unauthorized();
        }

        try
        {
            logger.LogInformation("Getting DMs between sender with ID: " + senderID + " and receiver with ID: " + receiverID);
            return Ok(dmService.GetDMs(senderID, receiverID, accessToken));
        } catch (Exception e)
        {
            logger.LogError(e, "An internal error has occurred");
            return StatusCode(500, "An internal Error has occurred");
        }
    }

    [HttpPost]
    [Route("AddDM")]
    [ProducesResponseType(typeof(DM), 200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(500)]
    public IActionResult AddDM(int senderID, int receiverID, string message)
    {
        string accessToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (string.IsNullOrEmpty(accessToken))
        {
            logger.LogError("Unauthorized access");
            return Unauthorized();
        }

        try
        {
            logger.LogInformation("Adding DM between sender with ID: " + senderID + " and receiver with ID: " + receiverID);
            return Ok(dmService.AddDM(senderID, receiverID, message, accessToken));
        }
        catch (Exception e)
        {
            logger.LogError(e, "An internal error has occurred");
            return StatusCode(500, "An internal Error has occurred");
        }
    }

    [HttpPut]
    [Route("UpdateDM")]
    [ProducesResponseType(typeof(DM), 200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(500)]
    public IActionResult UpdateDM(int dmID, string message)
    {
        string accessToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (string.IsNullOrEmpty(accessToken))
        {
            logger.LogError("Unauthorized access");
            return Unauthorized();
        }

        try
        {
            logger.LogInformation("Updating DM with ID: " + dmID);
            return Ok(dmService.UpdateDM(dmID, message, accessToken));
        }
        catch (Exception e)
        {
            logger.LogError(e, "An internal error has occurred");
            return StatusCode(500, "An internal Error has occurred");
        }
    }

    [HttpDelete]
    [Route("DeleteDM")]
    [ProducesResponseType(200)]
    [ProducesResponseType(401)]
    [ProducesResponseType(500)]
    public IActionResult DeleteDM(int dmId)
    {
        string accessToken = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (string.IsNullOrEmpty(accessToken))
        {
            logger.LogError("Unauthorized access");
            return Unauthorized();
        }

        try
        {
            logger.LogInformation("Deleting DM with ID: " + dmId);
            dmService.DeleteDM(dmId, accessToken);
            return Ok();
        }
        catch (Exception e)
        {
            logger.LogError(e, "An internal error has occurred");
            return StatusCode(500, "An internal Error has occurred");
        }
    }
}
