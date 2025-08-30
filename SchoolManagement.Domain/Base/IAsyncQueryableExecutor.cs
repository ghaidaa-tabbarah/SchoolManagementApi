using System.Linq.Expressions;

namespace SchoolManagement.Domain.Base;

public interface IAsyncQueryableExecutor
{
    #region Any/All

    Task<bool> AnyAsync<T>(
        IQueryable<T> queryable,
        CancellationToken cancellationToken = default);

    Task<bool> AnyAsync<T>(
        IQueryable<T> queryable,
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task<bool> AllAsync<T>(
        IQueryable<T> queryable,
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    #endregion

    #region Count/LongCount

    Task<int> CountAsync<T>(
        IQueryable<T> queryable,
        CancellationToken cancellationToken = default);


    Task<int> CountAsync<T>(
        IQueryable<T> queryable,
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);


    Task<long> LongCountAsync<T>(
        IQueryable<T> queryable,
        CancellationToken cancellationToken = default);


    Task<long> LongCountAsync<T>(
        IQueryable<T> queryable,
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    #endregion

    #region First/FirstOrDefault

    Task<T> FirstAsync<T>(
        IQueryable<T> queryable,
        CancellationToken cancellationToken = default);


    Task<T> FirstAsync<T>(
        IQueryable<T> queryable,
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);


    Task<T?> FirstOrDefaultAsync<T>(
        IQueryable<T> queryable,
        CancellationToken cancellationToken = default);


    Task<T?> FirstOrDefaultAsync<T>(
        IQueryable<T> queryable,
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    #endregion

    #region Last/LastOrDefault

    Task<T> LastAsync<T>(
        IQueryable<T> queryable,
        CancellationToken cancellationToken = default);


    Task<T> LastAsync<T>(
        IQueryable<T> queryable,
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);


    Task<T?> LastOrDefaultAsync<T>(
        IQueryable<T> queryable,
        CancellationToken cancellationToken = default);


    Task<T?> LastOrDefaultAsync<T>(
        IQueryable<T> queryable,
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    #endregion

    #region Single/SingleOrDefault

    Task<T> SingleAsync<T>(
        IQueryable<T> queryable,
        CancellationToken cancellationToken = default);


    Task<T> SingleAsync<T>(
        IQueryable<T> queryable,
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    Task<T?> SingleOrDefaultAsync<T>(
        IQueryable<T> queryable,
        CancellationToken cancellationToken = default);

    Task<T?> SingleOrDefaultAsync<T>(
        IQueryable<T> queryable,
        Expression<Func<T, bool>> predicate,
        CancellationToken cancellationToken = default);

    #endregion
    
    #region ToList/Array

    Task<List<T>> ToListAsync<T>(
        IQueryable<T> queryable,
        CancellationToken cancellationToken = default);


    Task<T[]> ToArrayAsync<T>(
        IQueryable<T> queryable,
        CancellationToken cancellationToken = default);
    
    

    #endregion
}