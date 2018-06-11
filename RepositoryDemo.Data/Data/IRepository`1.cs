using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using RepositoryDemo.Data.Entities;

namespace RepositoryDemo.Data.Data
{
    public interface IRepository<TEntity> : ICsvRepository where TEntity: IEntity
    {
        TEntity Get(Guid id);
        ICollection<TEntity> Get(Expression<Func<TEntity, bool>> predicate);
        
        // Repostiory would also have insert, update and delete methods
    }
}