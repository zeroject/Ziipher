using DirectMessageApplication;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DirectMessageInfrastructure;

public class DMRepository : IDMRepository
{
    private DbContextOptions<DbContext> _options;


    public DMRepository()
    {
        _options = new DbContextOptionsBuilder<DbContext>().UseInMemoryDatabase("DMDB").Options;
    }

    public List<DM> GetDMs(int senderID, int receiverID)
    {
        using (var context = new DbContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
        {
            return context.DMs.Where(c => c.SenderID == senderID && c.ReceiverID == receiverID).ToList();
        }
    }

    public DM AddDM(int senderID, int receiverID, string message)
    {
        using (var context = new DbContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
        {
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
        using (var context = new DbContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
        {
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
        using (var context = new DbContext(_options, Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped))
        {
            DM dmToUpdate = context.DMs.Find(dmID);
            _ = context.DMs.Remove(dmToUpdate);
            context.SaveChanges();
        }
    }
}
