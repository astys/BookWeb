using BookWeb.Application.Database;
using BookWeb.Application.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookWeb.Infrastructure.Repositories;

public abstract class GenericRepository<TEntity>(DatabaseContext databaseContext) : IGenericRepository<TEntity> where TEntity : class
{
    protected readonly DbSet<TEntity> DbSet = databaseContext.Set<TEntity>();
    
    public async Task AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        var baseEntities = entities.ToList();
        
        if (baseEntities.Count == 0)
        {
            return;
        }
        
        await DbSet.AddRangeAsync(baseEntities);
    }

    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await databaseContext.SaveChangesAsync() > -1;
    }

    public IQueryable<TEntity> GetAll()
    {
        return DbSet;
    }

    public async Task<TEntity?> GetByIdAsync(string id)
    {
        return await DbSet.FindAsync(id);
    }
}