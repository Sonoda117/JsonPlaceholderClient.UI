using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace JsonPlaceholderClient.Tests;

//Reference: https://blog.jetbrains.com/dotnet/2023/10/24/how-to-use-testcontainers-with-dotnet-unit-tests/
public sealed class JsonPlaceholderFixture : IAsyncLifetime
{
    private const int DockerContainerPort = 3000;
    private const string DockerImageName = "svenwal/jsonplaceholder";

    private readonly IContainer _Container;

    public JsonPlaceholderFixture()
    {
        _Container = new ContainerBuilder()
            .WithImage(DockerImageName)
            .WithPortBinding(DockerContainerPort, true)
            .WithWaitStrategy(Wait.ForUnixContainer().UntilPortIsAvailable(DockerContainerPort))
            .Build();
    }

    public string Address { get; private set; } = string.Empty;

    public async Task InitializeAsync()
    {
        await _Container.StartAsync();
        Address = $"http://localhost:{_Container.GetMappedPublicPort(DockerContainerPort)}/";
    }
    public Task DisposeAsync() => _Container.DisposeAsync().AsTask();
}