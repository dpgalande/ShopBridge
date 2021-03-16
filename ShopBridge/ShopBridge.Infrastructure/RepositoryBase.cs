using ShopBridge.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShopBridge.Infrastructure
{
    public class RepositoryBase<TEntity> : IRepository<TEntity>
         where TEntity : class
    {
        private readonly ShopBridgeDbContext _dataContext;

        private IDbSet<TEntity> Dbset => _dataContext.Set<TEntity>();

        public RepositoryBase()
        {
            _dataContext = new ShopBridgeDbContext();
        }

        public  int Commit()
        {
            return _dataContext.SaveChanges();
        }

        public IEnumerable<TEntity> GetAll()
        {
            return Dbset.AsEnumerable();
        }

        public TEntity GetById(int id)
        {
            return Dbset.Find(id);
        }

        public void Insert(TEntity entity)
        {
            Dbset.Add(entity);
        }

        public void Update(TEntity entity, bool checkMaskFieldValue = true)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");
            Dbset.Attach(entity);
            _dataContext.Entry(entity).State = EntityState.Modified;
            if (checkMaskFieldValue)
            {
                var currentValues = _dataContext.Entry(entity).CurrentValues;
                foreach (string propertyName in currentValues.PropertyNames)
                {
                    var current = currentValues[propertyName];
                    var maskedValue = (current != null) ? GetMaskedValueBasedOnType(current.GetType()) : null;
                    if (current != null && maskedValue != null && maskedValue.Equals(current))
                    {
                        _dataContext.Entry(entity).Property(propertyName).IsModified = false;
                    }
                    else
                    {
                        _dataContext.Entry(entity).Property(propertyName).IsModified = true;
                    }
                }
            }
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null)
            {
                throw new ObjectNotFoundException("entity");
            }
            Dbset.Remove(entity);
        }

        public void Delete(TEntity entity)
        {
            Dbset.Remove(entity);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> @where)
        {
            return Dbset.FirstOrDefault(where);
        }

        public IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return Dbset.Where(where).ToList();
        }

        public object GetMaskedValueBasedOnType(Type type)
        {
            if (type == typeof(string))
            {
                return "*****";
            }
            else if (type == typeof(int) || type == typeof(double) || type == typeof(float) || type == typeof(int?) || type == typeof(double?) || type == typeof(float?))
            {
                return -999999;
            }
            else if (type == typeof(DateTime))
            {
                return DateTime.MinValue;
            }
            else if (type == typeof(TimeSpan))
            {
                return TimeSpan.MaxValue;
            }
            return null;
        }

    }
}