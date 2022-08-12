namespace TagebuchGhost.HttpClientProxy;

public class GhostHttpClientOptions
{
    public string? ContentApiKey { get; set; } = string.Empty;
    public string? ContentApiKeyKey { get; set; } = "key";
    public string? GhostUrl { get; set; } = string.Empty;
    public string VersionAcceptHeader { get; set; } = "Accept-Version";
    public string VersionAccept { get; set; } = "v5.8";
    public string ContentApiPath { get; set; } = "/ghost/api/content/";
}