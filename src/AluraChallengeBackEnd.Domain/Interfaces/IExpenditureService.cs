namespace AluraChallengeBackEnd.Domain.Interfaces
{
    public interface IExpenditureService
    {
        Task<bool> SaveAsync(Expenditure Expenditure);
        Task<bool> EditAsync(Expenditure Expenditure);    
    }
}