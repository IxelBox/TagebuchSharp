using MessageBus;
using Microsoft.Extensions.Hosting;

namespace TagebuchSharp;

public class ListingStartupHostService : IHostedService
{
    private readonly IEnumerable<IMessageBusListener> _listeners;

    public ListingStartupHostService(IEnumerable<IMessageBusListener> listener)
    {
        _listeners = listener;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        foreach (var listener in _listeners)
        {
            await listener.StartListingAsync();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
