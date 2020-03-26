using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Infrastructure.Domain.BaseEntities;
using Infrastructure.Utilities.Helpers;
using Infrastructure.Domain.DataBaseContext;
using System.Threading;

namespace Infrastructure.Domain.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
         where TEntity : class, IAggregateRoot
    {
        private readonly DbContextBase _context;

        public DbSet<TEntity> Entities { get; }
        public virtual IQueryable<TEntity> Table => Entities;
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();



        public Repository(DbContextBase context)
        {
            _context = context;
            Entities = _context.Set<TEntity>();
        }

        #region Async Method
        public virtual async Task<TEntity> GetByIdAsync(params object[] ids)
        {
            return await Entities.FindAsync(ids);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            AssertHelper.NotNull(entity, nameof(entity));
            await Entities.AddAsync(entity).ConfigureAwait(false);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            AssertHelper.NotNull(entities, nameof(entities));
            await Entities.AddRangeAsync(entities).ConfigureAwait(false);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            AssertHelper.NotNull(entity, nameof(entity));
            Entities.Update(entity);
        }

        public virtual async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            AssertHelper.NotNull(entities, nameof(entities));
            Entities.UpdateRange(entities);
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            AssertHelper.NotNull(entity, nameof(entity));
            Entities.Remove(entity);
        }

        public virtual async Task DeleteRangeAsync(IEnumerable<TEntity> entities)
        {
            AssertHelper.NotNull(entities, nameof(entities));
            Entities.RemoveRange(entities);
        }
        #endregion

        #region Sync Methods
        public virtual TEntity GetById(params object[] ids)
        {
            return Entities.Find(ids);
        }

        public virtual void Add(TEntity entity)
        {
            AssertHelper.NotNull(entity, nameof(entity));
            Entities.Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            AssertHelper.NotNull(entities, nameof(entities));
            Entities.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            AssertHelper.NotNull(entity, nameof(entity));
            Entities.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            AssertHelper.NotNull(entities, nameof(entities));
            Entities.UpdateRange(entities);
        }

        public virtual void Delete(TEntity entity)
        {
            AssertHelper.NotNull(entity, nameof(entity));
            Entities.Remove(entity);
        }

        public virtual void DeleteRange(IEnumerable<TEntity> entities)
        {
            AssertHelper.NotNull(entities, nameof(entities));
            Entities.RemoveRange(entities);
        }
        #endregion
        #region Attach & Detach
        public virtual void Detach(TEntity entity)
        {
            AssertHelper.NotNull(entity, nameof(entity));
            var entry = _context.Entry(entity);
            if (entry != null)
                entry.State = EntityState.Detached;
        }

        public virtual void Attach(TEntity entity)
        {
            AssertHelper.NotNull(entity, nameof(entity));
            if (_context.Entry(entity).State == EntityState.Detached)
                Entities.Attach(entity);
        }
        #endregion

        #region Explicit Loading
        public virtual async Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty)
            where TProperty : class
        {
            Attach(entity);

            var collection = _context.Entry(entity).Collection(collectionProperty);
            if (!collection.IsLoaded)
                await collection.LoadAsync().ConfigureAwait(false);
        }

        public virtual void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty)
            where TProperty : class
        {
            Attach(entity);
            var collection = _context.Entry(entity).Collection(collectionProperty);
            if (!collection.IsLoaded)
                collection.Load();
        }

        public virtual async Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty)
            where TProperty : class
        {
            Attach(entity);
            var reference = _context.Entry(entity).Reference(referenceProperty);
            if (!reference.IsLoaded)
                await reference.LoadAsync().ConfigureAwait(false);
        }

        public virtual void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty)
            where TProperty : class
        {
            Attach(entity);
            var reference = _context.Entry(entity).Reference(referenceProperty);
            if (!reference.IsLoaded)
                reference.Load();
        }
        #endregion

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void SaveChangesAsync()
        {
            _context.SaveChangesAsync();
        }

        public async Task SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveEntitiesAsync();
        }
    }
}
