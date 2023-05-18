using Microsoft.EntityFrameworkCore;
using Pricord.Domain.Authentication;
using Pricord.Domain.BattleRecords;
using Pricord.Domain.Timelines;
using Pricord.Domain.Units;

namespace Pricord.Infrastructure.Persistence;

internal class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<BattleRecord> BattleRecords { get; set; } = null!;
    public DbSet<Timeline> Timelines { get; set; } = null!;
    public DbSet<Boss> Boss { get; set; } = null!;
    public DbSet<PlayableCharacter> PlayableCharacters { get; set; } = null!;

    public DbSet<Unit> Units { get; set; } = null!;
}