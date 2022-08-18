namespace AluraChallengeBackEnd.Api.Configurations;

public class ViewModelToDomainMapping : Profile
{
    public ViewModelToDomainMapping()
    {
        CreateMap<IncomeViewModel, Income>()
            .ConstructUsing(vm => new Income(vm.Description, vm.Value, vm.DateIncome));

        CreateMap<ExpenditureViewModel, Expenditure>()
            .ConstructUsing(vm => new Expenditure(vm.Description, vm.Value, vm.DateExpenditure, vm.Category));
    }       
}