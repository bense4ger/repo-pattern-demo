using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using RepositoryDemo.Data.Entities;

namespace RepositoryDemo.Data.Data
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : IEntity
    {
        public Repository(IDataContext<TEntity> context)
        {
            Context = context;
        }
        
        private IDataContext<TEntity> Context { get; }
        
        public TEntity Get(Guid id)
        {
            return Context.FindById(id);
        }

        public ICollection<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Find(predicate);
        }
    }
}