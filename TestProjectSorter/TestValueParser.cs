using System;
using Xunit;
using ClassLibrarySorter;

namespace ClassLibrarySorter.Tests
{
    public class ValueParserTests
    {
        // ------------- INT ----------------
        [Fact]
        public void ParseValue_Int_Success()
        {
            var input = "42";
            var result = ValueParser.ParseValue(input, typeof(int));
            Assert.Equal(42, result);
        }

        [Fact]
        public void ParseValue_Int_InvalidFormat_ThrowsFormatException()
        {
            var input = "abc";
            Assert.Throws<FormatException>(() => ValueParser.ParseValue(input, typeof(int)));
        }

        // ------------- FLOAT -------------
        [Fact]
        public void ParseValue_Float_Success()
        {
            var input = "3,14";
            var result = ValueParser.ParseValue(input, typeof(float));
            Assert.Equal(3.14f, result);
        }

        [Fact]
        public void ParseValue_Float_InvalidFormat_ThrowsFormatException()
        {
            var input = "pi";
            Assert.Throws<FormatException>(() => ValueParser.ParseValue(input, typeof(float)));
        }

        // ------------- DATETIME ----------
        [Theory]
        [InlineData("01.01.2020", 2020, 1, 1)]
        [InlineData("1.1.2020", 2020, 1, 1)]
        [InlineData("2020-01-01", 2020, 1, 1)]
        public void ParseValue_DateTime_Success(string input, int year, int month, int day)
        {
            var result = ValueParser.ParseValue(input, typeof(DateTime));
            var expected = new DateTime(year, month, day);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void ParseValue_DateTime_InvalidFormat_ThrowsFormatException()
        {
            var input = "31-02-2020"; // некорректная дата
            Assert.Throws<FormatException>(() => ValueParser.ParseValue(input, typeof(DateTime)));
        }

        // ------------- NULL TYPE ----------
        [Fact]
        public void ParseValue_NullType_ThrowsArgumentNullException()
        {
            var input = "123";
            Assert.Throws<ArgumentNullException>(() => ValueParser.ParseValue(input, null));
        }

        // ------------- Unsupported Type ----------
        [Fact]
        public void ParseValue_UnsupportedType_ThrowsArgumentException()
        {
            var input = "true";
            Assert.Throws<ArgumentException>(() => ValueParser.ParseValue(input, typeof(bool)));
        }
    }
}
