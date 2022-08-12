
namespace AluraChallengeBackEnd.Data.Configurations;

public class ExpenditureConfiguration : IEntityTypeConfiguration<Expenditure>
{
    public void Configure(EntityTypeBuilder<Expenditure> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Description)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Value)
            .IsRequired();

        builder.Property(p => p.DateExpenditure)
            .HasColumnType("Date")
            .IsRequired();
    }
}