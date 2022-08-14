using System.Diagnostics.CodeAnalysis;
using TagebuchGhost.HttpClientProxy;

namespace TagebuchSharp.LamarRegistry;

internal class FakeContentHttpClient : IGhostContentHttpClient
{
    public Task<(TValue Data, string Raw)> GetJsonAsync<TValue>([NotNull] string pathAndQuery)
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
        => Task.FromResult((default(TValue), string.Empty));
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
    public Task<string> GetStringAsync([NotNull] string pathAndQuery) => Task.FromResult(string.Empty);
}
