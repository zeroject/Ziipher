using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostInfrastructure
{
    public interface IPostRepository
    {
        /// <summary>
        /// Retrieves all posts with their corresponding likes for a given timeline.
        /// </summary>
        /// <param name="timelineID">The ID of the timeline.</param>
        /// <returns>A dictionary containing posts as keys and their corresponding likes as values.</returns>
        public Task<Dictionary<Post, Like>> GetAllPosts(int timelineID);

        /// <summary>
        /// Creates a new post in the specified timeline.
        /// </summary>
        /// <param name="timelineID">The ID of the timeline.</param>
        /// <param name="newPost">The new post to be created.</param>
        public Task<Post> CreatePost(int timelineID, Post newPost);

        /// <summary>
        /// Retrieves a specific post with its corresponding likes from the given timeline.
        /// </summary>
        /// <param name="timelineID">The ID of the timeline.</param>
        /// <param name="postId">The ID of the post.</param>
        /// <returns>A dictionary containing the post as key and its corresponding likes as value.</returns>
        public Task<Dictionary<Post, Like>> GetPost(int timelineID, int postId);

        /// <summary>
        /// Updates the text and/or post date of a specific post in the given timeline.
        /// </summary>
        /// <param name="timelineID">The ID of the timeline.</param>
        /// <param name="postId">The ID of the post.</param>
        /// <param name="newText">The new text for the post.</param>
        /// <param name="newPostDate">The new post date (optional).</param>
        public void UpdatePost(int timelineID, int postId, string newText, DateTime? newPostDate = null);

        /// <summary>
        /// Deletes a specific post from the given timeline.
        /// </summary>
        /// <param name="timelineID">The ID of the timeline.</param>
        /// <param name="postId">The ID of the post.</param>
        public void DeletePost(int timelineID, int postId);

        /// <summary>
        /// Retrieves all posts created by a specific user in the given timeline.
        /// </summary>
        /// <param name="timelineID">The ID of the timeline.</param>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>A list of posts created by the user.</returns>
        public List<Post> GetPostsByUser(int timelineID, int userId);
    }
}
