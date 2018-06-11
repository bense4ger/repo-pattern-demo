using AutoMapper;
using RepositoryDemo.Data.Entities;
using RepositoryDemo.Domain.Games.Models;

namespace RepositoryDemo.Domain.Games.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<Game, GameModel>();
            CreateMap<GameModel, Game>();
        }
    }
}