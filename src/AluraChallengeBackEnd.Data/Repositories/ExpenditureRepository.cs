namespace AluraChallengeBackEnd.Data.Repositories;

public class ExpenditureRepository : Repository<Expenditure>, IExpenditureRepository
{
    public ExpenditureRepository(AppDbContext db) : base(db) { }

    public async Task<IEnumerable<Expenditure>> GetByDescriptionAndMonthAsync(string description, int month) =>
        await FindAsync(e => e.Description == description && e.DateExpenditure.Month == month);

    public Task<CategoryExpenditure> GetCategoryByDescriptionAsync(string description) =>
        Db.CategoriesExpenditure.FirstOrDefaultAsync(c => c.Description == description);

    public async Task<Expenditure> GetByDescriptionAsync(string description) =>
        (await FindAsync(e => e.Description == description)).FirstOrDefault();
}