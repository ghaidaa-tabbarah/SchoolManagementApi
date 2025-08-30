 using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Base;

namespace SchoolManagement.Infrastructure.Base;

public class AsyncQueryableExecutor : IAsyncQueryableExecutor
{
    public Task<bool> AnyAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default)
    {
        return queryable.AnyAsync(cancellationToken);
    }

    public Task<bool> AnyAsync<T>(IQueryable<T> queryable, Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return queryable.AnyAsync(predicate, cancellationToken);
    }

    public Task<bool> AllAsync<T>(IQueryable<T> queryable, Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return queryable.AllAsync(predicate, cancellationToken);
    }

    public Task<int> CountAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default)
    {
        return queryable.CountAsync(cancellationToken);
    }

    public Task<int> CountAsync<T>(IQueryable<T> queryable, Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return queryable.CountAsync(predicate, cancellationToken);
    }

    public Task<long> LongCountAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default)
    {
        return queryable.LongCountAsync(cancellationToken);
    }

    public Task<long> LongCountAsync<T>(IQueryable<T> queryable, Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return queryable.LongCountAsync(predicate, cancellationToken);
    }

    public Task<T> FirstAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default)
    {
        return queryable.FirstAsync(cancellationToken);
    }

    public Task<T> FirstAsync<T>(IQueryable<T> queryable, Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return queryable.FirstAsync(predicate, cancellationToken);
    }

    public Task<T?> FirstOrDefaultAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default)
    {
        return queryable.FirstOrDefaultAsync(cancellationToken);
    }

    public Task<T?> FirstOrDefaultAsync<T>(IQueryable<T> queryable, Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return queryable.FirstOrDefaultAsync(predicate, cancellationToken);
    }

    public Task<T> LastAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default)
    {
        return queryable.LastAsync(cancellationToken);
    }

    public Task<T> LastAsync<T>(IQueryable<T> queryable, Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return queryable.LastAsync(predicate, cancellationToken);
    }

    public Task<T?> LastOrDefaultAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default)
    {
        return queryable.LastOrDefaultAsync(cancellationToken);
    }

    public Task<T?> LastOrDefaultAsync<T>(IQueryable<T> queryable, Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return queryable.LastOrDefaultAsync(predicate, cancellationToken);
    }

    public Task<T> SingleAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default)
    {
        return queryable.SingleAsync(cancellationToken);
    }

    public Task<T> SingleAsync<T>(IQueryable<T> queryable, Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return queryable.SingleAsync(predicate, cancellationToken);
    }

    public Task<T?> SingleOrDefaultAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default)
    {
        return queryable.SingleOrDefaultAsync(cancellationToken);
    }

    public Task<T?> SingleOrDefaultAsync<T>(IQueryable<T> queryable, Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default)
    {
        return queryable.SingleOrDefaultAsync(predicate, cancellationToken);
    }

    public Task<List<T>> ToListAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default)
    {
        return queryable.ToListAsync(cancellationToken);
    }

    public Task<T[]> ToArrayAsync<T>(IQueryable<T> queryable, CancellationToken cancellationToken = default)
    {
        return queryable.ToArrayAsync(cancellationToken);
    }
    
   
}