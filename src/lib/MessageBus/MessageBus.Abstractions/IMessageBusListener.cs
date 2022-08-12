namespace MessageBus;

public interface IMessageBusListener
{
    Task StartListingAsync();
}