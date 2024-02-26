using Domain;

namespace DirectMessageApplication;

public class DMService : IDMService
{
    private readonly IDMRepository dmRepository;

    public DMService(IDMRepository _dmRepository)
    {
        dmRepository = _dmRepository;
    }

    public List<DM> GetDMs(int senderID, int receiverID)
    {
        return dmRepository.GetDMs(senderID, receiverID);
    }

    public DM AddDM(int senderID, int receiverID, string message)
    {
        return dmRepository.AddDM(senderID, receiverID, message);
    }

    public DM UpdateDM(int dmID, int senderID, int receiverID, string message)
    {
        return dmRepository.UpdateDM(dmID, senderID, receiverID, message);
    }

    public void DeleteDM(int dmID)
    {
        dmRepository.DeleteDM(dmID);
    }
}
