using Microsoft.Extensions.Options;
using TagebuchSharp.Options;
using TagebuchGhost.HttpClientProxy;

namespace TagebuchSharp.Services;

public class GhostUrlRenamer : IUrlRenamer
{
    private readonly WebSettings _webSettings;
    private readonly GhostHttpClientOptions _ghostSettings;

    public GhostUrlRenamer(IOptions<WebSettings> settings, IOptions<GhostHttpClientOptions> ghostOptions)
    {
        _ghostSettings = ghostOptions.Value;
        _webSettings = settings.Value;
    }
    public string FixUrl(string? url)
    {
        return url?.Replace(_ghostSettings?.GhostUrl ?? "", _webSettings?.Url ?? "");
    }
}
