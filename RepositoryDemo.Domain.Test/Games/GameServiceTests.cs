using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using FluentAssertions;
using Moq;
using RepositoryDemo.Data.Data;
using RepositoryDemo.Data.Entities;
using RepositoryDemo.Domain.Games.Models;
using RepositoryDemo.Domain.Games.Profiles;
using RepositoryDemo.Domain.Games.Services;
using RepositoryDemo.Domain.Games.Services.Contracts;
using Xunit;

namespace RepositoryDemo.Domain.Test.Games
{
    public class GameServiceTests
    {
        public GameServiceTests()
        {
            Mapper.Reset();
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<GameProfile>();
            });

            var data = new List<Game>
            {
                new Game {Id = Guid.NewGuid(), Type = "Type1", Name = "Foo"},
                new Game {Id = Guid.NewGuid(), Type = "Type2", Name = "Bar"}
            };
            
            var mockRepo = new Mock<IRepository<Game>>();
            mockRepo.Setup(m => m.Get(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns<Expression<Func<Game, bool>>>(expr => data.Where(expr.Compile()).ToList());
            
            Sut = new GameService(mockRepo.Object);
        }
        
        private IGameService Sut { get; }

        [Fact]
        public void GetGamesByType_ReturnsData_IfTypePresent()
        {
            var result = Sut.GetGamesByType("Type1");

            result.Should().HaveCount(1);
            result.First().Should().BeOfType<GameModel>();
        }

        [Fact]
        public void GetGamesByType_ReturnsEmpty_IfTypeNotPresent()
        {
            var result = Sut.GetGamesByType("Type3");

            result.Should().HaveCount(0);
        }
    }
}