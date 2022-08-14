using MessageBus;
using TagebuchSharp.Messages;

namespace TagebuchGhost.IntegrationTests;

[Collection("Docker Compose Ghost")]
public class PageAndPostDataRequest
{
    private readonly DockerComposeGhostFixture _compose;


    public PageAndPostDataRequest(DockerComposeGhostFixture compose)
    {
        _compose = compose;
    }

    [Fact]
    public async Task GetBasicInformation()
    {
        var msgBus = await new CreateTagebuchSharpStub().StartApp();
        var response = await msgBus.Request<GetBasicInformationsRequest, GetBasicInformationsResponse>(new GetBasicInformationsRequest());

        Assert.NotEmpty(response.Navigation);
    }

    [Fact]
    public async Task GetPageData()
    {
        var msgBus = await new CreateTagebuchSharpStub().StartApp();
        var response = await msgBus.Request<GetPageDataRequest, GetPageDataResponse>(new GetPageDataRequest("contact"));

        Assert.NotEmpty(response.Page.Title);
        Assert.NotEmpty(response.Page.Html);
        Assert.NotEmpty(response.Page.Slug);
        Assert.Empty(response.Page.Tags);
    }

    [Fact]
    public async Task GetPostDatasWithoutData()
    {
        var msgBus = await new CreateTagebuchSharpStub().StartApp();
        var response = await msgBus.Request<GetAllPostsRequest, GetAllPostsResponse>(new(1, 15, false));

        Assert.Equal(1, response.CurrentPage);
        Assert.Equal(1, response.TotalPages);
        Assert.NotEmpty(response.Posts);
        var posts = response.Posts;
        Assert.All(posts, p =>
        {
            Assert.NotEmpty(p.Title);
            Assert.NotEmpty(p.Slug);
            Assert.NotEmpty(p.Excerpt);
            Assert.NotEqual(default(DateTime), p.PublishedAt);
            Assert.NotEqual(default(DateTime), p.UpdatedAt);
            Assert.Empty(p.Html ?? string.Empty);
            Assert.NotEmpty(p.Tags);
        });
    }

    [Fact]
    public async Task GetPostDatasWithData()
    {
        var msgBus = await new CreateTagebuchSharpStub().StartApp();
        var response = await msgBus.Request<GetAllPostsRequest, GetAllPostsResponse>(new(1, 15, true));

        Assert.Equal(1, response.CurrentPage);
        Assert.Equal(1, response.TotalPages);
        Assert.NotEmpty(response.Posts);
        var posts = response.Posts;
        Assert.All(posts, p =>
        {
            Assert.NotEmpty(p.Title);
            Assert.NotEmpty(p.Slug);
            Assert.NotEmpty(p.Excerpt);
            Assert.NotEqual(default(DateTime), p.PublishedAt);
            Assert.NotEqual(default(DateTime), p.UpdatedAt);
            Assert.NotEmpty(p.Html ?? string.Empty);
            Assert.NotEmpty(p.Tags);
        });
    }

    [Fact]
    public async Task GetPostData()
    {
        var msgBus = await new CreateTagebuchSharpStub().StartApp();
        var response = await msgBus.Request<GetPostDataRequest, GetPostDataResponse>(new("coming-soon"));

        Assert.NotNull(response.Page);
        var p = response.Page;

        Assert.NotEmpty(p.Title);
        Assert.NotEmpty(p.Slug);
        Assert.NotEmpty(p.Excerpt);
        Assert.NotEqual(default(DateTime), p.PublishedAt);
        Assert.NotEqual(default(DateTime), p.UpdatedAt);
        Assert.NotEmpty(p.Html ?? string.Empty);
        Assert.NotEmpty(p.Tags);
    }

}
