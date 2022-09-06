namespace AluraChallengeBackEnd.Api;

public class StartupTest
{
    public IConfiguration Configuration { get; init; }

    public StartupTest(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
    }

    public void Configure(WebApplication app, IWebHostEnvironment environment)
    {
        if (app.Environment.IsDevelopment())
            app.UseDeveloperExceptionPage();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}