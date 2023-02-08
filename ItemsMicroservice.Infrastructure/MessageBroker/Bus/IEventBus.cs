namespace ItemsMicroservice.Infrastructure.MessageBroker.Bus;

public interface IEventBus
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) 
        where T : class;
}
