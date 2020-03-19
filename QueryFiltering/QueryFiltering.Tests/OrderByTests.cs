using QueryFiltering.Tests.Model;
using System.Linq;
using Xunit;

namespace QueryFiltering.Tests
{
    public class OrderByTests
    {
        [Fact]
        public void OrderIntValueByAscending_ImplicitSyntax_ReturnsOrderedElements()
        {
            IQueryable<TestObject> queryable = new[]
            {
                new TestObject(){IntValue = 3},
                new TestObject(){IntValue = 2},
                new TestObject(){IntValue = 1},
            }.AsQueryable();

            IQueryable<TestObject> expected = queryable.OrderBy(x => x.IntValue);
            IQueryable<TestObject> actual = queryable.ApplyQuery("$orderBy=IntValue");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OrderIntValueByAscending_ExplicitSyntax_ReturnsOrderedElements()
        {
            IQueryable<TestObject> queryable = new[]
            {
                new TestObject(){IntValue = 3},
                new TestObject(){IntValue = 2},
                new TestObject(){IntValue = 1}
            }.AsQueryable();

            IQueryable<TestObject> expected = queryable.OrderBy(x => x.IntValue);
            IQueryable<TestObject> actual = queryable.ApplyQuery("$orderBy=IntValue asc");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OrderIntValueByDescending_ReturnsOrderedElements()
        {
            IQueryable<TestObject> queryable = new[]
            {
                new TestObject(){IntValue = 1},
                new TestObject(){IntValue = 2},
                new TestObject(){IntValue = 3},
            }.AsQueryable();

            IQueryable<TestObject> expected = queryable.OrderByDescending(x => x.IntValue);
            IQueryable<TestObject> actual = queryable.ApplyQuery("$orderBy=IntValue desc");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OrderIntValueByAscendingThenOrderDoubleValueByDescending_ReturnsOrderedElements()
        {
            IQueryable<TestObject> queryable = new[]
            {
                new TestObject(){IntValue = 4, DoubleValue = 1},
                new TestObject(){IntValue = 3, DoubleValue = 6},
                new TestObject(){IntValue = 2, DoubleValue = 4},
                new TestObject(){IntValue = 2, DoubleValue = 3},
                new TestObject(){IntValue = 2, DoubleValue = 5},
                new TestObject(){IntValue = 2, DoubleValue = 2},
                new TestObject(){IntValue = 1, DoubleValue = 7},
            }.AsQueryable();

            IQueryable<TestObject> expected = queryable.OrderBy(x => x.IntValue).ThenByDescending(x => x.DoubleValue);
            IQueryable<TestObject> actual = queryable.ApplyQuery("$orderBy=IntValue, DoubleValue desc");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void OrderInnerObjectIntValueByAscendingThenOrderInnerObjectDoubleValueByDescending_ReturnsOrderedElements()
        {
            IQueryable<TestObject> queryable = new[]
            {
                new TestObject(){InnerObject = new InnerObject(){IntValue = 4}, DoubleValue = 1},
                new TestObject(){InnerObject = new InnerObject(){IntValue = 3}, DoubleValue = 6},
                new TestObject(){InnerObject = new InnerObject(){IntValue = 2}, DoubleValue = 4},
                new TestObject(){InnerObject = new InnerObject(){IntValue = 2}, DoubleValue = 3},
                new TestObject(){InnerObject = new InnerObject(){IntValue = 2}, DoubleValue = 5},
                new TestObject(){InnerObject = new InnerObject(){IntValue = 2}, DoubleValue = 2},
                new TestObject(){InnerObject = new InnerObject(){IntValue = 1}, DoubleValue = 7},
            }.AsQueryable();

            IQueryable<TestObject> expected = queryable.OrderBy(x => x.InnerObject.IntValue).ThenByDescending(x => x.DoubleValue);
            IQueryable<TestObject> actual = queryable.ApplyQuery("$orderBy=InnerObject/IntValue, DoubleValue desc");

            Assert.Equal(expected, actual);
        }
    }
}
