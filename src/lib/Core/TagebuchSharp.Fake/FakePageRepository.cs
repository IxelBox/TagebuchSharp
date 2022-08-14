using TagebuchSharp.Messages;
using TagebuchSharp.Services;

namespace TagebuchSharp.LamarRegistry;

internal class FakePageRepository : IPageRepository
{
    public Task<GetBasicInformationsResponse> GetBasicInformationsAsync(GetBasicInformationsRequest arg)
        => Task.FromResult(new GetBasicInformationsResponse(
            "MyBlog",
            "My Ultimativ Blog",
            "https://via.placeholder.com/600x72.png",
            "https://via.placeholder.com/60.png",
            "de",
            "de-de",
            new[] {
                new NavigationItem("About", "about"),
                new NavigationItem("Contact", "contact")
            },
             new[] {
                new NavigationItem("Impressum", "impressum"),
                new NavigationItem("Privacy", "privacy")
            }));

    public Task<GetPageDataResponse> GetPageDataAsync(GetPageDataRequest arg)
    {
        var lorem = new Bogus.DataSets.Lorem("de");
        return Task.FromResult(
            new GetPageDataResponse(
                new PageItem(
                    arg.Slug,
                    arg.Slug,
                    lorem.Sentence(30),
                    lorem.Text(),
                    null, null, null, null, null, null,
                    $"http://localhost/{arg.Slug}",
                    new TagItem[0],
                    DateTime.Now,
                    DateTime.Now)));
    }
}
