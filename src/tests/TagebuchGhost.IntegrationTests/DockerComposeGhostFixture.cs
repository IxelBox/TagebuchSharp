using System.Diagnostics;

namespace TagebuchGhost.IntegrationTests;

[CollectionDefinition("Docker Compose Ghost")]
public class DockerComposeGhostCollection : ICollectionFixture<DockerComposeGhostFixture>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}
public class DockerComposeGhostFixture : IDisposable
{
    private static string GetDockerComposeDirectory()
    {
        return ".";
    }

    public DockerComposeGhostFixture()
    {
        var processStartInfo = new ProcessStartInfo("docker.exe", "compose up -d");
        processStartInfo.WorkingDirectory = GetDockerComposeDirectory();
        processStartInfo.UseShellExecute = false;
        processStartInfo.CreateNoWindow = true;
        var process = new Process();
        process.StartInfo = processStartInfo;
        process.Start();
        Task.Delay(10000).Wait();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            var processStartInfo = new ProcessStartInfo("docker.exe", "compose down");
            processStartInfo.WorkingDirectory = GetDockerComposeDirectory();
            Process.Start(processStartInfo);
            Task.Delay(1000).Wait();
        }
    }
}
