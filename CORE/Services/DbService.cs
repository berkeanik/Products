using CORE.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CORE.Services
{
    // Common database operations for the entity services.
    public abstract class DbService<TEntity> : Service, IDisposable where TEntity : Record, new()
    {
        private readonly DbContext _db;

        protected DbService(DbContext db)
        {
            _db = db;
        }

        // Can be overridden in the services to include relational data and to order the query.
        protected virtual IQueryable<TEntity> DbQuery()
        {
            return _db.Set<TEntity>().AsNoTracking();
        }

        protected TEntity DbSingle(int id)
        {
            return DbQuery().AsTracking().SingleOrDefault(entity => entity.Id == id);
        }

        protected TEntity DbSingle(Expression<Func<TEntity, bool>> predicate)
        {
            return DbQuery().AsTracking().SingleOrDefault(predicate);
        }

        protected virtual int DbSave()
        {
            return _db.SaveChanges();
        }

        protected void DbAdd(TEntity entity, bool save = true)
        {
            _db.Set<TEntity>().Add(entity);
            if (save)
                DbSave();
        }

        protected void DbUpdate(TEntity entity, bool save = true)
        {
            _db.Set<TEntity>().Update(entity);
            if (save)
                DbSave();
        }

        protected void DbRemove(TEntity entity, bool save = true)
        {
            _db.Set<TEntity>().Remove(entity);
            if (save)
                DbSave();
        }

        // Used for removing the relational many to many records.
        protected void DbRemove<TNavigationEntity>(List<TNavigationEntity> navigationEntities) where TNavigationEntity : Record, new()
        {
            _db.Set<TNavigationEntity>().RemoveRange(navigationEntities);
        }

        public void Dispose()
        {
            _db.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
