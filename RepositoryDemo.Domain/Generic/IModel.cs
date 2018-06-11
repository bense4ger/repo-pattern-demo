using System;

namespace RepositoryDemo.Domain.Generic
{
    public interface IModel
    {
        Guid Id { get; set; }
    }
}