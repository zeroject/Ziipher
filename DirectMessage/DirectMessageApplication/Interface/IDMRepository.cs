using Domain;

namespace DirectMessageApplication;

/// <summary>
/// Represents the interface for a Direct Message repository.
/// </summary>
public interface IDMRepository
{
    /// <summary>
    /// Retrieves a list of Direct Messages between the specified sender and receiver.
    /// </summary>
    /// <param name="senderID">The ID of the sender.</param>
    /// <param name="receiverID">The ID of the receiver.</param>
    /// <returns>A list of Direct Messages.</returns>
    List<DM> GetDMs(int senderID, int receiverID);

    /// <summary>
    /// Adds a new Direct Message between the specified sender and receiver.
    /// </summary>
    /// <param name="senderID">The ID of the sender.</param>
    /// <param name="receiverID">The ID of the receiver.</param>
    /// <param name="message">The message content.</param>
    /// <returns>The newly added Direct Message.</returns>
    DM AddDM(int senderID, int receiverID, string message);

    /// <summary>
    /// Updates an existing Direct Message with the specified ID.
    /// </summary>
    /// <param name="dmID">The ID of the Direct Message to update.</param>
    /// <param name="senderID">The ID of the sender.</param>
    /// <param name="receiverID">The ID of the receiver.</param>
    /// <param name="message">The updated message content.</param>
    /// <returns>The updated Direct Message.</returns>
    DM UpdateDM(int dmID, int senderID, int receiverID, string message);

    /// <summary>
    /// Deletes the Direct Message with the specified ID.
    /// </summary>
    /// <param name="dmID">The ID of the Direct Message to delete.</param>
    void DeleteDM(int dmID);

    /// <summary>
    /// Asynchronously validates a token.
    /// </summary>
    /// <param name="token">The token to validate.</param>
    /// <returns>A task representing the asynchronous operation. The task result is true if the token is valid; otherwise, false.</returns>
    Task<bool> GetValidationAsync(string token);
}
