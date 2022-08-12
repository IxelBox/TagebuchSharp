namespace MessageBus;

public interface IMessageHandler : IMessageType
{
    Task<object> Handle(object request);
}

//public interface IMessageHandler<in TRequest, TResponse> : IMessageHandler
//{
//    public new Type RequestType => typeof(TRequest);

//    public new Type ResponseType => typeof(TResponse);

//    Task<TResponse> Handle(TRequest request);
//}
