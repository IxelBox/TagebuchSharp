namespace TagebuchGhost.IntegrationTests;

[Collection("Docker Compose Ghost")]
public class DockerSetupTests
{
    private readonly DockerComposeGhostFixture _compose;

    public DockerSetupTests(DockerComposeGhostFixture compose)
    {
        _compose = compose;
    }

    [Fact]
    public async Task DockerSetup()
    {
        //password: qwert12345
        HttpClient client = new HttpClient();
        var output = await client.GetStringAsync("http://localhost:8888/ghost");
        Assert.NotEmpty(output);
    }
}
