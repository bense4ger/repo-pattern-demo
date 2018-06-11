using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RepositoryDemo.Domain.Games.Services.Contracts;
using RepositoryDemo.Web.Models.Games;

namespace RepositoryDemo.Web.Controllers
{
    public class GamesController : Controller
    {
        public GamesController(IGameService gameService)
        {
            GameService = gameService;
        }
        
        private IGameService GameService { get; }
        
        public IActionResult Index()
        {
            var models = GameService.GetGamesByType("Fun");
            var vm = Mapper.Map<ICollection<GameViewModel>>(models);

            return View(vm);
        }
    }
}