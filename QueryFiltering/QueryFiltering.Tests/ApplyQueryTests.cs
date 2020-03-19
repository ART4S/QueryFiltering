using System;
using System.Linq;
using Xunit;

namespace QueryFiltering.Tests
{
    public class ApplyQueryTests
    {
        [Fact]
        public void ApplyEmptyQuery_ReturnsSameQueryable()
        {
            IQueryable<object> queryable = new object[] { }.AsQueryable();

            IQueryable<object> expected = queryable;
            IQueryable<object> actual = queryable.ApplyQuery("");

            Assert.Same(expected, actual);
        }

        [Fact]
        public void ApplyNullQuery_ThrowsArgumentNullException()
        {
            IQueryable<object> queryable = new object[] { }.AsQueryable();

            Assert.Throws<ArgumentNullException>(() => queryable.ApplyQuery(null));
        }
    }
}
