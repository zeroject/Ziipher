using Domain;

namespace DirectMessageApplication;

public interface IDMService
{
    List<DM> GetDMs(int senderID, int receiverID);
    DM AddDM(int senderID, int receiverID, string message);
    DM UpdateDM(int dmID, int senderID, int receiverID, string message);
    void DeleteDM(int dmID);
}
