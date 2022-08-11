namespace AluraChallengeBackEnd.Data.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    protected readonly AppDbContext Db;
    protected readonly DbSet<TEntity> DbSet;

    public IUnitOfWork UnitOfWork { get; }

    public Repository(AppDbContext db)
    {
        Db = db;
        DbSet = Db.Set<TEntity>();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await DbSet.ToListAsync();

    public virtual async Task<TEntity> GetAsync(Guid id) => await DbSet.FindAsync(id);

    public virtual void Save(TEntity entity) =>  DbSet.AddAsync(entity);

    public virtual void Edit(TEntity entity) => Db.Entry(entity).State = EntityState.Modified;

    public virtual void Delete(Guid id) => Db.Remove(id);

    public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate) => 
        await DbSet.AsNoTracking().Where(predicate).ToListAsync();

    public void Dispose() => Db?.Dispose();
}