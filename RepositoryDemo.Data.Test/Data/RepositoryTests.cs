using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentAssertions;
using Moq;
using RepositoryDemo.Data.Data;
using RepositoryDemo.Data.Entities;
using Xunit;

namespace RepositoryDemo.Data.Test.Data
{
    public class RepositoryTests
    {
        public RepositoryTests()
        {
            PassingId = Guid.NewGuid();
            FailingId = Guid.NewGuid();

            var entities = new List<Game> {new Game {Id = PassingId, Name = "Foo"}};
            
            var mockContext = new Mock<IDataContext<Game>>();

            mockContext.Setup(m => m.FindById(It.Is<Guid>(g => g == PassingId))).Returns(entities.First());
            mockContext.Setup(m => m.FindById(It.Is<Guid>(g => g == FailingId))).Returns<Game>(null);
            
            mockContext.Setup(m => m.Find(It.IsAny<Expression<Func<Game, bool>>>()))
                .Returns<Expression<Func<Game, bool>>>(exp => entities.Where(exp.Compile()).ToList());
            
            Sut = new Repository<Game>(mockContext.Object);
        }
        
        private Guid PassingId { get; }
        private Guid FailingId { get; }
        
        private IRepository<Game> Sut { get; }

        [Fact]
        public void Get_ReturnsEntityIfIdPresent()
        {
            var result = Sut.Get(PassingId);

            result.Should().BeOfType<Game>();
            result.Name.Should().Be("Foo");
        }

        [Fact]
        public void Get_ReturnsNullIfIdNotPresent()
        {
            var result = Sut.Get(FailingId);

            result.Should().BeNull();
        }

        [Fact]
        public void Get_ReturnsEntityIfPresent()
        {
            var result = Sut.Get(g => g.Name == "Foo");
            
            result.Should().HaveCount(1);
        }

        [Fact]
        public void Get_ReturnsNullIfNotPresent()
        {
            var result = Sut.Get(g => g.Name == "Nope");

            result.Should().HaveCount(0);
        }
    }
}