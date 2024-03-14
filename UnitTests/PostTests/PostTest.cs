using AutoMapper;
using Domain;
using Microsoft.Extensions.Options;
using NSubstitute;
using PostApplication;
using PostApplication.DTO_s;
using PostInfrastructure;
using TimelineInfrastructure;

namespace UnitTests.PostTests
{
    public class PostTest
    {
        private PostService _postService ;

        private IPostRepository _postRepo;

        private IMapper _mapper;


        public PostTest() 
        { 
            _postRepo = Substitute.For<IPostRepository>();
            _mapper = Substitute.For<IMapper>();
        }

        public PostService GetInstance() 
        {
            _postService = new PostService(_postRepo, _mapper);
            return _postService;
        }




        [Fact]
        public void CreatePostWTest()
        {
            GetInstance();
            int timelineID = 1;
            var newPostDTO = new PostPostDTO();
            var mappedPost = new Post();
            _mapper.Map<Post>(Arg.Any<PostPostDTO>()).Returns(mappedPost);
            _postService.CreatePost(timelineID, newPostDTO);

            Assert.Equal(timelineID, newPostDTO.TimelineID);
            Assert.True((DateTime.Now - newPostDTO.PostDate).TotalSeconds < 1, "PostDate was not set to the current time.");
            _mapper.Received(1).Map<Post>(newPostDTO);
            _postRepo.Received(1).CreatePost(timelineID, mappedPost);
        }

        [Fact]
        public void UppatePostTest() 
        {
            GetInstance();


        
        }
    }
}
