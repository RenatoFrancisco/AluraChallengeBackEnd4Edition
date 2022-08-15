namespace AluraChallengeBackEnd.Data.Repositories;

public class IncomeRepository : Repository<Income>, IIncomeRepository
{
    public IncomeRepository(AppDbContext db) : base(db) { }

    public async Task<IEnumerable<Income>> GetByDescriptionAndMonth(string description, int month) =>
        await FindAsync(i => i.Description == description && i.DateIncome.Month == month);
}