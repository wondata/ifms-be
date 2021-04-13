using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Interfaces.Repositories
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }

        Task<IQueryable<TEntity>> GetAllAsync<TEntity>() where TEntity : class;

        Task<TEntity> GetAsync<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;

        Task AddAsync<TEntity>(TEntity entity) where TEntity : class;

        Task AttachAsync<TEntity>(TEntity entity) where TEntity : class;

        Task DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> criteria) where TEntity : class;

        Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class;

        Task<IEnumerable<TEntity>> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;

    }

    public interface IUnitOfWork
    {
        bool IsInTransaction { get; }

        Task SaveChanges();

        Task SaveChanges(SaveOptions saveOptions);

        Task BeginTransaction();

        Task BeginTransaction(IsolationLevel isolationLevel);

        Task RollBackTransaction();

        Task CommitTransaction();
    }

    public enum SortOrder
    {
        Ascending = 0,
        Descending = 1
    }

    
}
