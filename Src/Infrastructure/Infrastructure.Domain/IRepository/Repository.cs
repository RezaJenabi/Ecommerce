using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Infrastructure.Domain.BaseEntities;
using Infrastructure.Helpers;

namespace Infrastructure.Domain.IRepository
{
    public class Repository<TEntity> : IRepository<TEntity>
         where TEntity : class, IAggregateRoot
    {
        private readonly DbContext _unitOfWork;

        public DbSet<TEntity> Entities { get; }
        public virtual IQueryable<TEntity> Table => Entities;
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();



        public Repository(DbContext unitOfWork)
        {
            _unitOfWork = unitOfWork;
            Entities = _unitOfWork.Set<TEntity>();
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
            var entry = _unitOfWork.Entry(entity);
            if (entry != null)
                entry.State = EntityState.Detached;
        }

        public virtual void Attach(TEntity entity)
        {
            AssertHelper.NotNull(entity, nameof(entity));
            if (_unitOfWork.Entry(entity).State == EntityState.Detached)
                Entities.Attach(entity);
        }
        #endregion

        #region Explicit Loading
        public virtual async Task LoadCollectionAsync<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty)
            where TProperty : class
        {
            Attach(entity);

            var collection = _unitOfWork.Entry(entity).Collection(collectionProperty);
            if (!collection.IsLoaded)
                await collection.LoadAsync().ConfigureAwait(false);
        }

        public virtual void LoadCollection<TProperty>(TEntity entity, Expression<Func<TEntity, IEnumerable<TProperty>>> collectionProperty)
            where TProperty : class
        {
            Attach(entity);
            var collection = _unitOfWork.Entry(entity).Collection(collectionProperty);
            if (!collection.IsLoaded)
                collection.Load();
        }

        public virtual async Task LoadReferenceAsync<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty)
            where TProperty : class
        {
            Attach(entity);
            var reference = _unitOfWork.Entry(entity).Reference(referenceProperty);
            if (!reference.IsLoaded)
                await reference.LoadAsync().ConfigureAwait(false);
        }

        public virtual void LoadReference<TProperty>(TEntity entity, Expression<Func<TEntity, TProperty>> referenceProperty)
            where TProperty : class
        {
            Attach(entity);
            var reference = _unitOfWork.Entry(entity).Reference(referenceProperty);
            if (!reference.IsLoaded)
                reference.Load();
        }
        #endregion
        
        public void SaveChanges()
        {
            _unitOfWork.SaveChanges();
        }

        public void SaveChangesAsync()
        {
            _unitOfWork.SaveChangesAsync();
        }
    }
}
