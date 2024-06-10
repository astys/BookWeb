namespace BookWeb.Application.Repositories;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    Task<bool> SaveChangesAsync();
    IQueryable<TEntity> GetAll();
    Task<TEntity?> GetByIdAsync(string id);
}