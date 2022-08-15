namespace AluraChallengeBackEnd.Domain.Interfaces;

public interface IIncomeRepository : IRepository<Income>
{
    Task<IEnumerable<Income>> GetByDescriptionAndMonth(string description, int month);
}