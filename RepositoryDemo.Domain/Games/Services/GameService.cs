using System.Collections.Generic;
using AutoMapper;
using RepositoryDemo.Data.Data;
using RepositoryDemo.Data.Entities;
using RepositoryDemo.Domain.Games.Models;
using RepositoryDemo.Domain.Games.Services.Contracts;

namespace RepositoryDemo.Domain.Games.Services
{
    public class GameService : IGameService
    {
        public GameService(IRepository<Game> repository)
        {
            Repository = repository;
        }

        private IRepository<Game> Repository { get; }
        
        public ICollection<GameModel> GetGamesByType(string type)
        {
            var entities = Repository.Get(g => g.Type == type);

            return Mapper.Map<ICollection<GameModel>>(entities);
        }
    }
}