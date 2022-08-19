namespace AluraChallengeBackEnd.Domain.Interfaces;

public interface IIncomeRepository : IRepository<Income>
{
    Task<IEnumerable<Income>> GetByDescriptionAndMonthAsync(string description, int month);

    Task<Income> GetByDescriptionAsync(string description);
}