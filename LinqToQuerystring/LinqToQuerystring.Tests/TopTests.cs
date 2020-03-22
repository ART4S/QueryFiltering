using System.Linq;
using LinqToQuerystring.Tests.Model;
using Xunit;

namespace LinqToQuerystring.Tests
{
    public class TopTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        [InlineData(-1)]
        public void SelectSomeCountTopElements_ReturnsSelectedElements(int count)
        {
            IQueryable<TestObject> queryable = new[]
            {
                new TestObject(),
                new TestObject(),
            }.AsQueryable();

            IQueryable<TestObject> expected = queryable.Take(count);
            IQueryable<TestObject> actual = queryable.ApplyQuery($"$top={count}");

            Assert.Equal(expected, actual);
        }
    }
}
