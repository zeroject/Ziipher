using EasyNetQ;
using Messaging.Messages;
using Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace PostApplication.Helper
{
    public class AddCommentToPostHandler: BackgroundService
    {
        private IServiceProvider _serviceProvider;
        private readonly ILogger<AddCommentToPostHandler> _logger;
        public AddCommentToPostHandler(IServiceProvider serviceProvider, ILogger<AddCommentToPostHandler> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async void HandleAddCommentToPost(PostAddComment message)
        {
            _logger.LogInformation(message.Message);

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var postService = scope.ServiceProvider.GetRequiredService<IPostService>();
                var post = new PostAddComment()
                {
                    PostId = message.PostId,
                    CommentID = message.CommentID
                };
                await postService.AddCommentToPost(post);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ArgumentException("Error in adding comment to post");
            }
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {

            _logger.LogInformation("MESSAGE RECIEVED FROM NASA");
            var messageClient = new MessageClient(
        RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")
    );
            string topic = "AddCommentToPost";

            await messageClient.Listen<PostAddComment>(HandleAddCommentToPost, topic);
        }

    }
}
