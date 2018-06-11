using AutoMapper;
using RepositoryDemo.Domain.Games.Models;
using RepositoryDemo.Web.Models.Games;

namespace RepositoryDemo.Web.Profiles
{
    public class GameProfile : Profile
    {
        public GameProfile()
        {
            CreateMap<GameModel, GameViewModel>();
            CreateMap<GameViewModel, GameModel>();
        }
    }
}