using System;

namespace RepositoryDemo.Data.Entities
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}