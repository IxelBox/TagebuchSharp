using TagebuchGhost.HttpClientProxy;
using TagebuchSharp.GhostData;
using TagebuchSharp.GhostData.Data;
using TagebuchSharp.Messages;

namespace TagebuchSharp.Services;

public class GhostPostRepository : IPostRepository
{
    private readonly IGhostContentHttpClient _httpClient;
    private readonly IUrlRenamer _urlRenamer;

    public GhostPostRepository(IGhostContentHttpClient httpClient, IUrlRenamer urlRenamer)
    {
        _httpClient = httpClient;
        _urlRenamer = urlRenamer;
    }

    public async Task<GetAllPostsResponse> GetAllPostsDataAsync(GetAllPostsRequest arg)
    {
        if (arg.PageNumber <= 0 || arg.ItemCount <= 0) throw new InvalidOperationException("current page number or item count aren't smaller and equal then 0");

        var publicFilter = "filter=visibility:public";
        var limitFilter = $"limit={arg.ItemCount}";
        var pageFilter = $"page={arg.PageNumber}";
        var includeTags = "include=tags";
        var fieldSelect = $"fields=title,url,slug,excerpt,published_at,updated_at{(arg.WithContent ? ",html" : string.Empty)}";
        var query = string.Join("&", publicFilter, limitFilter, pageFilter, includeTags, fieldSelect);
        var request = $"posts?{query}";
        var result = await _httpClient.GetJsonAsync<PostRoot>(request);
        if (!result.Data?.Posts?.Any() ?? false) throw new InvalidOperationException("No Posts found!");
        if (result.Data?.Meta is null) throw new InvalidOperationException("no meta data founds in posts request");

        return new GetAllPostsResponse(
            result.Data.Posts.Select(p =>
                new ShortPage(
                    p.Slug,
                    p.Title,
                    p.UpdatedAt,
                    p.PublishedAt,
                    p.Excerpt,
                    p.Html,
                    p.Tags.Select(t => new TagItem(
                        t.Id,
                        t.Name,
                        t.Slug,
                        t.Description
                        )).ToArray()
                      )).ToArray(),
                result.Data.Meta.Pagination.Total,
                result.Data.Meta.Pagination.Page
            );
    }

    public async Task<GetPostDataResponse> GetPostDataAsync(GetPostDataRequest arg)
    {
        if (string.IsNullOrWhiteSpace(arg.Slug)) throw new ArgumentException("Requested slug is null or whitespace.", nameof(arg));

        var p = (await _httpClient.GetJsonAsync<PostRoot>($"posts/slug/{arg.Slug}?include=tags")).Data?.Posts?.SingleOrDefault();
        if (p is null) throw new InvalidOperationException($"post with slug: {arg.Slug}, don't exists");

        return new GetPostDataResponse(p.CreatePageItem(_urlRenamer));
    }
}
