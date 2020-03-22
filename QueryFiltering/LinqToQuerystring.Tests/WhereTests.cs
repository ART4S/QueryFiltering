using System;
using System.Linq;
using LinqToQuerystring.Tests.Model;
using Xunit;

namespace LinqToQuerystring.Tests
{
    public class WhereTests
    {
        [Theory]
        [InlineData(int.MaxValue)]
        [InlineData(int.MinValue)]
        public void SelectElementsWhereIntValueEqualsSomeValue_ReturnsFilteredElements(int value)
        {
            IQueryable<TestObject> queryable = new[]
            {
                new TestObject(){IntValue = value},
                new TestObject()
            }.AsQueryable();

            IQueryable<TestObject> expected = queryable.Where(x => x.IntValue == value);
            IQueryable<TestObject> actual = queryable.ApplyQuery($"$where=IntValue eq {value}");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(long.MaxValue)]
        [InlineData(long.MinValue)]
        public void SelectElementsWhereLongValueEqualsSomeValue_ReturnsFilteredElements(long value)
        {
            IQueryable<TestObject> queryable = new[]
            {
                new TestObject(){LongValue = value},
                new TestObject()
            }.AsQueryable();

            IQueryable<TestObject> expected = queryable.Where(x => x.LongValue == value);
            IQueryable<TestObject> actual = queryable.ApplyQuery($"$where=LongValue eq {value}l");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1.00)]
        [InlineData(-1.00)]
        public void SelectElementsWhereDoubleValueEqualsSomeValue_ReturnsFilteredElements(int value)
        {
            IQueryable<TestObject> queryable = new[]
            {
                new TestObject(){DoubleValue = value},
                new TestObject()
            }.AsQueryable();

            IQueryable<TestObject> expected = queryable.Where(x => x.DoubleValue == value);
            IQueryable<TestObject> actual = queryable.ApplyQuery($"$where=DoubleValue eq {value}d");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void SelectElementsWhereBoolValueEqualsSome_ReturnsFilteredElements(bool value)
        {
            IQueryable<TestObject> queryable = new[]
            {
                new TestObject(){BoolValue = value},
                new TestObject(){BoolValue = !value}
            }.AsQueryable();

            IQueryable<TestObject> expected = queryable.Where(x => x.BoolValue == value);
            IQueryable<TestObject> actual = queryable.ApplyQuery($"$where=BoolValue eq {value.ToString().ToLower()}");

            Assert.Equal(expected, actual);
        }


        [Theory]
        [InlineData(1.00)]
        [InlineData(-1.00)]
        public void SelectElementsWhereDecimalValueEqualsSomeValue_ReturnsFilteredElements(decimal value)
        {
            IQueryable<TestObject> queryable = new[]
            {
                new TestObject(){DecimalValue = value},
                new TestObject()
            }.AsQueryable();

            IQueryable<TestObject> expected = queryable.Where(x => x.DecimalValue == value);
            IQueryable<TestObject> actual = queryable.ApplyQuery($"$where=DecimalValue eq {value}m");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1.00)]
        [InlineData(-1.00)]
        [InlineData(0)]
        public void SelectElementsWhereFloatValueEqualsSomeValue_ReturnsFilteredElements(float testValue)
        {
            IQueryable<TestObject> queryable = new[]
            {
                new TestObject(){FloatValue = testValue},
                new TestObject()
            }.AsQueryable();

            IQueryable<TestObject> expected = queryable.Where(x => x.FloatValue == testValue);
            IQueryable<TestObject> actual = queryable.ApplyQuery($"$where=FloatValue eq {testValue}m");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("2ec37a3c-1529-4298-a1da-30472b68e6a5")]
        [InlineData("00000000-0000-0000-0000-000000000000")]
        public void SelectElementsWhereGuidValueEqualsSomeValue_ReturnsFilteredElements(string testValue)
        {
            Guid testGuid = Guid.Parse(testValue);
            IQueryable<TestObject> queryable = new[]
            {
                new TestObject(){GuidValue = testGuid},
                new TestObject()
            }.AsQueryable();

            IQueryable<TestObject> expected = queryable.Where(x => x.GuidValue == testGuid);
            IQueryable<TestObject> actual = queryable.ApplyQuery($"$where=GuidValue eq {testGuid}");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("2000-01-01")]
        [InlineData("2000-01-01T12:59")]
        public void SelectElementsWhereDateTimeValueEqualsSomeValue_ReturnsFilteredElements(string testValue)
        {
            DateTime testDateTime = DateTime.Parse(testValue);
            IQueryable<TestObject> queryable = new[]
            {
                new TestObject(){DateTimeValue = testDateTime},
                new TestObject()
            }.AsQueryable();

            IQueryable<TestObject> expected = queryable.Where(x => x.DateTimeValue == testDateTime);
            IQueryable<TestObject> actual = queryable.ApplyQuery($"$where=DateTimeValue eq datetime'{testValue}'");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("test")]
        [InlineData("")]
        public void SelectElementsWhereStringValueEqualsSomeValue_ReturnsFilteredElements(string testValue)
        {
            IQueryable<TestObject> queryable = new[]
            {
                new TestObject(){StringValue = testValue},
                new TestObject()
            }.AsQueryable();

            IQueryable<TestObject> expected = queryable.Where(x => x.StringValue == testValue);
            IQueryable<TestObject> actual = queryable.ApplyQuery($"$where=StringValue eq '{testValue}'");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SelectElementsWhereStringValueEqualsNull_ReturnsFilteredElements()
        {
            IQueryable<TestObject> queryable = new[]
            {
                new TestObject(),
                new TestObject(){StringValue = "test"}
            }.AsQueryable();

            IQueryable<TestObject> expected = queryable.Where(x => x.StringValue == null);
            IQueryable<TestObject> actual = queryable.ApplyQuery("$where=StringValue eq null");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(-1)]
        public void SelectElementsWhereNullableIntValueEqualsSomeValue_ReturnsFilteredElements(int? testValue)
        {
            var queryable = new[]
            {
                new TestObject(){NullableIntValue = testValue},
                new TestObject()
            }.AsQueryable();

            var expected = queryable.Where(x => x.NullableIntValue == testValue);
            var actual = queryable.ApplyQuery($"$where=NullableIntValue eq {testValue}");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SelectElementsWhereNullableIntValueEqualsNull_ReturnsFilteredElements()
        {
            IQueryable<TestObject> queryable = new[]
            {
                new TestObject(){NullableIntValue = null},
                new TestObject(){NullableIntValue = 1}
            }.AsQueryable();

            IQueryable<TestObject> expected = queryable.Where(x => x.NullableIntValue == null);
            IQueryable<TestObject> actual = queryable.ApplyQuery("$where=NullableIntValue eq null");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("test")]
        [InlineData("")]
        public void SelectElementsWhereStringValueStartsWithSomeValue_ReturnsFilteredElements(string testValue)
        {
            IQueryable<TestObject> queryable = new[]
            {
                new TestObject(){StringValue = testValue},
                new TestObject(){StringValue = ""}
            }.AsQueryable();

            IQueryable<TestObject> expected = queryable.Where(x => x.StringValue.StartsWith(testValue));
            IQueryable<TestObject> actual = queryable.ApplyQuery($"$where=startswith(StringValue, '{testValue}') eq true");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("test")]
        [InlineData("")]
        public void SelectElementsWhereStringValueEndsWithSomeValue_ReturnsFilteredElements(string testValue)
        {
            IQueryable<TestObject> queryable = new[]
            {
                new TestObject(){StringValue = "0" + testValue},
                new TestObject(){StringValue = ""}
            }.AsQueryable();

            IQueryable<TestObject> expected = queryable.Where(x => x.StringValue.EndsWith(testValue));
            IQueryable<TestObject> actual = queryable.ApplyQuery($"$where=endswith(StringValue, '{testValue}') eq true");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("test")]
        [InlineData("")]
        public void SelectElementsWhereToUpperStringValueEqualsSomeValue_ReturnsFilteredElements(string testValue)
        {
            IQueryable<TestObject> queryable = new[]
            {
                new TestObject(){StringValue = testValue},
                new TestObject(){StringValue = ""}
            }.AsQueryable();

            string matchTestValue = testValue.ToUpper();

            IQueryable<TestObject> expected = queryable.Where(x => x.StringValue.ToUpper() == matchTestValue);
            IQueryable<TestObject> actual = queryable.ApplyQuery($"$where=toupper(StringValue) eq '{matchTestValue}'");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("TEST")]
        [InlineData("")]
        public void SelectElementsWhereToLowerStringValueEqualsSomeValue_ReturnsFilteredElements(string testValue)
        {
            IQueryable<TestObject> queryable = new[]
            {
                new TestObject(){StringValue = testValue},
                new TestObject(){StringValue = ""}
            }.AsQueryable();

            string matchTestValue = testValue.ToLower();

            IQueryable<TestObject> expected = queryable.Where(x => x.StringValue.ToLower() == matchTestValue);
            IQueryable<TestObject> actual = queryable.ApplyQuery($"$where=tolower(StringValue) eq '{matchTestValue}'");

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("test")]
        [InlineData("")]
        public void SelectElementsWhereStringValueNotEqualsSomeString_ReturnsFilteredElements(string testValue)
        {
            IQueryable<TestObject> queryable = new[]
            {
                new TestObject(),
                new TestObject(){StringValue = testValue},
            }.AsQueryable();

            IQueryable<TestObject> expected = queryable.Where(x => !(x.StringValue == testValue));
            IQueryable<TestObject> actual = queryable.ApplyQuery($"$where=not StringValue eq '{testValue}'");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SelectElementsWhereInnerObjectNotNullAndInnerObjectIntValueEqualsSomeValue_ReturnsFilteredElements()
        {
            IQueryable<TestObject> queryable = new[]
            {
                new TestObject(){InnerObject = new InnerObject(){IntValue = 1}},
                new TestObject()
            }.AsQueryable();

            IQueryable<TestObject> expected = queryable.Where(x => x.InnerObject != null && x.InnerObject.IntValue == 1);
            IQueryable<TestObject> actual = queryable.ApplyQuery("$where=InnerObject ne null and InnerObject/IntValue eq 1");

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SelectElementsWhereInnerObjectIntValueEqualsSomeValueOrOtherInnerObjectStringValueStartsWithSomeValue_ReturnsFilteredElements()
        {
            IQueryable<TestObject> queryable = new[]
            {
                new TestObject(){InnerObject = new InnerObject(){IntValue = 1}},
                new TestObject(){InnerObject = new InnerObject(){OtherInnerObject = new InnerObject(){StringValue = "testString"}}},
                new TestObject()
            }.AsQueryable();

            IQueryable<TestObject> expected = queryable.Where(
                x => x.InnerObject != null &&
                     (x.InnerObject.IntValue == 1 ||
                      x.InnerObject.OtherInnerObject != null &&
                      x.InnerObject.OtherInnerObject.StringValue.StartsWith("test")));

            IQueryable<TestObject> actual = queryable.ApplyQuery(
                "$where=InnerObject ne null and " +
                "(InnerObject/IntValue eq 1 or " +
                "(InnerObject/OtherInnerObject ne null and " +
                "startswith(InnerObject/OtherInnerObject/StringValue, 'test') eq true))");

            Assert.Equal(expected, actual);
        }
    }
}
