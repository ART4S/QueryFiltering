﻿using System.Linq;
using LinqToQuerystring.Tests.Model;
using Xunit;

namespace LinqToQuerystring.Tests
{
    public class SkipTests
    {
        [Theory]
        [InlineData(1)]
        [InlineData(0)]
        [InlineData(-1)]
        public void SkipSomeElements_ReturnsRemainElements(int count)
        {
            IQueryable<TestObject> queryable = new[]
            {
                new TestObject(),
                new TestObject(),
            }.AsQueryable();

            IQueryable<TestObject> expected = queryable.Skip(count);
            IQueryable<TestObject> actual = queryable.ApplyQuery($"$skip={count}");

            Assert.Equal(expected, actual);
        }
    }
}
