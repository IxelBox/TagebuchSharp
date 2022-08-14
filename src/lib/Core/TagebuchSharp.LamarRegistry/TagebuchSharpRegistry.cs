using Lamar;
using MessageBus;
using TagebuchGhost.HttpClientProxy;
using TagebuchSharp.Services;

namespace TagebuchSharp.LamarRegistry;

public class TagebuchSharpRegistry : ServiceRegistry
{
    public TagebuchSharpRegistry()
    {
        For<IGhostContentHttpClient>().Use<GhostContentHttpClient>().Singleton();
        For<IPostRepository>().Use<GhostPostRepository>().Singleton();
        For<IPageRepository>().Use<GhostPageRepository>().Singleton();
        For<IMessageBusListener>().Use<TagebuchSharpListener>().Singleton();
        For<IUrlRenamer>().Use<GhostUrlRenamer>().Singleton();
    }
}
