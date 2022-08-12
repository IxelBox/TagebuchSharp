using System.Collections.Concurrent;

namespace MessageBus;

public interface IMessageBus
{
    Task<bool> Subscribe<TRequest, TResponse>(MessageListener<TRequest, TResponse> messageListener);
    Task<bool> UnSubscribe<TRequest, TResponse>(MessageListener<TRequest, TResponse> messageListener);

    Task Send<TRequest>(TRequest request);

    Task AddMessageHandler<TRequest, TResponse>(MessageHandler<TRequest, TResponse> messageHandler);
}

public static class MessageBusExtensions
{
    public static async Task<IReadOnlyCollection<TResponse>> RequestMany<TRequest, TResponse>(this IMessageBus bus, TRequest request)
    {
        ConcurrentBag<TResponse> responses = new();
        var listener = new MessageListener<TRequest, TResponse>(response => { responses.Add(response); return Task.CompletedTask; });
        await bus.Subscribe(listener);
        await bus.Send(request);
        await bus.UnSubscribe(listener);
        return responses;

    }

    public static async Task<TResponse> Request<TRequest, TResponse>(this IMessageBus bus, TRequest request)
    {
        IReadOnlyCollection<TResponse> responses = await RequestMany<TRequest, TResponse>(bus, request);
        return responses.Single();
    }
}
