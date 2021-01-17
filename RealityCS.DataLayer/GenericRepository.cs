using RealityCS.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RealityCS.DataLayer.Context.BaseContext;
using RealityCS.DataLayer.Context.RealitycsClient;
using RealityCS.DataLayer.Context.RealitycsShared;
using RealityCS.DataLayer.Context.RealitycsEnumeration;
using RealityCS.SharedMethods;
using RealityCS.Core.Infrastructure;
using RealityCS.DataLayer.Context.KPIEntity;
using RealityCS.DataLayer.Context.GraphicalEntity;

namespace RealityCS.DataLayer
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : RealitycsBase
    {
        // Instance of the DbContext. Must be passed or injected.        
        private IRealitycsBaseContext Context { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public GenericRepository()
        {
            Context = GetContext();
        }


        private IRealitycsBaseContext GetContext()
        {
            if(typeof(TEntity).BaseType==typeof(RealitycsClientBase))
            {
                return RealitycsEngineContext.Current.Resolve<RealitycsClientContext>();
            }
            else if(typeof(TEntity).BaseType == typeof(RealitycsSharedBase))
            {
                return RealitycsEngineContext.Current.Resolve<RealitycsSharedContext>();
            }
            else if(typeof(TEntity).BaseType == typeof(RealitycsEnumerationBase))
            {
                return RealitycsEngineContext.Current.Resolve<RealitycsEnumerationContext>();
            }
            else if(typeof(TEntity).BaseType == typeof(RealyticsKPIBase) || typeof(TEntity).BaseType == typeof(RealyticsKPIDataBase))
            {
                return RealitycsEngineContext.Current.Resolve<RealitycsKPIContext>();//check with Piyush
            }
            else if (typeof(TEntity).BaseType == typeof(RealyticsGraphicalBase))
            {
                return RealitycsEngineContext.Current.Resolve<RealitycsGraphicalContext>();//check with Piyush
            }
            else
            {
                throw new RealitycsException("Invalid Class Type ");
            }
        }

        /// <summary>
        /// Gets a table
        /// </summary>
        public IQueryable<TEntity> Table => DbSet;

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public IQueryable<TEntity> TableNoTracking => DbSet.AsNoTracking();


        /// <summary>
        /// 
        /// </summary>
        //Internally re-usable DbSet instance.
        protected DbSet<TEntity> DbSet
        {
            get
            {
                if (_dbSet == null)
                    _dbSet = Context.Set<TEntity>();
                return _dbSet;
            }
        }
        private DbSet<TEntity> _dbSet;

        /// <summary>
        /// Rollback of entity changes and return full error message
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <returns>Error message</returns>
        protected string GetFullErrorTextAndRollbackEntityChanges(DbUpdateException exception)
        {
            //rollback entity changes
            if (Context is DbContext dbContext)
            {
                var entries = dbContext.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

                entries.ForEach(entry => entry.State = EntityState.Unchanged);
            }

            Context.SaveChanges();
            return exception.ToString();
        }

        #region Regular Members
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IList<TEntity> GetAll()
        {
            return this.DbSet.ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public IList<TEntity> GetAllMatched(Expression<Func<TEntity, bool>> match)
        {
            return this.DbSet.Where(match).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> queryable = this.DbSet;
            foreach (Expression<Func<TEntity, object>> includeProperty in includeProperties)
            {

                queryable = queryable.Include<TEntity, object>(includeProperty);
            }
            return queryable;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual TEntity GetById(object id)
        {
            return this.DbSet.Find(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public virtual TEntity Find(Expression<Func<TEntity, bool>> match)
        {
            return this.DbSet.SingleOrDefault(match);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IQueryable<TEntity> GetIQueryable()
        {
            return this.DbSet.AsQueryable<TEntity>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public virtual IList<TEntity> GetAllPaged(int pageIndex, int pageSize, out int totalCount)
        {
            totalCount = this.DbSet.Count();
            return this.DbSet.Skip(pageSize * pageIndex).Take(pageSize).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return this.DbSet.Count();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        public virtual object Insert(TEntity entity, bool saveChanges = false)
        {
            var rtn = this.DbSet.Add(entity);
            if (saveChanges)
            {
                Context.SaveChanges();
            }
            return rtn;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        public virtual void Insert(IEnumerable<TEntity> entity, bool saveChanges = false)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException();
                DbSet.AddRange(entity);
                if (saveChanges)
                {
                    Context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="saveChanges"></param>
        public virtual void Delete(object id, bool saveChanges = false)
        {
            var item = GetById(id);
            this.DbSet.Remove(item);
            if (saveChanges)
            {
                Context.SaveChanges();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChanges"></param>
        public virtual void Delete(TEntity entity, bool saveChanges = false)
        {
            this.DbSet.Attach(entity);
            this.DbSet.Remove(entity);
            if (saveChanges)
            {
                Context.SaveChanges();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChanges"></param>
        public virtual void Update(TEntity entity, bool saveChanges = false)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                DbSet.Update(entity);
                if(saveChanges)
                    Context.SaveChanges();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="key"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        public virtual TEntity Update(TEntity entity, object key, bool saveChanges = false)
        {
            if (entity == null)
                return null;
            var exist = this.DbSet.Find(key);
            if (exist != null)
            {
                DbSet.Update(entity);
                if (saveChanges)
                {
                    Context.SaveChanges();
                }
            }
            return exist;
        }
        /// <summary>
        /// 
        /// </summary>
        public virtual void Commit()
        {
            Context.SaveChanges();
        }
        #endregion

        #region Async Members
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IList<TEntity>> GetAllAsync()
        {
            return await this.DbSet.ToListAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public virtual async Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match)
        {
            return await this.DbSet.Where(match).ToListAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await this.DbSet.FindAsync(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match)
        {
            return await this.DbSet.FirstOrDefaultAsync(match);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<int> CountAsync()
        {
            return await this.DbSet.CountAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        public virtual async Task<object> InsertAsync(TEntity entity, bool saveChanges = false)
        {
            try
            {
                var rtn = await this.DbSet.AddAsync(entity);
                if (saveChanges)
                {
                    ////Debugging use.
                    //try
                    //{
                    await Context.SaveChangesAsync();
                    //}
                    //catch (Exception ex)
                    //{
                    //    var te = ex;
                    //}
                }
                return rtn;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task InsertAsync(IEnumerable<TEntity> entity, bool saveChanges = false)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException();
                await this.DbSet.AddRangeAsync(entity);
                if (saveChanges)
                {
                    await Context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(object id, bool saveChanges = false)
        {
            this.DbSet.Remove(GetById(id));
            if (saveChanges)
            {
                await Context.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        public virtual async Task DeleteAsync(TEntity entity, bool saveChanges = false)
        {
            this.DbSet.Attach(entity);
            this.DbSet.Remove(entity);
            if (saveChanges)
            {
                await Context.SaveChangesAsync();
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(TEntity entity, bool saveChanges = false)
        {

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                DbSet.Update(entity);
                if (saveChanges)
                    await Context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(IList<TEntity> entity, bool saveChanges = false)
        {

            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            try
            {
                DbSet.UpdateRange(entity);
                if (saveChanges)
                    await Context.SaveChangesAsync();
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        #endregion

        private bool disposed = false;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                   // Context.Dispose();
                }
                this.disposed = true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }        
    }
}
