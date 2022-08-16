namespace AluraChallengeBackEnd.Domain.Interfaces;

public interface IIncomeService
{
    Task<bool> SaveAsync(Income income);
    Task<bool> EditAsync(Income income);
}