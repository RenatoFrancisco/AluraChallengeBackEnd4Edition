namespace AluraChallengeBackEnd.Core.Data;

public interface IUnitOfWork
{
    Task<bool> CommitAsync();
}