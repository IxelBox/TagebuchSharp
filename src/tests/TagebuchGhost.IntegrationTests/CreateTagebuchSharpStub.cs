using Lamar;
using Messagebus.LamarRegistry;
using MessageBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using TagebuchGhost.HttpClientProxy;
using TagebuchSharp.LamarRegistry;
using TagebuchSharp.Options;

namespace TagebuchGhost.IntegrationTests;

public class CreateTagebuchSharpStub
{
    private GhostHttpClientOptions _defaultOptions => new()
    {
        ContentApiKey = "18f7f624f8ec5f024f635e2c25",
        GhostUrl = "http://localhost:8888/"
    };

    Container _container;

    public CreateTagebuchSharpStub(GhostHttpClientOptions options = null)
    {
        options ??= _defaultOptions;
        _container = new Container(x =>
        {
            x.AddHttpClient();
            x.AddLogging();
            x.IncludeRegistry<TagebuchSharpRegistry>();
            x.IncludeRegistry<MessageBusRegistry>();
            x.ForSingletonOf<IOptions<GhostHttpClientOptions>>().Use(Options.Create(options));
            x.ForSingletonOf<IOptions<WebSettings>>().Use(Options.Create(new WebSettings { Url = "http://localhost:5001/" }));
        });

    }

    public async Task<IMessageBus> StartApp()
    {
        _container.AssertConfigurationIsValid();
        var listeners = _container.GetInstance<IEnumerable<IMessageBusListener>>();
        foreach (var listener in listeners)
        {
            await listener.StartListingAsync();
        }
        return _container.GetInstance<IMessageBus>();
    }

}
