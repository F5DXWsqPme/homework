using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace HW2_6_1.Tests
{
    public class ListFunctionsTest
    {
        [Test]
        public void MapShouldReturnEmptyList()
        {
            Assert.AreEqual(0, ListFunctions.Map(new List<int>(), x => -1).Count);
        }

        [Test]
        public void MapShouldReturnEqualList()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, 6 };

            Assert.AreEqual(list, ListFunctions.Map(list, x => x));
        }

        [Test]
        public void MapShouldReturnRightTransformedList()
        {
            Assert.AreEqual(
                new List<int>() { 0, 4, 9, 16, 25, 36 },
                ListFunctions.Map(new List<int>() { 0, -2, 3, -4, 5, 6 }, x => x * x));
        }

        [Test]
        public void FilterShouldReturnEmptyListWhenListEmpty()
        {
            Assert.AreEqual(0, ListFunctions.Filter(new List<int>(), x => true).Count);
            Assert.AreEqual(0, ListFunctions.Filter(new List<int>(), x => false).Count);
        }

        [Test]
        public void FilterShouldReturnEmptyList()
        {
            Assert.AreEqual(0, ListFunctions.Filter(new List<int>() { 1, 2, 3 }, x => false).Count);
        }

        [Test]
        public void FilterShouldReturnEqualList()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, 6 };

            Assert.AreEqual(list, ListFunctions.Filter(list, x => true));
        }

        [Test]
        public void FilterShouldReturnRightTransformedList()
        {
            Assert.AreEqual(
                new List<int>() { 1, 4 },
                ListFunctions.Filter(new List<int>() { 0, 1, 2, 3, 4, 5 }, x => x % 3 == 1));
        }

        [Test]
        public void FoldShouldReturnEmptyList()
        {
            Assert.AreEqual(0, ListFunctions.Fold(new List<int>(), 1, (accumulator, item) => -1).Count);
        }

        [Test]
        public void FoldShouldReturnEqualList()
        {
            var list = new List<int>() { 1, 2, 3, 4, 5, 6 };

            Assert.AreEqual(list, ListFunctions.Fold(list, 0, (accumulator, item) => item));
        }

        [Test]
        public void FoldShouldReturnRightTransformedList()
        {
            Assert.AreEqual(
                new List<int>() { 1, 0, 2, 1, 3 },
                ListFunctions.Fold(new List<int>() { 0, 1, 2, 3, 4 }, -1, (accumulator, item) => item - accumulator));
        }

        [Test]
        public void FoldShouldReturnAriphmeticProgression()
        {
            Assert.AreEqual(
                new List<int>() { 1, 3, 6, 10, 15 },
                ListFunctions.Fold(new List<int>() { 1, 2, 3, 4, 5 }, 0, (accumulator, item) => item + accumulator));
        }
    }
}