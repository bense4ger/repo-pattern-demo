using System;

namespace RepositoryDemo.Data.Entities
{
    public class Game : IEntity
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }
}