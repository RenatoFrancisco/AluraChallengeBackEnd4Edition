
namespace AluraChallengeBackEnd.Data.Configurations;

public class IncomeConfiguration : IEntityTypeConfiguration<Income>
{
    public void Configure(EntityTypeBuilder<Income> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Description)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.Value)
            .IsRequired();

        builder.Property(p => p.DateIncome)
            .HasColumnType("Date")
            .IsRequired();
    }
}