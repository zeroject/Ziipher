using EasyNetQ;
using Messaging;
using Messaging.Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimelineApplication.DTO;

namespace TimelineApplication.Helper
{
   public class AddPostToTimelineHandler: BackgroundService
    {
        private IServiceProvider _serviceProvider; 
        public AddPostToTimelineHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void HandleAddPostToTimeline(AddPostIfCreated message)
        {
            Console.WriteLine(message.Message);

            using (var scope = _serviceProvider.CreateScope())
            {
                var timelineService = scope.ServiceProvider.GetRequiredService<ITimelineService>();

                var post = new PostAddTimeline()
                {
                    PostId = message.PostID,
                    TimelineID = message.TimelineId
                };

                timelineService.AddPostToTimeline(post);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Message received");

            var messageClient = new MessageClient(
        RabbitHutch.CreateBus("host=rabbitmq;port=5672;virtualHost=/;username=guest;password=guest")
    );
            string topic = "AddPostToTimeline";

            await messageClient.Listen<AddPostIfCreated>(HandleAddPostToTimeline, topic);
        }
    }
}
