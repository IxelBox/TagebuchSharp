using TagebuchSharp.Services;

namespace TagebuchSharp.LamarRegistry;

internal class FakeUrlRenamer : IUrlRenamer
{
    public string FixUrl(string? url) => url ?? string.Empty;
}
