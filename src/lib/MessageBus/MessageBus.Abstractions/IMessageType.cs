namespace MessageBus;

public interface IMessageType
{
    Type RequestType { get; }
    Type ResponseType { get; }
}

//public interface IMessageHandler<in TRequest, TResponse> : IMessageHandler
//{
//    public new Type RequestType => typeof(TRequest);

//    public new Type ResponseType => typeof(TResponse);

//    Task<TResponse> Handle(TRequest request);
//}
