using RealityCS.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RealityCS.DataLayer
{
    public interface IGenericRepository<TEntity> where TEntity : RealitycsBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IList<TEntity> GetAll();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        IList<TEntity> GetAllMatched(Expression<Func<TEntity, bool>> match);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(object id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        TEntity Find(Expression<Func<TEntity, bool>> match);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IQueryable<TEntity> GetIQueryable();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        IList<TEntity> GetAllPaged(int pageIndex, int pageSize, out int totalCount);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int Count();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        object Insert(TEntity entity, bool saveChanges = false);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="saveChanges"></param>
        void Delete(object id, bool saveChanges = false);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChanges"></param>
        void Delete(TEntity entity, bool saveChanges = false);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChanges"></param>
        void Update(TEntity entity, bool saveChanges = false);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="key"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        TEntity Update(TEntity entity, object key, bool saveChanges = false);
        /// <summary>
        /// 
        /// </summary>
        void Commit();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<IList<TEntity>> GetAllAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        Task<IList<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> match);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(object id);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> match);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Task<int> CountAsync();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        Task<object> InsertAsync(TEntity entity, bool saveChanges = false);

        Task InsertAsync(IEnumerable<TEntity> entity, bool saveChanges = false);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        Task DeleteAsync(object id, bool saveChanges = false);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        Task DeleteAsync(TEntity entity, bool saveChanges = false);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        Task UpdateAsync(TEntity entity, bool saveChanges = false);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChanges"></param>
        /// <returns></returns>
        Task UpdateAsync(IList<TEntity> entity, bool saveChanges = false);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <summary>
        /// 
        /// </summary>
        void Dispose();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="saveChanges"></param>
        void Insert(IEnumerable<TEntity> entity, bool saveChanges = false);

            //
        IQueryable<TEntity> Table { get; }
        IQueryable<TEntity> TableNoTracking { get; }

    }
}
