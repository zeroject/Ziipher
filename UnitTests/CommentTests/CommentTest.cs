using AutoMapper;
using CommentApplication;
using CommentInfrastructure;
using Domain;
using Domain.DTO_s;
using NSubstitute;

namespace UnitTests.CommentTests;

public class CommentTest
{
    private CommentCrud _commentService;
    private CommentRepository _commentRepo;
    private IMapper _mapper;

    public CommentTest()
    {
        _commentRepo = Substitute.For<CommentRepository>();
        _mapper = Substitute.For<IMapper>();
    }

    public CommentCrud GetInstance()
    {
        _commentService = new CommentCrud(_commentRepo);
        return _commentService;
    }


    [Fact]
    public void CreateCommentTest()
    {
        // Arrange
        var commentDTO = new CommentDTO
        {
            CommentText = "Sample comment text"
        };

        // Act
        var sut = GetInstance();
        sut.AddComment(commentDTO);

        // Assert
        _commentRepo.Received(1).AddComment(Arg.Any<Comment>());
    }
}


