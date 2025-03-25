using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Tests.Integration;

public class IntegrationTestFixture : IDisposable
{
    public HttpClient Client { get; }
    private readonly WebApplicationFactory<Program> _factory;

    public IntegrationTestFixture()
    {
        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    // Remove real DbContext
                    var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<DbCtx>));
                    if (descriptor != null) services.Remove(descriptor);

                    // Add in-memory DbContext
                    services.AddDbContext<DbCtx>(options =>
                        options.UseInMemoryDatabase("TestDb"));
                });
            });

        Client = _factory.CreateClient();
    }

    public void Dispose()
    {
        Client.Dispose();
        _factory.Dispose();
    }

    public IServiceScope CreateScope()
    {
        return _factory.Services.CreateScope();
    }
}
