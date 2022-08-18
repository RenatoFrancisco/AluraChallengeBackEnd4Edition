namespace AluraChallengeBackEnd.Domain.Interfaces;

public interface IIncomeService
{
    Task<bool> CreateAsync(Income income);
    Task<bool> EditAsync(Income income);
}