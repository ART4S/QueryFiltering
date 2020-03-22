using System.Collections.Generic;
using System.Linq;
using LinqToQuerystring.Tests.Model;
using Xunit;

namespace LinqToQuerystring.Tests
{
    public class SelectTests
    {
        [Fact]
        public void SelectOnePropertyFromEachElement_ReturnsElementsDictionaryCollectionWithOneProperty()
        {
            var queryable = new[]
            {
                new TestObject(){StringValue = "test", IntValue = 1},
            }.AsQueryable();

            KeyValuePair<string, object>[] expected = queryable.Select(x =>
                    new Dictionary<string, object>()
                    {
                        { nameof(x.StringValue), x.StringValue }
                    })
                .SelectMany(x => x)
                .ToArray();

            KeyValuePair<string, object>[] actual = ((IQueryable)queryable)
                .ApplyQuery("$select=StringValue")
                .Cast<Dictionary<string, object>>()
                .SelectMany(x => x)
                .ToArray();

            Assert.True(expected.SequenceEqual(actual));
        }
    }
}
