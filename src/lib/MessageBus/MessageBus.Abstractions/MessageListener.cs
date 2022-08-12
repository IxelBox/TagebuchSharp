namespace MessageBus;

public class MessageListener<TRequest, TResponse> : IMessageListener
{
    private readonly Func<TResponse, Task> _listenerFunc;

    public MessageListener(Func<TResponse, Task> listenerFunc)
    {
        _listenerFunc = listenerFunc;
    }

    public virtual Type RequestType => typeof(TRequest);
    public virtual Type ResponseType => typeof(TResponse);
    public async Task Listen(object response)
    {
        if (response is not TResponse)
        {
            throw new InvalidOperationException($"response isn't from type {typeof(TResponse)}");
        }
        await _listenerFunc((TResponse)response).ConfigureAwait(false);
    }
}

//public interface IMessageHandler<in TRequest, TResponse> : IMessageHandler
//{
//    public new Type RequestType => typeof(TRequest);

//    public new Type ResponseType => typeof(TResponse);

//    Task<TResponse> Handle(TRequest request);
//}
