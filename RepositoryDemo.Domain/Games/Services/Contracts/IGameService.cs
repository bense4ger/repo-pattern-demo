using System.Collections.Generic;
using RepositoryDemo.Domain.Games.Models;
using RepositoryDemo.Domain.Generic;

namespace RepositoryDemo.Domain.Games.Services.Contracts
{
    public interface IGameService : IDomainService
    {
        ICollection<GameModel> GetGamesByType(string type);
    }
}