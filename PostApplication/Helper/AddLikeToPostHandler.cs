using EasyNetQ;
using Messaging;
using Messaging.Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostApplication.Helper
{
    public class AddLikeToPostHandler : BackgroundService
    {
        private IServiceProvider serviceProvider;
        private readonly ILogger<AddLikeToPostHandler> logger;

        public AddLikeToPostHandler(IServiceProvider serviceProvider, ILogger<AddLikeToPostHandler> logger)
        {
            this.serviceProvider = serviceProvider;
            this.logger = logger;
        }

    public async void HandleAddLikeToPost(AddLikeIfCreated message)
        {
            logger.LogInformation(message.Message);

            try
            {
                using var scope = serviceProvider.CreateScope();
                var postService = scope.ServiceProvider.GetRequiredService<IPostService>();
                var post = new PostAddLike()
                {
                    LikeID = message.LikeID,
                    PostId = message.PostID,
                };
                await postService.AddLikeToPost(post);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ArgumentException("Error in adding like to post");
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("MESSAGE RECIEVED FROM NASA");
            var messageClient = new MessageClient(
                               RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")
                                          );
            string topic = "AddLikeToPost";

            return messageClient.Listen<AddLikeIfCreated>(HandleAddLikeToPost, topic);
        }
    }
}
