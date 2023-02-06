using ItemsMicroservice.Core.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ItemsMicroservice.Infrastructure.Database;

internal sealed class ItemsMicroserviceDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Color> Colors { get; set; }
    public DbSet<Item> Items { get; set; }

    public ItemsMicroserviceDbContext(DbContextOptions<ItemsMicroserviceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Item>(entity =>
        {
            entity.HasKey(entity => entity.Code);
            entity.HasIndex(entity => entity.Name).IsUnique();
        });

        base.OnModelCreating(builder);
    }
}
