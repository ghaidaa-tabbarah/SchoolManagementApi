using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Base;

namespace SchoolManagement.Infrastructure.Base;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    private readonly DbSet<TEntity> _dbSet;

    public Repository(SchoolManagementDbContext context)
    {
        _dbSet = context.Set<TEntity>();
    }

    public async Task AddAsync(TEntity obj)
    {
        await _dbSet.AddAsync(obj);
    }

    public Task<TEntity?> GetByIdAsync(Guid id,CancellationToken cancellationToken = default, bool includeDeletedValues = false)
    {
        var query = _dbSet.AsQueryable();

        if (!includeDeletedValues)
            query = query.Where(entity => entity.IsDeleted == false);

        return query.SingleOrDefaultAsync(entity => entity.Id == id, cancellationToken: cancellationToken);
    }

    public IQueryable<TEntity> GetQueryable(bool includeDeletedValues = false)
    {
        var query = _dbSet.AsQueryable().AsNoTracking();

        if (!includeDeletedValues)
            query = query.Where(entity => entity.IsDeleted == false);

        return query;
    }

    public void Update(TEntity obj)
    {
        _dbSet.Update(obj);
    }

    public void Remove(TEntity obj)
    {
        _dbSet.Remove(obj);
    }
}