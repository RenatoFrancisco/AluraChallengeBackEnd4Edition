namespace AluraChallengeBackEnd.Domain.Interfaces;

public interface IExpenditureRepository : IRepository<Expenditure>
{
    Task<IEnumerable<Expenditure>> GetByDescriptionAndMonth(string description, int month);

    Task<CategoryExpenditure> GetCategoryByDescriptionAsync(string description);
}