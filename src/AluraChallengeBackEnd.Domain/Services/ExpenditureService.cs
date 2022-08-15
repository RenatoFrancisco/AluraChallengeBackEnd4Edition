namespace AluraChallengeBackEnd.Domain.Services;

public class ExpenditureService : ServiceBase, IExpenditureService
{
    private readonly IExpenditureRepository _expenditureRepository;

    public ExpenditureService(IExpenditureRepository expenditureRepository, INotifier notifier) : base(notifier) =>
        _expenditureRepository = expenditureRepository;

    public async Task<bool> Edit(Expenditure Expenditure)
    {
        if (!await ExecuteValidations(Expenditure)) return false;
        _expenditureRepository.Save(Expenditure);
        return true;
    }

    public async Task<bool> Save(Expenditure Expenditure)
    {
        if (!await ExecuteValidations(Expenditure)) return false;
        _expenditureRepository.Edit(Expenditure);
        return true;
    }

    private async Task<bool> ExecuteValidations(Expenditure expenditure)
    {
        if (!ExecuteValidation(new ExpenditureValidation(), expenditure)) return false;

        if (await Exists(expenditure.Description, expenditure.DateExpenditure.Month))
        {
            Notify($"Already exists an Expenditure with the same Description '{expenditure.Description}' and Month '{expenditure.DateExpenditure.Month}' supplied.");
            return false;
        }

        return true;
    }

    private async Task<bool> Exists(string description, int month) => 
        (await _expenditureRepository.GetByDescriptionAndMonth(description, month)).Any();
}