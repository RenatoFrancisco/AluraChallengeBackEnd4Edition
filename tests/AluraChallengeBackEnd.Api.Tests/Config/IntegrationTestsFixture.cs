namespace AluraChallengeBackEnd.Api.Tests.Config;

public class IntegrationTestsFixture<TStartup> : IDisposable where TStartup : class
{
    public ApiFactory<TStartup> Factory { get; init; }
    public HttpClient Client { get; init; }

    public IntegrationTestsFixture()
    {
        Factory = new ApiFactory<TStartup>();
        Client = Factory.CreateClient();
    }

    public void Dispose()
    {
        Factory?.Dispose();
        Client?.Dispose();
    }
}
