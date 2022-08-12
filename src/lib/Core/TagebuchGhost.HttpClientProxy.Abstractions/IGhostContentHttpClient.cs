using System.Diagnostics.CodeAnalysis;

namespace TagebuchGhost.HttpClientProxy
{
    public interface IGhostContentHttpClient
    {
        Task<(TValue Data, string Raw)> GetJsonAsync<TValue>([NotNull] string pathAndQuery);
        Task<string> GetStringAsync([NotNull] string pathAndQuery);
    }
}