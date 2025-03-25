using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Tests.Unit;

public class InMemoryDatabaseFixture : IDisposable
{
    private readonly DbContextOptions<DbCtx> _options;

    public InMemoryDatabaseFixture()
    {
        // Create in-memory database for testing
        _options = new DbContextOptionsBuilder<DbCtx>()
            .UseInMemoryDatabase("TestDb")
            .Options;
        using var context = new DbCtx(_options);
        context.Database.EnsureCreated();
    }

    public DbCtx GetContext()
    {
        return new DbCtx(_options);
    }

    public void AddEntity<T>(T entity) where T : class
    {
        using var context = GetContext();
        context.Set<T>().Add(entity);
        context.SaveChanges();
    }

    public void Dispose()
    {
        using var context = GetContext();
        context.Database.EnsureDeleted();
    }
}
