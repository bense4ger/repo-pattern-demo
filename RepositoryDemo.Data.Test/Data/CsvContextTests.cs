using System;
using FluentAssertions;
using Moq;
using RepositoryDemo.Data.Data;
using RepositoryDemo.Data.Entities;
using Xunit;

namespace RepositoryDemo.Data.Test.Data
{
    public class CsvContextTests
    {
        public CsvContextTests()
        {
            Id = Guid.NewGuid();
            
            MockReader = new Mock<IFileReader>();
            MockReader.Setup(m => m.Configure(It.IsAny<string>())).Verifiable();
            MockReader
                .Setup(m => m.ReadLines())
                .Returns(new[] {"Id,Name", $"{Id},Foo", $"{Guid.NewGuid()},Bar"});
            
            
            Sut = new CsvContext<Game>(MockReader.Object);
        }
        
        private Mock<IFileReader> MockReader { get; }
        private Guid Id { get; }
        private CsvContext<Game> Sut { get; }
            
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