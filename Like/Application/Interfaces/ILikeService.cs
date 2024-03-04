using Domain;

namespace LikeApplication;

/// <summary>
/// Represents a service for managing likes.
/// </summary>
public interface ILikeService
{
    /// <summary>
    /// Retrieves the likes based on the provided LikeDTO.
    /// </summary>
    /// <param name="likeDTO">The LikeDTO containing the necessary information.</param>
    /// <returns>The Like object representing the retrieved likes.</returns>
    Like GetLikes(LikeDTO likeDTO);

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
