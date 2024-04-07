namespace Domain;

/// <summary>
/// Represents a like on a post.
/// </summary>
public class Like
{
    /// <summary>
    /// Gets or sets the ID of the like.
    /// </summary>
    public int ID { get; set; }

    /// <summary>
    /// Gets or sets the list of user IDs who liked the post.
    /// </summary>
    public List<int>? UserIDs { get; set; } = new List<int>();

    /// <summary>
    /// Gets or sets the ID of the post that was liked.
    /// </summary>
    public int PostID { get; set; }

    /// <summary>
    /// Gets the count of users who liked the post.
    /// </summary>
    public int Count => UserIDs.Count;
}
