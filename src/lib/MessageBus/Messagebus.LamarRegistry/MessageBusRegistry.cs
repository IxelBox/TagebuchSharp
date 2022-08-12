using Lamar;
using MessageBus;

namespace Messagebus.LamarRegistry;

public class MessageBusRegistry : ServiceRegistry
{

    public MessageBusRegistry()
    {
        For<IMessageBus>().Use<LocalMessageBus>().Singleton();
    }
}