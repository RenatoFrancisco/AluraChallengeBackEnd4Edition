namespace AluraChallengeBackEnd.Api.Tests.Config;

public class ApiFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseStartup<TStartup>();
        builder.UseEnvironment("Testing");
    }
}
