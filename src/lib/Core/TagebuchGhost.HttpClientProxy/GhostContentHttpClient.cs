using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http.Json;
using System.Text.Json;

namespace TagebuchGhost.HttpClientProxy;

public class GhostContentHttpClient : IGhostContentHttpClient
{
    private readonly GhostHttpClientOptions _options;
    private readonly HttpClient _httpClient;
    private readonly ILogger<GhostContentHttpClient> _logger;

    public GhostContentHttpClient(IHttpClientFactory httpClientFactory, IOptions<GhostHttpClientOptions> ghostHttpClientOptions, ILogger<GhostContentHttpClient>? logger = null)
    {
        if (ghostHttpClientOptions is null) throw new ArgumentNullException(nameof(ghostHttpClientOptions));

        _options = ghostHttpClientOptions.Value;

        if (_options.GhostUrl is null) throw new InvalidOperationException("Ghost URL can't null in configuration");

        _logger = logger ?? new NullLogger<GhostContentHttpClient>();
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri($"{_options.GhostUrl}{_options.ContentApiPath}");
        _httpClient.DefaultRequestHeaders.Add(_options.VersionAcceptHeader, _options.VersionAccept);

    }

    public virtual async Task<string> GetStringAsync([NotNull] string pathAndQuery)
    {
        _logger.LogDebug("Get string from {pathAndQuery}", pathAndQuery);
        return await _httpClient.GetStringAsync(AddContentApiKey(pathAndQuery));
    }

    public virtual async Task<(TValue Data, string Raw)> GetJsonAsync<TValue>([NotNull] string pathAndQuery)
    {
        _logger.LogDebug("Get JSON from {pathAndQuery}", pathAndQuery);
        var resultStr = await _httpClient.GetStringAsync(AddContentApiKey(pathAndQuery));
        var result = JsonSerializer.Deserialize<TValue>(resultStr);
        if (result is null) throw new InvalidOperationException($"Can't resolve result from {pathAndQuery}, result is null");
        return (result, resultStr);
    }

    private string AddContentApiKey([NotNull] string pathAndQuery)
    {
        string concatStr = pathAndQuery.Contains("?") ? "&" : "?";
        return $"{pathAndQuery}{concatStr}{_options.ContentApiKeyKey}={_options.ContentApiKey}";
    }

}