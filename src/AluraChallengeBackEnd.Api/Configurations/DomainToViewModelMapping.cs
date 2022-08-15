namespace AluraChallengeBackEnd.Api.Configurations;

public class DomainToViewModelMapping : Profile
{
    public DomainToViewModelMapping()
    {
        CreateMap<Income, IncomeViewModel>();
        CreateMap<Expenditure, ExpenditureViewModel>();
    }
}