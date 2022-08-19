namespace AluraChallengeBackEnd.Domain.Interfaces;

public interface IExpenditureRepository : IRepository<Expenditure>
{
    Task<IEnumerable<Expenditure>> GetByDescriptionAndMonthAsync(string description, int month);

    Task<CategoryExpenditure> GetCategoryByDescriptionAsync(string description);

    Task<Expenditure> GetByDescriptionAsync(string description);
}