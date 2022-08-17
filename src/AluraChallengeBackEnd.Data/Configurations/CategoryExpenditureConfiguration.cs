namespace AluraChallengeBackEnd.Data.Configurations;

public class CategoryExpenditureConfiguration : IEntityTypeConfiguration<CategoryExpenditure>
{
    public void Configure(EntityTypeBuilder<CategoryExpenditure> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Description)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasMany(c => c.Expenditures)
            .WithOne(e => e.CategoryExpenditure);
    }
}