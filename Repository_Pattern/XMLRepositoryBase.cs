using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Repository_Domain;
using Repository_Source;

namespace Repository_Pattern
{
    // XMLRepositoryBase for handling generic XML repository operations.
    public class XMLRepositoryBase<TContext, TEntity, TKey> : IRepository<TEntity, TKey>
        where TContext : XMLSet<TEntity> where TEntity : class, IEntity<TKey>
    {
        private readonly XMLSet<TEntity> m_context;

        public XMLRepositoryBase(string fileName)
        {
            m_context = new XMLSet<TEntity>(fileName);
        }

        public bool Delete(TKey id)
        {
            try
            {
                var items = m_context.Data;
                var itemToRemove = items.FirstOrDefault(f => f.Id.Equals(id));

                if (itemToRemove != null)
                {
                    items.Remove(itemToRemove);
                    m_context.Data = items;
                    m_context.Save();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                var list = m_context.Data.AsQueryable().Where(predicate).ToList();
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public TEntity Get(TKey id)
        {
            try
            {
                var items = m_context.Data;
                return items.FirstOrDefault(f => f.Id.Equals(id));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public ICollection<TEntity> GetAll()
        {
            return m_context.Data;
        }

        public TKey Insert(TEntity model)
        {
            var list = m_context.Data.ToList();

            // Check if TKey is an int
            if (typeof(TKey) == typeof(int))
            {
                // Generate a new unique ID
                var maxId = list.Any() ? list.Max(e => (int)(object)e.Id) : 0;
                var newId = maxId + 1;

                // Use reflection to set the ID property on the model
                model.GetType().GetProperty("Id").SetValue(model, (TKey)(object)newId);
            }

            list.Add(model);
            m_context.Data = list;
            m_context.Save();
            return model.Id;
        }

        public bool Remove(TKey id)
        {
            return Delete(id);
        }

        public bool Update(TEntity model)
        {
            try
            {
                var items = m_context.Data.ToList();
                var existingItem = items.FirstOrDefault(f => f.Id.Equals(model.Id));

                if (existingItem != null)
                {
                    items.Remove(existingItem);
                    items.Add(model);
                    m_context.Data = items;
                    m_context.Save();
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
