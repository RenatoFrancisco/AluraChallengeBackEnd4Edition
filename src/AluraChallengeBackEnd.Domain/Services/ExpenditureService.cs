namespace AluraChallengeBackEnd.Domain.Services;

public class ExpenditureService : ServiceBase, IExpenditureService
{
    private readonly IExpenditureRepository _expenditureRepository;

    public ExpenditureService(IExpenditureRepository expenditureRepository, INotifier notifier) : base(notifier) =>
        _expenditureRepository = expenditureRepository;

    public async Task<bool> SaveAsync(Expenditure expenditure)
    {
        if (!await ExecuteValidations(expenditure)) return false;
        _expenditureRepository.Save(expenditure);
        return await CommitAsync();
    }

    public async Task<bool> EditAsync(Expenditure expenditure)
    {
        if (!await ExecuteValidations(expenditure)) return false;
        _expenditureRepository.Edit(expenditure);
        return await CommitAsync();
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

    private async Task<bool> CommitAsync() => await _expenditureRepository.UnitOfWork.CommitAsync();
}