using DMS.Model.DMSModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Model.GenericRepository
{
    /// <summary>
    /// GenericRepository
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class GenericRepository<TEntity> where TEntity : class
    {
        #region Private member variables...

        /// <summary>
        /// The context{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
        /// </summary>
        internal DMSEntities Context;

        /// <summary>
        /// The database set{CC2D43FA-BBC4-448A-9D0B-7B57ADF2655C}
        /// </summary>
        internal DbSet<TEntity> DbSet;

        #endregion

        #region Public Constructor...
        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public GenericRepository(DMSEntities context)
        {
            this.Context = context;
            this.DbSet = context.Set<TEntity>();
        }
        #endregion

        #region Public member methods...

        /// <summary>
        /// generic Get method for Entities
        /// </summary>
        /// <returns>TEntity</returns>
        public virtual IEnumerable<TEntity> Get()
        {
            IQueryable<TEntity> query = DbSet;
            return query.ToList();
        }

        /// <summary>
        /// Generic get method on the basis of id for Entities.
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>TEntity</returns>
        public virtual TEntity GetByID(object id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// generic Insert method for the entities
        /// </summary>
        /// <param name="entity">entity</param>
        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        /// <summary>
        /// Generic Delete method for the entities
        /// </summary>
        /// <param name="id">id</param>
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// Generic Delete method for the entities
        /// </summary>
        /// <param name="entityToDelete">entityToDelete</param>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }

            DbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// Generic update method for the entities
        /// </summary>
        /// <param name="entityToUpdate">entityToUpdate</param>
        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
            ////Context.SaveChanges();
        }

        /// <summary>
        /// generic method to get many record on the basis of a condition.
        /// </summary>
        /// <param name="where">where</param>
        /// <returns>TEntity</returns>
        public virtual IEnumerable<TEntity> GetMany(Func<TEntity, bool> where)
        {
            return DbSet.Where(where).ToList();
        }

        /// <summary>
        /// generic method to get many record on the basis of a condition but query able.
        /// </summary>
        /// <param name="where">where</param>
        /// <returns>TEntity</returns>
        public virtual IQueryable<TEntity> GetManyQueryable(Func<TEntity, bool> where)
        {
            return DbSet.Where(where).AsQueryable();
        }

        /// <summary>
        /// generic get method , fetches data for the entities on the basis of condition.
        /// </summary>
        /// <param name="where">where</param>
        /// <returns>TEntity</returns>
        public TEntity Get(Func<TEntity, Boolean> where)
        {
            return DbSet.Where(where).FirstOrDefault<TEntity>();
        }

        /// <summary>
        /// generic delete method , deletes data for the entities on the basis of condition.
        /// </summary>
        /// <param name="where">where</param>
        public void Delete(Func<TEntity, Boolean> where)
        {
            IQueryable<TEntity> objects = DbSet.Where<TEntity>(where).AsQueryable();
            foreach (TEntity obj in objects)
            {
                DbSet.Remove(obj);
            }
        }

        /// <summary>
        /// generic method to fetch get the required table
        /// </summary>
        /// <returns>TEntity</returns>
        public virtual IQueryable<TEntity> Table()
        {
            return DbSet;
        }

        /// <summary>
        /// generic method to fetch all the records from database
        /// </summary>
        /// <returns>TEntity</returns>
        public virtual IEnumerable<TEntity> GetAll()
        {
            return DbSet.ToList();
        }

        /// <summary>
        /// Include multiple
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <param name="include">include</param>
        /// <returns>TEntity</returns>
        public IQueryable<TEntity> GetWithInclude(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate, params string[] include)
        {
            IQueryable<TEntity> query = this.DbSet;
            query = include.Aggregate(query, (current, inc) => current.Include(inc));
            return query.Where(predicate);
        }

        /// <summary>
        /// Include multiple
        /// </summary>
        /// <param name="predicate">predicate</param>
        /// <returns>TEntity</returns>
        public IQueryable<TEntity> GetWithPredicate(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            IQueryable<TEntity> query = this.DbSet;
            return query.Where(predicate);
        }

        /// <summary>
        /// Generic method to check if entity exists
        /// </summary>
        /// <param name="primaryKey">primaryKey</param>
        /// <returns>boolean</returns>
        public bool Exists(object primaryKey)
        {
            return DbSet.Find(primaryKey) != null;
        }

        /// <summary>
        /// Gets a single record by the specified criteria (usually the unique identifier)
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record that matches the specified criteria</returns>
        public TEntity GetSingle(Func<TEntity, bool> predicate)
        {
            return DbSet.Single<TEntity>(predicate);
        }

        /// <summary>
        /// The first record matching the specified criteria
        /// </summary>
        /// <param name="predicate">Criteria to match on</param>
        /// <returns>A single record containing the first record matching the specified criteria</returns>
        public TEntity GetFirst(Func<TEntity, bool> predicate)
        {
            return DbSet.First<TEntity>(predicate);
        }       
        #endregion
    }
}
