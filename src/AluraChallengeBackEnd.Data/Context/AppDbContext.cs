namespace AluraChallengeBackEnd.Data.Context;

public class AppDbContext : DbContext, IUnitOfWork
{
    public DbSet<Income> Incomes { get; set; }
    public DbSet<Expenditure> Expenditures { get; set; }
    public DbSet<CategoryExpenditure> CategoriesExpenditure { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

     public async Task<bool> CommitAsync()
     {
        const string fieldName = "CreatedOn";

        foreach (var entry in ChangeTracker
            .Entries()
            .Where(e => e.Entity.GetType()
                .GetProperty(fieldName) is not null))
        {
            if (entry.State == EntityState.Added)
                entry.Property(fieldName).CurrentValue = DateTime.Now;

            if (entry.State == EntityState.Modified)
                entry.Property(fieldName).IsModified = false;
        }

        return await base.SaveChangesAsync() > 0;
     }
}