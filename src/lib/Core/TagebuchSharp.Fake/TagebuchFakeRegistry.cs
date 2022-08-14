using Lamar;
using MessageBus;
using TagebuchGhost.HttpClientProxy;
using TagebuchSharp.Services;

namespace TagebuchSharp.LamarRegistry;

public class TagebuchFakeRegistry : ServiceRegistry
{
    public TagebuchFakeRegistry()
    {
        For<IGhostContentHttpClient>().Use<FakeContentHttpClient>().Singleton();
        For<IPostRepository>().Use<FakePostRepository>().Singleton();
        For<IPageRepository>().Use<FakePageRepository>().Singleton();
        For<IMessageBusListener>().Use<TagebuchSharpListener>().Singleton();
        For<IUrlRenamer>().Use<FakeUrlRenamer>().Singleton();
    }
}
