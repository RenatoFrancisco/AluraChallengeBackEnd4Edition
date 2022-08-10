namespace AluraChallengeBackEnd.Data.Repositories;

public class IncomeRepository : Repository<Income>, IIncomeRepository
{
    public IncomeRepository(AppDbContext db) : base(db) { }
}