using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TagebuchGhost.HttpClientProxy;
using TagebuchSharp.GhostData;
using TagebuchSharp.GhostData.Data;
using TagebuchSharp.Messages;

namespace TagebuchSharp.Services;

public class GhostPageRepository : IPageRepository
{
    private readonly IGhostContentHttpClient _httpClient;
    private readonly IUrlRenamer _urlRenamer;

    public GhostPageRepository(IGhostContentHttpClient httpClient, IUrlRenamer urlRenamer)
    {
        _httpClient = httpClient;
        _urlRenamer = urlRenamer;
    }
    public async Task<GetBasicInformationsResponse> GetBasicInformationsAsync(GetBasicInformationsRequest arg)
    {
        var s = (await _httpClient.GetJsonAsync<SettingsRoot>("settings/")).Data.Settings;
        if (s is null) throw new InvalidOperationException("Settings from Ghost are empty, check API compatibility and settings");
        return new GetBasicInformationsResponse(
            s.Title,
            s.Description,
            s.Logo,
            s.Icon,
            s.Lang,
            s.Timezone,
            s.Navigation.Select(i => new NavigationItem(i.Label, _urlRenamer.FixUrl(i.Url))).ToArray(),
            s.SecondaryNavigation.Select(i => new NavigationItem(i.Label, _urlRenamer.FixUrl(i.Url))).ToArray()
            );
    }

    public async Task<GetPageDataResponse> GetPageDataAsync(GetPageDataRequest arg)
    {
        if (string.IsNullOrWhiteSpace(arg.Slug)) throw new ArgumentException("Requested slug is null or whitespace.", nameof(arg));

        var p = (await _httpClient.GetJsonAsync<PageRoot>($"pages/slug/{arg.Slug}/")).Data?.Pages?.SingleOrDefault();
        if (p is null) throw new InvalidOperationException($"Page with slug: {arg.Slug}, don't exists");

        return new GetPageDataResponse(p.CreatePageItem(_urlRenamer));
    }
}
