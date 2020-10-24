namespace WebGateway.Messaging
{
    using System;
    using System.Threading.Tasks;

    public interface IPublisher
    {
        Task Publish<TMessage>(TMessage message);

        Task Publish<TMessage>(TMessage message, Type messageType);
    }
}
