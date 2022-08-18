namespace AluraChallengeBackEnd.Domain.Interfaces
{
    public interface IExpenditureService
    {
        Task<bool> CreateAsync(Expenditure Expenditure);
        Task<bool> EditAsync(Expenditure Expenditure);    
    }
}