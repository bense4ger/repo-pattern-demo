using FluentAssertions;
using RepositoryDemo.Data.Helpers;
using Xunit;
using Xunit.Sdk;

namespace RepositoryDemo.Data.Test.Helpers
{
    public class FileNameInferrerTests
    {
        [Fact]
        public void InferFileName_CorrectlyGetsTypeName()
        {
            var expected = "Foo.bar";
            var actual = typeof(Foo).InferFileName("bar");

            actual.Should().Be(expected);

        }
        
        private class Foo {}
    }
    
    
}