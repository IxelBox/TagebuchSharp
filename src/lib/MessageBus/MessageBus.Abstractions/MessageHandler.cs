namespace MessageBus;

public class MessageHandler<TRequest, TResponse> : IMessageHandler
{
    private readonly Func<TRequest, Task<TResponse>> _handlerFunc;

    public MessageHandler(Func<TRequest, Task<TResponse>> handlerFunc)
    {
        _handlerFunc = handlerFunc;
    }

    public Type RequestType => typeof(TRequest);
    public Type ResponseType => typeof(TResponse);

    public async Task<object> Handle(object request)
    {
        if (request is not TRequest)
        {
            throw new InvalidOperationException($"request isn't from type {typeof(TRequest)}");
        }

        var response = await _handlerFunc((TRequest)request).ConfigureAwait(false);
        if (response is not TResponse)
            throw new InvalidOperationException($"response isn't from type {typeof(TResponse)}");

        return response;
    }
}

