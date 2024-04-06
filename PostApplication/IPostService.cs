using Domain;
using PostApplication.DTO_s;

namespace PostApplication
{
    public interface IPostService
    {

        /// <summary>
        /// Retrieves all posts along with their corresponding likes from the specified timeline.
        /// </summary>
        /// <param name="timelineID">The ID of the timeline to retrieve posts from.</param>
        /// <returns>A dictionary containing the posts as keys and their corresponding likes as values.</returns>
        public List<Post> GetAllPosts(int timelineID);

        /// <summary>
        /// Create a post in the timeline
        /// </summary>
        /// <param name="timelineID">The timeline where the post is being created</param>
        /// <param name="newPost">The new post object</param>
        public Task<Post> CreatePost(PostPostDTO newPost);


        /// <summary>
        /// Retrieves a post and its associated like from the specified timeline and post ID.
        /// </summary>
        /// <param name="timelineID">The ID of the timeline.</param>
        /// <param name="postId">The ID of the post.</param>
        /// <returns>A dictionary containing the post and its associated like.</returns>
        public Post GetPost(int timelineID, int postId);

        /// <summary>
        /// Updates a post from a timeline
        /// </summary>
        /// <param name="timelineID">The timeline where we are updating a specific post</param>
        /// <param name="postId"> the post id of the ppost beingg updated</param>
        /// <param name="newText">the new text of the post</param>
        /// <param name="newPostDate">the updated date of thee post (may need to refactor this)</param>

        public void UpdatePost(int timelineID, int postId, string newText, DateTime? newPostDate = null);

        /// <summary>
        /// Deletes a post from a timeline
        /// </summary>
        /// <param name="timelineID">the timelline a post is being deleted from</param>
        /// <param name="postId">the post id of the post bbeing deleted</param>

        public void DeletePost(int timelineID, int postId);

        /// <summary>
        /// Gets all posts from a timeline by a user
        /// </summary>
        /// <param name="timelineID">the users timeline</param>
        /// <param name="userId">the user id</param>
        /// <returns></returns>

        public List<Post> GetPostsByUser(int timelineID, int userId);
    }
}