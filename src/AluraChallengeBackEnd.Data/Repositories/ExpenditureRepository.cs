namespace AluraChallengeBackEnd.Data.Repositories;

public class ExpenditureRepository : Repository<Expenditure>, IExpenditureRepository
{
    public ExpenditureRepository(AppDbContext db) : base(db) { }
}