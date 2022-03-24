using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

using TodoMaui.AppCtx;
using TodoMaui.Entities;

namespace TodoMaui.DbContexts;

public class TodoMauiDbContext : DbContext
{
    public DbSet<TodoEntity> Todos { get; set; }

    public TodoMauiDbContext()
    {
    }

    public TodoMauiDbContext(DbContextOptions<TodoMauiDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options
            .UseSqlite(Options.AppDbConnectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        foreach (IMutableForeignKey fk in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            fk.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        IEnumerable<EntityEntry> entries = ChangeTracker
            .Entries()
            .Where(e => e.State == EntityState.Modified);

        foreach (EntityEntry entityEntry in entries)
        {
            entityEntry.Entity.GetType().GetProperty("Updated")?.SetValue(entityEntry.Entity, DateTime.UtcNow);
        }

        return base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        IEnumerable<EntityEntry> entries = ChangeTracker
            .Entries()
            .Where(e => e.State == EntityState.Modified);

        foreach (EntityEntry entityEntry in entries)
        {
            entityEntry.Entity.GetType().GetProperty("Updated")?.SetValue(entityEntry.Entity, DateTime.UtcNow);
        }

        return base.SaveChanges();
    }
}
