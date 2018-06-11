using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using RepositoryDemo.Data.Data;
using RepositoryDemo.Data.Entities;
using Xunit;

namespace RepositoryDemo.Data.Test.Data
{
    public class JsonContextTests
    {
        public JsonContextTests()
        {
            Id = Guid.NewGuid();

            var data = new List<Game>
            {
                new Game {Id = Id, Name = "Foo"},
                new Game {Id = Guid.NewGuid(), Name = "Bar"}
            };
            
            MockFileReader = new Mock<IFileReader>();
            MockFileReader.Setup(m => m.Configure(It.IsAny<string>())).Verifiable();
            MockFileReader.Setup(m => m.ReadText()).Returns(JsonConvert.SerializeObject(data));
            
            Sut = new JsonContext<Game>(MockFileReader.Object);
        }
        
        private Mock<IFileReader> MockFileReader { get; set; }
        private Guid Id { get; set; }
        private JsonContext<Game> Sut { get; set; }
        
        [Fact]
        public void FindById_ReturnsOne()
        {
            var result = Sut.FindById(Id);

            result.Id.Should().Be(Id);
            result.Name.Should().Be("Foo");
        }

        [Fact]
        public void Find_ReturnsIfData()
        {
            var result = Sut.Find(g => g.Name == "Bar");

            result.Should().HaveCount(1);
        }

        [Fact]
        public void Find_ReturnsEmptyIfNoData()
        {
            var result = Sut.Find(g => g.Name == "Nope");

            result.Should().BeEmpty();
        }
    }
}