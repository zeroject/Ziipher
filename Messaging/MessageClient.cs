using EasyNetQ;

namespace Messaging
{
    public class MessageClient
    {
        private readonly IBus _bus;

        public MessageClient(IBus bus)
        {
            _bus = bus;
        }

        public async Task Send<T>(T message, string topic)
        {
            await Task.Run(() => { _bus.PubSub.PublishAsync(message, topic); });
        }

        public async Task Listen<T>(Action<T> handler, string topic)
        {
            await Task.Run(() => { _bus.PubSub.SubscribeAsync(topic, handler); });
        }


        public void RespondToRequest<TRequest, TResponse>(Func<TRequest, TResponse> responder)
            where TResponse : class
        {
            _bus.Rpc.Respond(responder);
        }
        public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
        {
            // This will correctly await the response from the RPC call
            return await _bus.Rpc.RequestAsync<TRequest, TResponse>(request);
        }
    }
}
