namespace MessageBus;

public interface IMessageListener : IMessageType
{
    Task Listen(object response);
}

//public interface IMessageHandler<in TRequest, TResponse> : IMessageHandler
//{
//    public new Type RequestType => typeof(TRequest);

//    public new Type ResponseType => typeof(TResponse);

//    Task<TResponse> Handle(TRequest request);
//}
