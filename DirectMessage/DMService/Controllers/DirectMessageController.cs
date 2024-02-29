using DirectMessageApplication;
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
    public IActionResult GetDMs(int senderID, int receiverID)
    {
        try
        {
            logger.LogInformation("Getting DMs between sender with ID: " + senderID + " and receiver with ID: " + receiverID);
            return Ok(dmService.GetDMs(senderID, receiverID));
        } catch (Exception e)
        {
            logger.LogError(e, "An internal error has occurred");
            return StatusCode(500, "An internal Error has occurred");
        }
    }

    [HttpPost]
    [Route("AddDM")]
    public IActionResult AddUser(int senderID, int receiverID, string message)
    {
        try
        {
            logger.LogInformation("Adding DM between sender with ID: " + senderID + " and receiver with ID: " + receiverID);
            return Ok(dmService.AddDM(senderID, receiverID, message));
        }
        catch (Exception e)
        {
            logger.LogError(e, "An internal error has occurred");
            return StatusCode(500, "An internal Error has occurred");
        }
    }

    [HttpPut]
    [Route("UpdateDM")]
    public IActionResult UpdateUser(int dmID, int senderID, int receiverID, string message)
    {
        try
        {
            logger.LogInformation("Updating DM with ID: " + dmID);
            return Ok(dmService.UpdateDM(dmID, senderID, receiverID, message));
        }
        catch (Exception e)
        {
            logger.LogError(e, "An internal error has occurred");
            return StatusCode(500, "An internal Error has occurred");
        }
    }

    [HttpDelete]
    [Route("DeleteDM")]
    public IActionResult DeleteDM(int dmID)
    {
        try
        {
            logger.LogInformation("Deleting DM with ID: " + dmID);
            return Ok(dmService.DeleteDM(dmID));
        }
        catch (Exception e)
        {
            logger.LogError(e, "An internal error has occurred");
            return StatusCode(500, "An internal Error has occurred");
        }
    }
}
