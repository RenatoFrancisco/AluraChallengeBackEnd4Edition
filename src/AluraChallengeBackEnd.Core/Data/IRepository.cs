namespace AluraChallengeBackEnd.Core.Data;


public interface IRepository<TEntity> : IDisposable where TEntity : Entity 
{
    Task<IEnumerable<TEntity>> GetAll();
    Task<TEntity> Get(Guid id);
    Task Save(TEntity entity);
    Task Edit(TEntity entity);
    Task Delete(Guid id);
    Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);

    IUnitOfWork UnitOfWork { get; }
}