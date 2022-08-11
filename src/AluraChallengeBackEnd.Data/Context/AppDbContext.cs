namespace AluraChallengeBackEnd.Data.Context;

public class AppDbContext : DbContext
{
    public DbSet<Income> Incomes { get; set; }
    public DbSet<Expenditure> Expenditures { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}