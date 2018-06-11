using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using RepositoryDemo.Data.Entities;

namespace RepositoryDemo.Data.Data
{
    public interface IDataContext<TEntity> where TEntity: IEntity
    {
        TEntity FindById(Guid id);
        ICollection<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        
        // A Context would also have insert, update and delete methods
    }
}