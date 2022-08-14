
using Bogus;
using TagebuchSharp.Messages;
using TagebuchSharp.Services;

namespace TagebuchSharp.LamarRegistry;

internal class FakePostRepository : IPostRepository
{
    readonly List<PageItem> _pages;
    public FakePostRepository()
    {
        var pageItemFaker = new Faker<PageItem>("de")
                    .CustomInstantiator(f => new PageItem(
                        f.Lorem.Slug(),
                        f.Lorem.Sentence(range: 1),
                        f.Lorem.Sentences(3, " "),
                        f.Lorem.Text(),
                        null,
                        null,
                        null,
                        null,
                        null,
                        null,
                        $"http://localhost/post/{f.Lorem.Slug()}",
                        f.Random.ArrayElements(TagItems).ToArray(),
                        f.Date.Past(),
                        f.Date.Past()
                    ));
        _pages.AddRange(pageItemFaker.Generate(100));
    }

    TagItem[] TagItems = new[]
    {
        new TagItem(Guid.NewGuid().ToString(), "News", "news", "a news"),
        new TagItem(Guid.NewGuid().ToString(), "Programming", "programming", "article with programming focus"),
        new TagItem(Guid.NewGuid().ToString(), "Foobar", "foobar", "a foobar article"),
        new TagItem(Guid.NewGuid().ToString(), "42", "42", "the answer"),
        new TagItem(Guid.NewGuid().ToString(), "Personal", "personal", "a personal message"),
    };

    public Task<GetAllPostsResponse> GetAllPostsDataAsync(GetAllPostsRequest arg)
    {
        return Task.FromResult(new GetAllPostsResponse(
            _pages.Skip((arg.PageNumber - 1) * arg.ItemCount).Take(arg.ItemCount)
                .Select(p => new ShortPage(
                    p.Slug,
                    p.Title,
                    p.UpdatedAt,
                    p.PublishedAt,
                    p.Excerpt,
                    arg.WithContent ? p.Html : "",
                    p.Tags
                    )).ToArray(),
            (int)Math.Ceiling(Convert.ToDouble(_pages.Count) / Convert.ToDouble(arg.ItemCount)),
            arg.PageNumber));
    }
    public Task<GetPostDataResponse> GetPostDataAsync(GetPostDataRequest arg)
    {
        return Task.FromResult(new GetPostDataResponse(_pages.First(p => p.Slug == arg.Slug)));
    }
}
