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
    }

    [Fact]
    public async Task GetPostDatas()
    {
        throw new NotImplementedException();
        var msgBus = await new CreateTagebuchSharpStub().StartApp();
        var response = await msgBus.Request<GetPageDataRequest, GetPageDataResponse>(new GetPageDataRequest("contact"));

        Assert.NotEmpty(response.Page.Title);
        Assert.NotEmpty(response.Page.Html);
        Assert.NotEmpty(response.Page.Slug);
    }


}