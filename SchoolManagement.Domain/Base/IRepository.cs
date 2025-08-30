namespace SchoolManagement.Domain.Base;

public interface IRepository<TEntity> where TEntity : Entity
{
    Task AddAsync(TEntity obj);
    Task<TEntity?> GetByIdAsync(Guid id,CancellationToken cancellationToken = default, bool includeDeletedValues = false);
    IQueryable<TEntity> GetQueryable(bool includeDeletedValues = false);
    void Update(TEntity obj);
    void Remove(TEntity obj);
    
}