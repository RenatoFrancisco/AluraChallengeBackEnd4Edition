namespace AluraChallengeBackEnd.Api.Configurations;

public class DomainToViewModelMapping : Profile
{
    public DomainToViewModelMapping()
    {
        CreateMap<Income, IncomeViewModel>();
        CreateMap<Expenditure, ExpenditureViewModel>()
            .ForMember(vm => vm.Category, e => e.MapFrom(e => e.CategoryExpenditure.Description));
    }
}