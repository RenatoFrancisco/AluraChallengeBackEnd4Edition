namespace AluraChallengeBackEnd.Domain.Services;

public class IncomeService : ServiceBase, IIncomeService
{
    private readonly IIncomeRepository _incomeRepository;

    public IncomeService(IIncomeRepository incomeRepository, INotifier notifier) : base(notifier) =>
         _incomeRepository = incomeRepository;

    public async Task<bool> Save(Income income)
    {
        if (!await ExecuteValidations(income)) return false;
        _incomeRepository.Save(income);
        return true;
    }

    public async Task<bool> Edit(Income income)
    {
        if (!await ExecuteValidations(income)) return false;
        _incomeRepository.Edit(income);
        return true;
    }

    private async Task<bool> ExecuteValidations(Income income)
    {
        if (!ExecuteValidation(new IncomeValidation(), income)) return false;

        if (await Exists(income.Description, income.DateIncome.Month))
        {
            Notify($"Already exists an Income with the same Description '{income.Description}' and Month '{income.DateIncome.Month}' supplied.");
            return false;
        }

        return true;
    }

    private async Task<bool> Exists(string description, int month) =>
        (await _incomeRepository.GetByDescriptionAndMonth(description, month)).Any();
}