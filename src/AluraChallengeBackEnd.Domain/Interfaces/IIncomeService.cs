namespace AluraChallengeBackEnd.Domain.Interfaces;

public interface IIncomeService
{
    Task<bool> Save(Income income);
    Task<bool> Edit(Income income);
}