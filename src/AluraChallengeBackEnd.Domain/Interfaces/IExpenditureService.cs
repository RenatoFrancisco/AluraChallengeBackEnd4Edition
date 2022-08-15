namespace AluraChallengeBackEnd.Domain.Interfaces
{
    public interface IExpenditureService
    {
        Task<bool> Save(Expenditure Expenditure);
        Task<bool> Edit(Expenditure Expenditure);    
    }
}