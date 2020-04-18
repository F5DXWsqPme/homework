using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

namespace Solution.Tests
{
    public class SetTest
    {
        private Set<int> setInt;
        private Set<string> setString;

        [SetUp]
        public void Setup()
        {
            this.setInt = new Set<int>(new DefaultComparer<int>());
            this.setString = new Set<string>(new DefaultComparer<string>());
        }

        private static IEnumerable<TestCaseData> AddTestDataInt()
        {
            var emptyList = new List<int>();

            yield return new TestCaseData(emptyList, 1).Returns(true).SetName("int: Add one element");

            var listWith1AndWithout0 = new List<int>() { 1, 2, 3 };

            yield return new TestCaseData(listWith1AndWithout0, 1).Returns(false).SetName("int: Add existed element");
            yield return new TestCaseData(listWith1AndWithout0, 0).Returns(true).SetName("int: Add not existed element");

            var inversedList = new List<int>() { 3, 2, 1 };

            yield return new TestCaseData(inversedList, 1).Returns(false).SetName("int: Add existed element");
            yield return new TestCaseData(inversedList, 0).Returns(true).SetName("int: Add not existed element");
        }

        [TestCaseSource(nameof(AddTestDataInt))]
        [Test]
        public bool ListIntShouldAddElement(List<int> initial, int final)
        {
            foreach (var item in initial)
            {
                this.setInt.Add(item);
            }

            return this.setInt.Add(final);
        }

        private static IEnumerable<TestCaseData> AddTestDataString()
        {
            var emptyList = new List<string>();

            yield return new TestCaseData(emptyList, "1").Returns(true).SetName("string: Add one element");

            var listWith1AndWithout0 = new List<string>() { "1", "2", "3" };

            yield return new TestCaseData(listWith1AndWithout0, "1").Returns(false).SetName("string: Add existed element");
            yield return new TestCaseData(listWith1AndWithout0, "0").Returns(true).SetName("string: Add not existed element");

            var inversedList = new List<string>() { "3", "2", "1" };

            yield return new TestCaseData(inversedList, "1").Returns(false).SetName("string: Add existed element");
            yield return new TestCaseData(inversedList, "0").Returns(true).SetName("string: Add not existed element");
        }

        [TestCaseSource(nameof(AddTestDataString))]
        [Test]
        public bool ListStringShouldAddElement(List<string> initial, string final)
        {
            foreach (var item in initial)
            {
                this.setString.Add(item);
            }

            return this.setString.Add(final);
        }

        private class DefaultComparer<T> : IComparer<T>
            where T : IComparable<T>
        {
            public int Compare([AllowNull] T first, [AllowNull] T second)
            {
                return first.CompareTo(second);
            }
        }
    }
}