using Hackathon.Fiap.Core.Abstractions;
using Hackathon.Fiap.Core.Aggregates.Contributors;
using Hackathon.Fiap.Core.Aggregates.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Hackathon.Fiap.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options, IDomainEventDispatcher? dispatcher) : IdentityDbContext<ApplicationUser>(options)
{
    private readonly IDomainEventDispatcher? _dispatcher = dispatcher;

    public DbSet<Contributor> Contributors => Set<Contributor>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        // ignore events if no dispatcher provided
        if (_dispatcher == null) return result;

        // dispatch events only if save was successful
        var entitiesWithEvents = ChangeTracker.Entries<HasDomainEventsBase>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Count > 0)
            .ToArray();

        await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

        return result;
    }

    public override int SaveChanges() =>
          SaveChangesAsync().GetAwaiter().GetResult();
}
