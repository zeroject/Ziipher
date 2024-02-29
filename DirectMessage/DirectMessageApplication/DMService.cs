using Microsoft.Extensions.Logging;
using Domain;

namespace DirectMessageApplication;

public class DMService : IDMService
{
    private readonly IDMRepository dmRepository;
    private readonly ILogger<DMService> logger;

    public DMService(IDMRepository _dmRepository, ILogger<DMService> _logger)
    {
        logger = _logger;
        dmRepository = _dmRepository;
    }

    public List<DM> GetDMs(int senderID, int receiverID, string token)
    {
        logger.LogInformation("GetDMs method called");
        
        if (dmRepository.GetValidationAsync(token).Result)
        {
            return dmRepository.GetDMs(senderID, receiverID);
        }
        else
        {
            logger.LogWarning("Unauthorized access attempt in GetDMs method");
            throw new UnauthorizedAccessException();
        }
    }

    public DM AddDM(int senderID, int receiverID, string message, string token)
    {
        logger.LogInformation("AddDM method called");
        
        if (dmRepository.GetValidationAsync(token).Result)
        {
            return dmRepository.AddDM(senderID, receiverID, message);
        }
        else
        {
            logger.LogWarning("Unauthorized access attempt in AddDM method");
            throw new UnauthorizedAccessException();
        }
    }

    public DM UpdateDM(int dmID, int senderID, int receiverID, string message, string token)
    {
        logger.LogInformation("UpdateDM method called");
        
        if (dmRepository.GetValidationAsync(token).Result)
        {
            return dmRepository.UpdateDM(dmID, senderID, receiverID, message);
        }
        else
        {
            logger.LogWarning("Unauthorized access attempt in UpdateDM method");
            throw new UnauthorizedAccessException();
        }
    }

    public void DeleteDM(int dmID, string token)
    {
        logger.LogInformation("DeleteDM method called");
        
        if (dmRepository.GetValidationAsync(token).Result)
        {
            dmRepository.DeleteDM(dmID);
        }
        else
        {
            logger.LogWarning("Unauthorized access attempt in DeleteDM method");
            throw new UnauthorizedAccessException();
        }
    }
}
