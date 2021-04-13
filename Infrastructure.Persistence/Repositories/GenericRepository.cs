using Application.Interfaces.Repositories;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories
{
    public class GenericRepository : IRepository
    {
        private readonly DbContext _context;

        private DbContext DbContext
        {
            get
            {
                return this._context;
            }
        }

        private UnitOfWork _unitOfWork;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                if (_unitOfWork == null)
                {
                    _unitOfWork = new UnitOfWork(this._context);
                }
                return _unitOfWork;
            }
        }

        public GenericRepository()
        {

        }

        public GenericRepository(BaseContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _context = context;
        }

        public async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            await DbContext.Set<TEntity>().AddAsync(entity);
        }

        public async Task AttachAsync<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            DbContext.Set<TEntity>().Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            IEnumerable<TEntity> records = await FindAsync(predicate);

            foreach (TEntity record in records)
            {
                await DeleteAsync(record);
            }
        }

        public async Task<IQueryable<TEntity>> GetAllAsync<TEntity>() where TEntity : class
        {
            try
            {
                return (await GetQueryAsync<TEntity>());
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<TEntity> GetAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return (await GetQueryAsync<TEntity>()).FirstOrDefault(predicate);
        }

        public async Task UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            DbContext.Set<TEntity>().Update(entity);

            //var keyName = DbContext.Model.FindEntityType(typeof(TEntity))
            //    .FindPrimaryKey()
            //    .Properties
            //    .Select(x => x.Name).Single();

            //var keyValue = entity.GetType().GetProperty(keyName).GetValue(entity, null);

            //var attachedObject = DbContext.ChangeTracker
            //    .Entries<TEntity>().FirstOrDefault(x => x.Metadata.FindPrimaryKey().Properties.First(y => y.Name == keyName) == keyValue);

            //if(attachedObject != null)
            //{
            //    attachedObject.State = EntityState.Detached;
            //}

            //DbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task<IEnumerable<TEntity>> FindAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return (await GetQueryAsync<TEntity>()).Where(predicate);
        }

        private async Task<IQueryable<TEntity>> GetQueryAsync<TEntity>() where TEntity : class
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>().AsNoTracking();
            return query;
        }

        private async Task DeleteAsync<TEntity>(TEntity entity) where TEntity : class
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            DbContext.Set<TEntity>().Remove(entity);
        }

    }
}
