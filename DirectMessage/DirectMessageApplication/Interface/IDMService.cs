using Domain;

namespace DirectMessageApplication;

/// <summary>
/// Represents a service for managing direct messages.
/// </summary>
public interface IDMService
{
    /// <summary>
    /// Retrieves a list of direct messages between the specified sender and receiver.
    /// </summary>
    /// <param name="senderID">The ID of the sender.</param>
    /// <param name="receiverID">The ID of the receiver.</param>
    /// <param name="token">The authentication token.</param>
    /// <returns>A list of direct messages.</returns>
    List<DM> GetDMs(int senderID, int receiverID, string token);

    /// <summary>
    /// Adds a new direct message between the specified sender and receiver.
    /// </summary>
    /// <param name="senderID">The ID of the sender.</param>
    /// <param name="receiverID">The ID of the receiver.</param>
    /// <param name="message">The message content.</param>
    /// <param name="token">The authentication token.</param>
    /// <returns>The newly added direct message.</returns>
    DM AddDM(int senderID, int receiverID, string message, string token);

    /// <summary>
    /// Updates a direct message.
    /// </summary>
    /// <param name="dmID">The ID of the direct message.</param>
    /// <param name="message">The new message content.</param>
    /// <param name="token">The authentication token.</param>
    /// <returns>The updated direct message.</returns>
    DM UpdateDM(int dmID, string message, string token);

    /// <summary>
    /// Deletes a direct message.
    /// </summary>
    /// <param name="dmID">The ID of the direct message to delete.</param>
    /// <param name="token">The authentication token.</param>
    void DeleteDM(int dmID, string token);
}
