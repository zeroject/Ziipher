using System.Net.Http.Json;
using DirectMessageApplication;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DirectMessageInfrastructure;

public class DMRepository : IDMRepository
{
    private DbContextOptions<DbContext> options;
    private readonly ILogger<DMRepository> logger;

    public DMRepository(ILogger<DMRepository> _logger)
    {
        logger = _logger;
        options = new DbContextOptionsBuilder<DbContext>().UseInMemoryDatabase("DMDB").Options;
    }

    public List<DM> GetDMs(int senderID, int receiverID)
    {
        using (var context = new DbContext(options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
        {
            logger.LogInformation("Getting DMs from the database");
            return context.DMs.Where(c => c.SenderID == senderID && c.ReceiverID == receiverID).ToList();
        }
    }

    public DM AddDM(int senderID, int receiverID, string message)
    {
        using (var context = new DbContext(options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
        {
            logger.LogInformation("Adding a new DM to the database");
            DM dm = new DM()
            {
                SenderID = senderID,
                ReceiverID = receiverID,
                Message = message
            };
            _ = context.Add(dm);
            context.SaveChanges();
            return dm;
        }
    }

    public DM UpdateDM(int dmID, int senderID, int receiverID, string message)
    {
        using (var context = new DbContext(options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
        {
            logger.LogInformation("Updating a DM in the database");
            DM dmToUpdate = context.DMs.Find(dmID);
            dmToUpdate.SenderID = senderID;
            dmToUpdate.ReceiverID = receiverID;
            dmToUpdate.Message = message;
            _ = context.DMs.Update(dmToUpdate);
            context.SaveChanges();
            return dmToUpdate;
        }
    }

    public void DeleteDM(int dmID)
    {
        using (var context = new DbContext(options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
        {
            logger.LogInformation("Deleting a DM from the database");
            DM dmToUpdate = context.DMs.Find(dmID);
            _ = context.DMs.Remove(dmToUpdate);
            context.SaveChanges();
        }
    }

    public async Task<bool> GetValidationAsync(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            logger.LogError("Authorization token not found in request headers");
            return false;
        }
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("http://authservice:8080");
            var response = await client.PostAsJsonAsync($"/auth/validateUser?token={token}", "");
            if (!response.IsSuccessStatusCode)
            {
                logger.LogError("Error validating user to AuthWanabe");
                return false;
            }
            var data = await response.Content.ReadFromJsonAsync<bool>();
            if (data != true)
            {
                logger.LogInformation("User failed to auth himself");
                return false;
            }
            return true;
        }
    }
}
