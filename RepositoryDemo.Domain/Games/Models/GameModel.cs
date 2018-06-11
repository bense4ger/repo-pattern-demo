using System;
using RepositoryDemo.Domain.Generic;

namespace RepositoryDemo.Domain.Games.Models
{
    public class GameModel : IModel
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }
}