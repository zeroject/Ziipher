using Domain;

namespace LikeApplication;

/// <summary>
/// Represents a repository for managing likes.
/// </summary>
public interface ILikeRepository
{
    /// <summary>
    /// Retrieves a list of likes.
    /// </summary>
    /// <returns>A list of <see cref="Like"/> objects.</returns>
    List<Like> GetLikes();

    /// <summary>
    /// Retrieves the like associated with the specified post ID.
    /// </summary>
    /// <param name="postId">The ID of the post.</param>
    /// <returns>The like associated with the post.</returns>
    Like GetLike(int postId);

    /// <summary>
    /// Creates a new like based on the provided LikeDTO.
    /// </summary>
    /// <param name="likeDTO">The LikeDTO containing the necessary information.</param>
    /// <returns>The Like object representing the created like.</returns>
    Like CreateLike(LikeDTO likeDTO);

    /// <summary>
    /// Adds a like based on the provided LikeDTO.
    /// </summary>
    /// <param name="likeDTO">The LikeDTO containing the necessary information.</param>
    /// <returns>The Like object representing the added like.</returns>
    Like AddLike(LikeDTO likeDTO);

    /// <summary>
    /// Removes a like based on the provided LikeDTO.
    /// </summary>
    /// <param name="likeDTO">The LikeDTO containing the necessary information.</param>
    void RemoveLike(LikeDTO likeDTO);
}
