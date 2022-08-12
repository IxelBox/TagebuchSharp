using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System.Collections.Concurrent;

namespace MessageBus;

public class LocalMessageBus : IMessageBus
{
    readonly ConcurrentDictionary<Type, ConcurrentBag<IMessageHandler>> _messageHandlers = new();
    readonly SynchronizedCollection<IMessageListener> _messageListeners = new();
    private readonly ILogger _logger;

    public LocalMessageBus(ILogger logger = null)
    {
        _logger = logger ?? NullLogger.Instance;
    }


    public Task AddMessageHandler<TRequest, TResponse>(MessageHandler<TRequest, TResponse> messageHandler)
    {
        _messageHandlers.AddOrUpdate(typeof(TRequest), new ConcurrentBag<IMessageHandler>(new[] { messageHandler }), (_, bag) =>
        {
            bag.Add(messageHandler);
            return bag;
        });

        return Task.CompletedTask;
    }

    public Task Send<TRequest>(TRequest request)
    {
        _logger.LogDebug("Send request {request}", request);
        if (!_messageHandlers.ContainsKey(typeof(TRequest)))
            throw new ArgumentException("Request type hasn't a handler", nameof(request));

        return SendExecute(request);
    }

    private async Task SendExecute<TRequest>(TRequest request)
    {
        var handlers = _messageHandlers[typeof(TRequest)];

        _logger.LogDebug("Request {request} handled by {handlers}", request, handlers);
        foreach (var handler in handlers)
        {
            var validListener = _messageListeners
                 .Where(listener =>
                 listener.ResponseType == handler.ResponseType &&
                 listener.RequestType == handler.RequestType).ToList();

            if (validListener.Any())
            {
                _logger.LogDebug("before handler {handler} execute request {request}", handler, request);
                var response = await handler.Handle(request).ConfigureAwait(false);
                foreach (var listener in validListener)
                {
                    _logger.LogDebug("before listening {listener}", listener);
                    _logger.LogTrace("before listening {request}, {response}, {listener}", request, response, listener);
                    await listener.Listen(response).ConfigureAwait(false);
                }
            }
        }
    }

    public Task<bool> Subscribe<TRequest, TResponse>(MessageListener<TRequest, TResponse> messageListener)
    {
        _messageListeners.Add(messageListener);
        return Task.FromResult(_messageListeners.Contains(messageListener));
    }

    public Task<bool> UnSubscribe<TRequest, TResponse>(MessageListener<TRequest, TResponse> messageListener)
    {
        return Task.FromResult(_messageListeners.Remove(messageListener));
    }
}
