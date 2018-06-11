using System;
using FluentAssertions;
using RepositoryDemo.Data.Helpers;
using Xunit;

namespace RepositoryDemo.Data.Test.Helpers
{
    public class CsvToEntityConverterTests
    {
        [Fact]
        public void ToEntity_ConvertsFromCsv()
        {
            var id = Guid.NewGuid();
            var expected = new Foo() {Id = id, Name = "Foo"};
            var actual = new[] {$"{id}", "Foo"}.ToEntity<Foo>(new[] {"Id", "Name"});

            actual.Should().BeEquivalentTo(expected);
        }

        private class Foo
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }
    }
}