using AutoMapper;
using Domain;
using EasyNetQ;
using Messaging;
using NSubstitute;
using PostApplication;
using PostApplication.DTO_s;
using PostApplication.Helper;
using PostInfrastructure;
using TimelineApplication;
using TimelineApplication.DTO;
using TimelineInfrastructure;

namespace Tests;

public class PostTest
{
    private PostService _postService;
    private TimelineService _timelineService;
    private IPostRepository _postRepo;
    private ITimelineRepository _timelineRepo;
    private IMapper _mapper;


    public PostTest()
    {
        _postRepo = Substitute.For<IPostRepository>();
        _mapper = Substitute.For<IMapper>();
    }

    [Fact]
    public void UnitTest_GetPost()
    {
        // Arrange
        _postRepo = Substitute.For<IPostRepository>();
        _postService = new PostService(_postRepo, _mapper, null);

        int postID = 1;
        int timelineID = 1;

        _postRepo.GetPost(Arg.Any<int>(), Arg.Any<int>()).Returns(new Post { PostID = postID, Text = "Test", PostDate = DateTime.Now, TimelineID = timelineID });

        //Act
        var result = _postService.GetPost(timelineID, postID);

        //Assert
        _postRepo.Received(1).GetPost(Arg.Any<int>(), Arg.Any<int>());
        Assert.NotNull(result);
    }

    [Fact]
    public void UnitTest_GetAllPosts()
    {
        // Arrange
        _postRepo = Substitute.For<IPostRepository>();
        _postService = new PostService(_postRepo, _mapper, null);

        int timelineID = 1;

        _postRepo.GetAllPosts(Arg.Any<int>()).Returns(new List<Post> { new Post { PostID = 1, Text = "Test", PostDate = DateTime.Now, TimelineID = timelineID } });

        //Act
        var result = _postService.GetAllPosts(timelineID);

        //Assert
        _postRepo.Received(1).GetAllPosts(Arg.Any<int>());
        Assert.NotNull(result);
    }

    [Fact]
    public void UnitTest_UpdatePost()
    {
        // Arrange
        _postRepo = Substitute.For<IPostRepository>();
        _postService = new PostService(_postRepo, _mapper, null);

        int postID = 1;
        int timelineID = 1;

        //Act
        _postService.UpdatePost(timelineID, postID, "Test", DateTime.Now);

        //Assert
        _postRepo.Received(1).UpdatePost(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>(), Arg.Any<DateTime>());
    }

    [Fact]
    public void UnitTest_DeletePost()
    {
        // Arrange
        _postRepo = Substitute.For<IPostRepository>();
        _postService = new PostService(_postRepo, _mapper, null);

        int postID = 1;
        int timelineID = 1;

        //Act
        _postService.DeletePost(timelineID, postID);

        //Assert
        _postRepo.Received(1).DeletePost(Arg.Any<int>(), Arg.Any<int>());
    }

    [Fact]
    public void UnitTest_GetPostsByUser()
    {
        // Arrange
        _postRepo = Substitute.For<IPostRepository>();
        _postService = new PostService(_postRepo, _mapper, null);

        int timelineID = 1;
        int userID = 1;

        _postRepo.GetPostsByUser(Arg.Any<int>(), Arg.Any<int>()).Returns(new List<Post> { new Post { PostID = 1, Text = "Test", PostDate = DateTime.Now, TimelineID = timelineID } });

        //Act
        var result = _postService.GetPostsByUser(timelineID, userID);

        //Assert
        _postRepo.Received(1).GetPostsByUser(Arg.Any<int>(), Arg.Any<int>());
        Assert.NotNull(result);
    }

    [Fact]
    public async void UnitTest_AddCommentToPost()
    {
        // Arrange
        _postRepo = Substitute.For<IPostRepository>();
        _postService = new PostService(_postRepo, _mapper, null);

        //Act
        await _postService.AddCommentToPost(new PostAddComment { PostId = 1, Message = "Test", CommentID = 1 });

        //Assert
        await _postRepo.Received(1).AddCommentToPost(Arg.Any<Comment>());
    }

// Service Test for CreatePost

    [Fact]
    public async Task ServiceTest_CreatePost()
    {
        // Arrange
        var _bus = Substitute.For<IBus>();
        var MC = Substitute.For<MessageClient>(_bus);

        _postRepo = Substitute.For<IPostRepository>();
        _timelineRepo = Substitute.For<ITimelineRepository>();

        _postService = new PostService(_postRepo, _mapper, MC);
        _timelineService = new TimelineService(_timelineRepo, _mapper);

        _postRepo.CreatePost(Arg.Any<Post>()).Returns(new Post { PostID = 23, Text = "Test", PostDate = DateTime.Now, TimelineID = 1 });
        _timelineRepo.CreateTimeline(Arg.Any<Timeline>()).Returns(new Timeline { TimelineID = 1, UserID = 1 });
        _timelineRepo.GetTimeline(Arg.Any<int>()).Returns(new Timeline { TimelineID = 1, UserID = 1, PostIDs = new List<int> { 23 } });

        // Act
        _timelineService.CreateTimeline(new PostTimelineDTO { UserID = 1 });
        await _postService.CreatePost(new PostPostDTO { PostDate = DateTime.Now, Text = "Test", TimelineID = 1 });
        var result = _timelineService.GetTimeline(1);

        // Assert
        _timelineRepo.Received().CreateTimeline(Arg.Any<Timeline>());
        await _postRepo.Received().CreatePost(Arg.Any<Post>());
        _timelineRepo.Received().GetTimeline(1);

        Assert.Equal(1, result.TimelineID);
        Assert.Single(result.PostIDs, 23);
    }
}