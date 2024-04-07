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
using TimelineApplication.DTO;

namespace TimelineApplication.Helper
{
    public class AddPostToTimelineHandler : BackgroundService
    {
        private IServiceProvider _serviceProvider;
        private readonly ILogger<AddPostToTimelineHandler> _logger;
        public AddPostToTimelineHandler(IServiceProvider serviceProvider, ILogger<AddPostToTimelineHandler> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        public async void HandleAddPostToTimeline(AddPostIfCreated message)
        {
            _logger.LogInformation(message.Message);

            try
            {
                using var scope = _serviceProvider.CreateScope();
                var timelineService = scope.ServiceProvider.GetRequiredService<ITimelineService>();
                var post = new PostAddTimeline()
                {
                    PostId = message.PostID,
                    TimelineID = message.TimelineId
                };
                await timelineService.AddPostToTimeline(post);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ArgumentException("Error in adding post to timeline");
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _logger.LogInformation("MESSAGE RECIEVED FROM NASA");
            var messageClient = new MessageClient(
        RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")
    );
            string topic = "AddPostToTimeline";

            await messageClient.Listen<AddPostIfCreated>(HandleAddPostToTimeline, topic);
        }
    }
}
