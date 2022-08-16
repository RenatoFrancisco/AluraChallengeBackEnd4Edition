namespace AluraChallengeBackEnd.Core.Data;


public interface IRepository<TEntity> : IDisposable where TEntity : Entity 
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> GetAsync(Guid id);
    void Create(TEntity entity);
    void Edit(TEntity entity);
    void Delete(TEntity entity);
    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

    IUnitOfWork UnitOfWork { get; }
}