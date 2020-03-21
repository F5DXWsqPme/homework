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
        public void FoldShouldDontTransformEmptyList()
        {
            Assert.AreEqual(1, ListFunctions.Fold(new List<int>(), 1, (accumulator, item) => -1));
        }

        [Test]
        public void FoldShouldReturnRightValue()
        {
            Assert.AreEqual(3, ListFunctions.Fold(new List<int>() { 0, 1, 2, 3, 4 }, -1, (accumulator, item) => item - accumulator));
        }

        [Test]
        public void FoldShouldReturnAriphmeticProgression()
        {
            Assert.AreEqual(15, ListFunctions.Fold(new List<int>() { 1, 2, 3, 4, 5 }, 0, (accumulator, item) => item + accumulator));
            Assert.AreEqual(21, ListFunctions.Fold(new List<int>() { 2, 3, 4, 5, 6 }, 1, (accumulator, item) => item + accumulator));
        }
    }
}