using System;
using System.Collections.Generic;
using Xunit;
using ClassLibrarySorter;

namespace TestProject
{
    public class ArraySorterTypedTests
    {
        private readonly ArraySorter sorter = new ArraySorter();

        // ----------- INT -----------
        [Fact]
        public void BubbleSort_SortsIntegers()
        {
            var input = new List<object> { 5, 2, 9, 1 };
            var expected = new List<object> { 1, 2, 5, 9 };

            var result = sorter.BubbleSort(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SelectionSort_SortsIntegers()
        {
            var input = new List<object> { 10, -5, 3, 7 };
            var expected = new List<object> { -5, 3, 7, 10 };

            var result = sorter.SelectionSort(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void InsertionSort_SortsIntegers()
        {
            var input = new List<object> { 12, 11, 13, 5, 6 };
            var expected = new List<object> { 5, 6, 11, 12, 13 };

            var result = sorter.InsertionSort(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void QuickSort_SortsIntegers()
        {
            var input = new List<object> { 4, 2, 7, 1, 3 };
            var expected = new List<object> { 1, 2, 3, 4, 7 };

            var result = sorter.QuickSort(input);

            Assert.Equal(expected, result);
        }

        // ----------- FLOAT -----------
        [Fact]
        public void BubbleSort_SortsFloats()
        {
            var input = new List<object> { 3.2f, 1.5f, 4.8f, 2.0f };
            var expected = new List<object> { 1.5f, 2.0f, 3.2f, 4.8f };

            var result = sorter.BubbleSort(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SelectionSort_SortsFloats()
        {
            var input = new List<object> { -1.1f, 2.5f, 0.0f };
            var expected = new List<object> { -1.1f, 0.0f, 2.5f };

            var result = sorter.SelectionSort(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void InsertionSort_SortsFloats()
        {
            var input = new List<object> { 5.5f, 2.2f, 9.9f, 1.1f };
            var expected = new List<object> { 1.1f, 2.2f, 5.5f, 9.9f };

            var result = sorter.InsertionSort(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void QuickSort_SortsFloats()
        {
            var input = new List<object> { 10.1f, -5.5f, 3.3f, 7.7f };
            var expected = new List<object> { -5.5f, 3.3f, 7.7f, 10.1f };

            var result = sorter.QuickSort(input);

            Assert.Equal(expected, result);
        }

        // ----------- DATETIME -----------
        [Fact]
        public void BubbleSort_SortsDateTimes()
        {
            var input = new List<object>
            {
                new DateTime(2021, 12, 31),
                new DateTime(2020, 1, 1),
                new DateTime(2022, 5, 1)
            };
            var expected = new List<object>
            {
                new DateTime(2020, 1, 1),
                new DateTime(2021, 12, 31),
                new DateTime(2022, 5, 1)
            };

            var result = sorter.BubbleSort(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SelectionSort_SortsDateTimes()
        {
            var input = new List<object>
            {
                new DateTime(1999, 5, 5),
                new DateTime(2005, 10, 10),
                new DateTime(2001, 1, 1)
            };
            var expected = new List<object>
            {
                new DateTime(1999, 5, 5),
                new DateTime(2001, 1, 1),
                new DateTime(2005, 10, 10)
            };

            var result = sorter.SelectionSort(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void InsertionSort_SortsDateTimes()
        {
            var input = new List<object>
            {
                new DateTime(2023, 3, 3),
                new DateTime(2022, 2, 2),
                new DateTime(2024, 4, 4)
            };
            var expected = new List<object>
            {
                new DateTime(2022, 2, 2),
                new DateTime(2023, 3, 3),
                new DateTime(2024, 4, 4)
            };

            var result = sorter.InsertionSort(input);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void QuickSort_SortsDateTimes()
        {
            var input = new List<object>
            {
                new DateTime(2025, 1, 1),
                new DateTime(2020, 6, 15),
                new DateTime(2023, 9, 9)
            };
            var expected = new List<object>
            {
                new DateTime(2020, 6, 15),
                new DateTime(2023, 9, 9),
                new DateTime(2025, 1, 1)
            };

            var result = sorter.QuickSort(input);

            Assert.Equal(expected, result);
        }
    }
}

