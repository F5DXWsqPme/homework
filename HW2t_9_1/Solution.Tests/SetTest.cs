using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;

namespace Solution.Tests
{
    public class SetTest
    {
        private Set<int> set;
        private Set<int> setInt;
        private Set<string> setString;

        [SetUp]
        public void Setup()
        {
            this.set = new Set<int>(new DefaultComparer<int>());
            this.setInt = new Set<int>(new DefaultComparer<int>());
            this.setString = new Set<string>(new DefaultComparer<string>());
        }

        private static IEnumerable<TestCaseData> AddTestDataInt()
        {
            var emptyList = new List<int>();

            yield return new TestCaseData(emptyList, 1).Returns(true).SetCategory("Add one element");

            var listWith1AndWithout0 = new List<int>() { 1, 2, 3 };

            yield return new TestCaseData(listWith1AndWithout0, 1).Returns(false).SetCategory("Add existed element");
            yield return new TestCaseData(listWith1AndWithout0, 0).Returns(true).SetCategory("Add not existed element");

            var inversedList = new List<int>() { 3, 2, 1 };

            yield return new TestCaseData(inversedList, 1).Returns(false).SetCategory("Add existed element");
            yield return new TestCaseData(inversedList, 0).Returns(true).SetCategory("Add not existed element");
        }

        [TestCaseSource(nameof(AddTestDataInt))]
        [Test]
        public bool SetIntShouldAddElement(List<int> initial, int final)
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

            yield return new TestCaseData(emptyList, "1").Returns(true).SetCategory("Add one element");

            var listWith1AndWithout0 = new List<string>() { "1", "2", "3" };

            yield return new TestCaseData(listWith1AndWithout0, "1").Returns(false).SetCategory("Add existed element");
            yield return new TestCaseData(listWith1AndWithout0, "0").Returns(true).SetCategory("Add not existed element");

            var inversedList = new List<string>() { "3", "2", "1" };

            yield return new TestCaseData(inversedList, "1").Returns(false).SetCategory("Add existed element");
            yield return new TestCaseData(inversedList, "0").Returns(true).SetCategory("Add not existed element");
        }

        [TestCaseSource(nameof(AddTestDataString))]
        [Test]
        public bool SetStringShouldAddElement(List<string> initial, string final)
        {
            foreach (var item in initial)
            {
                this.setString.Add(item);
            }

            return this.setString.Add(final);
        }

        private static IEnumerable<TestCaseData> ContainsTestDataInt()
        {
            var emptyList = new List<int>();

            yield return new TestCaseData(emptyList, 1).Returns(false).SetCategory("Contains in empty set");

            var listWith1AndWithout0 = new List<int>() { 1, 2, 3 };

            yield return new TestCaseData(listWith1AndWithout0, 1).Returns(true).SetCategory("Contains existed element");
            yield return new TestCaseData(listWith1AndWithout0, 0).Returns(false).SetCategory("Contains not existed element");

            var inversedList = new List<int>() { 3, 2, 1 };

            yield return new TestCaseData(inversedList, 1).Returns(true).SetCategory("Contains existed element");
            yield return new TestCaseData(inversedList, 0).Returns(false).SetCategory("Contains not existed element");
        }

        [TestCaseSource(nameof(ContainsTestDataInt))]
        [Test]
        public bool SetIntShouldContainsElement(List<int> initial, int final)
        {
            foreach (var item in initial)
            {
                this.setInt.Add(item);
            }

            return this.setInt.Contains(final);
        }

        private static IEnumerable<TestCaseData> ContainsTestDataString()
        {
            var emptyList = new List<string>();

            yield return new TestCaseData(emptyList, "1").Returns(false).SetCategory("Contains in empty set");

            var listWith1AndWithout0 = new List<string>() { "1", "2", "3" };

            yield return new TestCaseData(listWith1AndWithout0, "1").Returns(true).SetCategory("Contains existed element");
            yield return new TestCaseData(listWith1AndWithout0, "0").Returns(false).SetCategory("Contains not existed element");

            var inversedList = new List<string>() { "3", "2", "1" };

            yield return new TestCaseData(inversedList, "1").Returns(true).SetCategory("Contains existed element");
            yield return new TestCaseData(inversedList, "0").Returns(false).SetCategory("Contains not existed element");
        }

        [TestCaseSource(nameof(ContainsTestDataString))]
        [Test]
        public bool SetStringShouldContainsElement(List<string> initial, string final)
        {
            foreach (var item in initial)
            {
                this.setString.Add(item);
            }

            return this.setString.Contains(final);
        }

        private static IEnumerable<TestCaseData> GetEnumeratorTestDataInt()
        {
            yield return new TestCaseData(new List<int>()).SetCategory("Empty");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }).SetCategory("3 elements");
            yield return new TestCaseData(new List<int>() { 3, 2, 1 }).SetCategory("Inversed");
            yield return new TestCaseData(new List<int>() { 1, 2, 3, 4, 5 }).SetCategory("5 elements");
            yield return new TestCaseData(new List<int>() { 4, 5, 2, 3, 1 }).SetCategory("Random order");
        }

        [TestCaseSource(nameof(GetEnumeratorTestDataInt))]
        [Test]
        public void SetIntShouldGetEnumerator(List<int> data)
        {
            foreach (var item in data)
            {
                this.setInt.Add(item);
            }

            data.Sort();

            {
                IEnumerator<int> iterator = this.setInt.GetEnumerator();

                foreach (var item in data)
                {
                    iterator.MoveNext();
                    Assert.AreEqual(item, iterator.Current);
                }
            }

            {
                IEnumerator iterator = ((IEnumerable)this.setInt).GetEnumerator();

                foreach (var item in data)
                {
                    iterator.MoveNext();
                    Assert.AreEqual(item, iterator.Current);
                }
            }
        }

        private static IEnumerable<TestCaseData> GetEnumeratorTestDataString()
        {
            yield return new TestCaseData(new List<string>()).SetCategory("Empty");
            yield return new TestCaseData(new List<string>() { "1", "2", "3" }).SetCategory("3 elements");
            yield return new TestCaseData(new List<string>() { "3", "2", "1" }).SetCategory("Inversed");
            yield return new TestCaseData(new List<string>() { "1", "2", "3", "4", "5" }).SetCategory("5 elements");
            yield return new TestCaseData(new List<string>() { "4", "5", "2", "3", "1" }).SetCategory("Random order");
        }

        [TestCaseSource(nameof(GetEnumeratorTestDataString))]
        [Test]
        public void SetStringShouldGetEnumerator(List<string> data)
        {
            foreach (var item in data)
            {
                this.setString.Add(item);
            }

            data.Sort();

            {
                IEnumerator<string> iterator = this.setString.GetEnumerator();

                foreach (var item in data)
                {
                    iterator.MoveNext();
                    Assert.AreEqual(item, iterator.Current);
                }
            }

            {
                IEnumerator iterator = ((IEnumerable)this.setString).GetEnumerator();

                foreach (var item in data)
                {
                    iterator.MoveNext();
                    Assert.AreEqual(item, iterator.Current);
                }
            }
        }

        [Test]
        public void SetShouldRemoveElementFromEmptySet()
        {
            Assert.IsFalse(this.setInt.Remove(1));
            Assert.IsFalse(this.setString.Remove("1"));
        }

        [Test]
        public void SetShouldRemoveElement()
        {
            this.setInt.Add(1);
            this.setString.Add("1");

            Assert.IsTrue(this.setInt.Remove(1));
            Assert.IsTrue(this.setString.Remove("1"));

            Assert.IsFalse(this.setInt.Contains(1));
            Assert.IsFalse(this.setString.Contains("1"));
        }

        private static IEnumerable<TestCaseData> RemoveTestData()
        {
            for (int i = 1; i <= 5; i++)
            {
                yield return new TestCaseData(i).SetCategory("Current element for remove: " + i.ToString());
            }
        }

        [TestCaseSource(nameof(RemoveTestData))]
        [Test]
        public void SetShouldRemoveElementAndSaveOther(int itemForRemove)
        {
            var data = new List<int>() { 3, 2, 5, 1, 4 };

            this.setInt.Clear();
            this.setString.Clear();

            foreach (var item in data)
            {
                this.setInt.Add(item);
                this.setString.Add(item.ToString());
            }

            Assert.IsTrue(this.setInt.Remove(itemForRemove));
            Assert.IsTrue(this.setString.Remove(itemForRemove.ToString()));

            foreach (var item in data)
            {
                if (item == itemForRemove)
                {
                    Assert.IsFalse(this.setInt.Contains(item));
                    Assert.IsFalse(this.setString.Contains(item.ToString()));
                }
                else
                {
                    Assert.IsTrue(this.setInt.Contains(item));
                    Assert.IsTrue(this.setString.Contains(item.ToString()));
                }
            }
        }

        [Test]
        public void SetShouldGetRightCountEmpty()
        {
            Assert.AreEqual(0, this.setInt.Count);
            Assert.AreEqual(0, this.setString.Count);
        }

        [Test]
        public void SetShouldGetRightCount()
        {
            this.setInt.Add(0);
            this.setString.Add("0");
            this.setInt.Add(1);
            this.setString.Add("1");

            Assert.AreEqual(2, this.setInt.Count);
            Assert.AreEqual(2, this.setString.Count);

            this.setInt.Remove(1);
            this.setString.Remove("1");

            Assert.AreEqual(1, this.setInt.Count);
            Assert.AreEqual(1, this.setString.Count);
        }

        [Test]
        public void SetShouldGetRightCountEqual()
        {
            this.setInt.Add(0);
            this.setString.Add("0");
            this.setInt.Add(0);
            this.setString.Add("0");

            Assert.AreEqual(1, this.setInt.Count);
            Assert.AreEqual(1, this.setString.Count);
        }

        [Test]
        public void SetShouldClear()
        {
            this.setInt.Add(0);
            this.setString.Add("0");
            this.setInt.Add(1);
            this.setString.Add("1");

            this.setInt.Clear();
            this.setString.Clear();

            Assert.AreEqual(0, this.setInt.Count);
            Assert.AreEqual(0, this.setString.Count);
            Assert.IsFalse(this.setInt.Contains(1));
            Assert.IsFalse(this.setString.Contains("1"));
            Assert.IsFalse(this.setInt.Contains(0));
            Assert.IsFalse(this.setString.Contains("0"));

            this.setInt.Add(1);
            this.setString.Add("1");

            Assert.AreEqual(1, this.setInt.Count);
            Assert.AreEqual(1, this.setString.Count);
            Assert.IsTrue(this.setInt.Contains(1));
            Assert.IsTrue(this.setString.Contains("1"));
        }

        [Test]
        public void SetShouldNotReadOnly()
        {
            Assert.IsFalse(this.setInt.IsReadOnly);
            Assert.IsFalse(this.setString.IsReadOnly);
        }

        private static IEnumerable<TestCaseData> CopyToTestData()
        {
            yield return new TestCaseData(new List<int>() { }, 0).SetCategory("Empty");
            yield return new TestCaseData(new List<int>() { }, 3).SetCategory("Empty");
            yield return new TestCaseData(new List<int>() { 1, 2, 3, 4, 5 }, 0).SetCategory("Not empty");
            yield return new TestCaseData(new List<int>() { 5, 4, 3, 2, 1 }, 0).SetCategory("Not empty inversed");
        }

        [TestCaseSource(nameof(CopyToTestData))]
        [Test]
        public void SetShouldCopyTo(List<int> data, int position)
        {
            foreach (var item in data)
            {
                this.setInt.Add(item);
                this.setString.Add(item.ToString());
            }

            int size = data.Count + position + 1;

            int[] arrayInt = new int[size];
            string[] arrayString = new string[size];

            this.setInt.CopyTo(arrayInt, position);
            this.setString.CopyTo(arrayString, position);

            data.Sort();

            IEnumerator<int> iteratorInt = this.setInt.GetEnumerator();
            IEnumerator<string> iteratorString = this.setString.GetEnumerator();

            for (int i = 0; i < size; i++)
            {
                if (i < position || i >= position + data.Count)
                {
                    Assert.AreEqual(0, arrayInt[i]);
                    Assert.AreEqual(null, arrayString[i]);
                }
                else
                {
                    iteratorInt.MoveNext();
                    iteratorString.MoveNext();

                    Assert.AreEqual(iteratorInt.Current, arrayInt[i]);
                    Assert.AreEqual(iteratorString.Current, arrayString[i]);
                }
            }
        }

        /* From now on, only Set <int> is in the tests, because the remaining
         * methods use the methods already tested and should not have any differences.
         */

        [Test]
        public void SetShouldAddElementsBecauseSetIsCollection()
        {
            ((ICollection<int>)this.set).Add(1);
            ((ICollection<int>)this.set).Add(2);

            Assert.IsTrue(this.set.Contains(1));
            Assert.IsTrue(this.set.Contains(2));
            Assert.IsFalse(this.set.Contains(0));
            Assert.IsFalse(this.set.Contains(3));
        }

        private static IEnumerable<TestCaseData> SetEqualsTestData()
        {
            yield return new TestCaseData(new List<int>() { }, new List<int> { }).Returns(true).SetCategory("Empty");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { }).Returns(false).SetCategory("Empty and not empty");
            yield return new TestCaseData(new List<int>() { }, new List<int> { 1, 2, 3 }).Returns(false).SetCategory("Empty and not empty");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 1, 2, 3 }).Returns(true).SetCategory("Equals");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 3, 2, 1 }).Returns(true).SetCategory("Equals but different order");
            yield return new TestCaseData(new List<int>() { 3, 2, 1 }, new List<int> { 2, 1, 3 }).Returns(true).SetCategory("Equals but different order");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 1, 2, 3, 4 }).Returns(false).SetCategory("Proper subset");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 4, 3, 2, 1 }).Returns(false).SetCategory("Proper subset");
            yield return new TestCaseData(new List<int>() { 1, 2, 3, 4 }, new List<int> { 1, 4, 2 }).Returns(false).SetCategory("Proper superset");
            yield return new TestCaseData(new List<int>() { 4, 3, 2, 1 }, new List<int> { 1, 4, 2 }).Returns(false).SetCategory("Proper superset");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 1, 4, 2 }).Returns(false).SetCategory("Overlaps");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 3, 4, 5 }).Returns(false).SetCategory("Overlaps");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 4, 5, 6 }).Returns(false).SetCategory("Not overlaps");
            yield return new TestCaseData(new List<int>() { 1, 3, 2 }, new List<int> { 6, 5, 4 }).Returns(false).SetCategory("Not overlaps");
        }

        [TestCaseSource(nameof(SetEqualsTestData))]
        [Test]
        public bool SetShouldCompareSets(List<int> data, List<int> compareWith)
        {
            foreach (var item in data)
            {
                this.set.Add(item);
            }

            return this.set.SetEquals(compareWith);
        }

        private static IEnumerable<TestCaseData> OverlapsTestData()
        {
            yield return new TestCaseData(new List<int>() { }, new List<int> { }).Returns(false).SetCategory("Empty");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { }).Returns(false).SetCategory("Empty and not empty");
            yield return new TestCaseData(new List<int>() { }, new List<int> { 1, 2, 3 }).Returns(false).SetCategory("Empty and not empty");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 1, 2, 3 }).Returns(true).SetCategory("Equals");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 3, 2, 1 }).Returns(true).SetCategory("Equals but different order");
            yield return new TestCaseData(new List<int>() { 3, 2, 1 }, new List<int> { 2, 1, 3 }).Returns(true).SetCategory("Equals but different order");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 1, 2, 3, 4 }).Returns(true).SetCategory("Proper subset");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 4, 3, 2, 1 }).Returns(true).SetCategory("Proper subset");
            yield return new TestCaseData(new List<int>() { 1, 2, 3, 4 }, new List<int> { 1, 4, 2 }).Returns(true).SetCategory("Proper superset");
            yield return new TestCaseData(new List<int>() { 4, 3, 2, 1 }, new List<int> { 1, 4, 2 }).Returns(true).SetCategory("Proper superset");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 1, 4, 2 }).Returns(true).SetCategory("Overlaps");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 3, 4, 5 }).Returns(true).SetCategory("Overlaps");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 4, 5, 6 }).Returns(false).SetCategory("Not overlaps");
            yield return new TestCaseData(new List<int>() { 1, 3, 2 }, new List<int> { 6, 5, 4 }).Returns(false).SetCategory("Not overlaps");
        }

        [TestCaseSource(nameof(OverlapsTestData))]
        [Test]
        public bool SetShouldGetOverlaps(List<int> data, List<int> compareWith)
        {
            foreach (var item in data)
            {
                this.set.Add(item);
            }

            return this.set.Overlaps(compareWith);
        }

        private static IEnumerable<TestCaseData> SubsetTestData()
        {
            yield return new TestCaseData(new List<int>() { }, new List<int> { }).Returns(true).SetCategory("Empty");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { }).Returns(false).SetCategory("Empty and not empty");
            yield return new TestCaseData(new List<int>() { }, new List<int> { 1, 2, 3 }).Returns(true).SetCategory("Empty and not empty");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 1, 2, 3 }).Returns(true).SetCategory("Equals");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 3, 2, 1 }).Returns(true).SetCategory("Equals but different order");
            yield return new TestCaseData(new List<int>() { 3, 2, 1 }, new List<int> { 2, 1, 3 }).Returns(true).SetCategory("Equals but different order");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 1, 2, 3, 4 }).Returns(true).SetCategory("Proper subset");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 4, 3, 2, 1 }).Returns(true).SetCategory("Proper subset");
            yield return new TestCaseData(new List<int>() { 1, 2, 3, 4 }, new List<int> { 1, 4, 2 }).Returns(false).SetCategory("Proper superset");
            yield return new TestCaseData(new List<int>() { 4, 3, 2, 1 }, new List<int> { 1, 4, 2 }).Returns(false).SetCategory("Proper superset");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 1, 4, 2 }).Returns(false).SetCategory("Overlaps");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 3, 4, 5 }).Returns(false).SetCategory("Overlaps");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 4, 5, 6 }).Returns(false).SetCategory("Not overlaps");
            yield return new TestCaseData(new List<int>() { 1, 3, 2 }, new List<int> { 6, 5, 4 }).Returns(false).SetCategory("Not overlaps");
        }

        [TestCaseSource(nameof(SubsetTestData))]
        [Test]
        public bool SetShouldGetSubset(List<int> data, List<int> compareWith)
        {
            foreach (var item in data)
            {
                this.set.Add(item);
            }

            return this.set.IsSubsetOf(compareWith);
        }

        private static IEnumerable<TestCaseData> ProperSubsetTestData()
        {
            yield return new TestCaseData(new List<int>() { }, new List<int> { }).Returns(false).SetCategory("Empty");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { }).Returns(false).SetCategory("Empty and not empty");
            yield return new TestCaseData(new List<int>() { }, new List<int> { 1, 2, 3 }).Returns(true).SetCategory("Empty and not empty");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 1, 2, 3 }).Returns(false).SetCategory("Equals");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 3, 2, 1 }).Returns(false).SetCategory("Equals but different order");
            yield return new TestCaseData(new List<int>() { 3, 2, 1 }, new List<int> { 2, 1, 3 }).Returns(false).SetCategory("Equals but different order");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 1, 2, 3, 4 }).Returns(true).SetCategory("Proper subset");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 4, 3, 2, 1 }).Returns(true).SetCategory("Proper subset");
            yield return new TestCaseData(new List<int>() { 1, 2, 3, 4 }, new List<int> { 1, 4, 2 }).Returns(false).SetCategory("Proper superset");
            yield return new TestCaseData(new List<int>() { 4, 3, 2, 1 }, new List<int> { 1, 4, 2 }).Returns(false).SetCategory("Proper superset");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 1, 4, 2 }).Returns(false).SetCategory("Overlaps");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 3, 4, 5 }).Returns(false).SetCategory("Overlaps");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 4, 5, 6 }).Returns(false).SetCategory("Not overlaps");
            yield return new TestCaseData(new List<int>() { 1, 3, 2 }, new List<int> { 6, 5, 4 }).Returns(false).SetCategory("Not overlaps");
        }

        [TestCaseSource(nameof(ProperSubsetTestData))]
        [Test]
        public bool SetShouldGetProperSubset(List<int> data, List<int> compareWith)
        {
            foreach (var item in data)
            {
                this.set.Add(item);
            }

            return this.set.IsProperSubsetOf(compareWith);
        }

        private static IEnumerable<TestCaseData> SupersetTestData()
        {
            yield return new TestCaseData(new List<int>() { }, new List<int> { }).Returns(true).SetCategory("Empty");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { }).Returns(true).SetCategory("Empty and not empty");
            yield return new TestCaseData(new List<int>() { }, new List<int> { 1, 2, 3 }).Returns(false).SetCategory("Empty and not empty");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 1, 2, 3 }).Returns(true).SetCategory("Equals");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 3, 2, 1 }).Returns(true).SetCategory("Equals but different order");
            yield return new TestCaseData(new List<int>() { 3, 2, 1 }, new List<int> { 2, 1, 3 }).Returns(true).SetCategory("Equals but different order");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 1, 2, 3, 4 }).Returns(false).SetCategory("Proper subset");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 4, 3, 2, 1 }).Returns(false).SetCategory("Proper subset");
            yield return new TestCaseData(new List<int>() { 1, 2, 3, 4 }, new List<int> { 1, 4, 2 }).Returns(true).SetCategory("Proper superset");
            yield return new TestCaseData(new List<int>() { 4, 3, 2, 1 }, new List<int> { 1, 4, 2 }).Returns(true).SetCategory("Proper superset");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 1, 4, 2 }).Returns(false).SetCategory("Overlaps");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 3, 4, 5 }).Returns(false).SetCategory("Overlaps");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 4, 5, 6 }).Returns(false).SetCategory("Not overlaps");
            yield return new TestCaseData(new List<int>() { 1, 3, 2 }, new List<int> { 6, 5, 4 }).Returns(false).SetCategory("Not overlaps");
        }

        [TestCaseSource(nameof(SupersetTestData))]
        [Test]
        public bool SetShouldGetSuperset(List<int> data, List<int> compareWith)
        {
            foreach (var item in data)
            {
                this.set.Add(item);
            }

            return this.set.IsSupersetOf(compareWith);
        }

        private static IEnumerable<TestCaseData> ProperSupersetTestData()
        {
            yield return new TestCaseData(new List<int>() { }, new List<int> { }).Returns(false).SetCategory("Empty");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { }).Returns(true).SetCategory("Empty and not empty");
            yield return new TestCaseData(new List<int>() { }, new List<int> { 1, 2, 3 }).Returns(false).SetCategory("Empty and not empty");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 1, 2, 3 }).Returns(false).SetCategory("Equals");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 3, 2, 1 }).Returns(false).SetCategory("Equals but different order");
            yield return new TestCaseData(new List<int>() { 3, 2, 1 }, new List<int> { 2, 1, 3 }).Returns(false).SetCategory("Equals but different order");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 1, 2, 3, 4 }).Returns(false).SetCategory("Proper subset");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 4, 3, 2, 1 }).Returns(false).SetCategory("Proper subset");
            yield return new TestCaseData(new List<int>() { 1, 2, 3, 4 }, new List<int> { 1, 4, 2 }).Returns(true).SetCategory("Proper superset");
            yield return new TestCaseData(new List<int>() { 4, 3, 2, 1 }, new List<int> { 1, 4, 2 }).Returns(true).SetCategory("Proper superset");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 1, 4, 2 }).Returns(false).SetCategory("Overlaps");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 3, 4, 5 }).Returns(false).SetCategory("Overlaps");
            yield return new TestCaseData(new List<int>() { 1, 2, 3 }, new List<int> { 4, 5, 6 }).Returns(false).SetCategory("Not overlaps");
            yield return new TestCaseData(new List<int>() { 1, 3, 2 }, new List<int> { 6, 5, 4 }).Returns(false).SetCategory("Not overlaps");
        }

        [TestCaseSource(nameof(ProperSupersetTestData))]
        [Test]
        public bool SetShouldGetProperSuperset(List<int> data, List<int> compareWith)
        {
            foreach (var item in data)
            {
                this.set.Add(item);
            }

            return this.set.IsProperSupersetOf(compareWith);
        }

        private static IEnumerable<TestCaseData> UnionTestData()
        {
            yield return new TestCaseData(
                new List<int>() { }, new List<int> { }, new List<int> { }).SetCategory("Empty");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { }, new List<int> { 1, 2, 3 }).SetCategory("Empty and not empty");
            yield return new TestCaseData(
                new List<int>() { }, new List<int> { 1, 2, 3 }, new List<int> { 1, 2, 3 }).SetCategory("Empty and not empty");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 1, 2, 3 }, new List<int> { 1, 2, 3 }).SetCategory("Equals");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 3, 2, 1 }, new List<int> { 1, 2, 3 }).SetCategory("Equals but different order");
            yield return new TestCaseData(
                new List<int>() { 3, 2, 1 }, new List<int> { 2, 1, 3 }, new List<int> { 1, 2, 3 }).SetCategory("Equals but different order");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 1, 2, 3, 4 }, new List<int> { 1, 2, 3, 4 }).SetCategory("Proper subset");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 4, 3, 2, 1 }, new List<int> { 1, 2, 3, 4 }).SetCategory("Proper subset");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3, 4 }, new List<int> { 1, 4, 2 }, new List<int> { 1, 2, 3, 4 }).SetCategory("Proper superset");
            yield return new TestCaseData(
                new List<int>() { 4, 3, 2, 1 }, new List<int> { 1, 4, 2 }, new List<int> { 1, 2, 3, 4 }).SetCategory("Proper superset");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 1, 4, 2 }, new List<int> { 1, 2, 3, 4 }).SetCategory("Overlaps");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 3, 4, 5 }, new List<int> { 1, 2, 3, 4, 5 }).SetCategory("Overlaps");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 4, 5, 6 }, new List<int> { 1, 2, 3, 4, 5, 6 }).SetCategory("Not overlaps");
            yield return new TestCaseData(
                new List<int>() { 1, 3, 2 }, new List<int> { 6, 5, 4 }, new List<int> { 1, 2, 3, 4, 5, 6 }).SetCategory("Not overlaps");
        }

        [TestCaseSource(nameof(UnionTestData))]
        [Test]
        public void SetShouldUnion(List<int> first, List<int> second, List<int> result)
        {
            foreach (var item in first)
            {
                this.set.Add(item);
            }

            this.set.UnionWith(second);

            Assert.IsTrue(this.set.SetEquals(result));
        }

        private static IEnumerable<TestCaseData> SimmetricExceptTestData()
        {
            yield return new TestCaseData(
                new List<int>() { }, new List<int> { }, new List<int> { }).SetCategory("Empty");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { }, new List<int> { 1, 2, 3 }).SetCategory("Empty and not empty");
            yield return new TestCaseData(
                new List<int>() { }, new List<int> { 1, 2, 3 }, new List<int> { 1, 2, 3 }).SetCategory("Empty and not empty");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 1, 2, 3 }, new List<int> { }).SetCategory("Equals");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 3, 2, 1 }, new List<int> { }).SetCategory("Equals but different order");
            yield return new TestCaseData(
                new List<int>() { 3, 2, 1 }, new List<int> { 2, 1, 3 }, new List<int> { }).SetCategory("Equals but different order");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 1, 2, 3, 4 }, new List<int> { 4 }).SetCategory("Proper subset");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 4, 3, 2, 1 }, new List<int> { 4 }).SetCategory("Proper subset");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3, 4 }, new List<int> { 1, 4, 2 }, new List<int> { 3 }).SetCategory("Proper superset");
            yield return new TestCaseData(
                new List<int>() { 4, 3, 2, 1 }, new List<int> { 1, 4, 2 }, new List<int> { 3 }).SetCategory("Proper superset");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 1, 4, 2 }, new List<int> { 3, 4 }).SetCategory("Overlaps");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 3, 4, 5 }, new List<int> { 1, 2, 4, 5 }).SetCategory("Overlaps");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 4, 5, 6 }, new List<int> { 1, 2, 3, 4, 5, 6 }).SetCategory("Not overlaps");
            yield return new TestCaseData(
                new List<int>() { 1, 3, 2 }, new List<int> { 6, 5, 4 }, new List<int> { 1, 2, 3, 4, 5, 6 }).SetCategory("Not overlaps");
        }

        [TestCaseSource(nameof(SimmetricExceptTestData))]
        [Test]
        public void SetShouldSimmetricExcept(List<int> first, List<int> second, List<int> result)
        {
            foreach (var item in first)
            {
                this.set.Add(item);
            }

            this.set.SymmetricExceptWith(second);

            Assert.IsTrue(this.set.SetEquals(result));
        }

        private static IEnumerable<TestCaseData> ExceptTestData()
        {
            yield return new TestCaseData(
                new List<int>() { }, new List<int> { }, new List<int> { }).SetCategory("Empty");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { }, new List<int> { 1, 2, 3 }).SetCategory("Empty and not empty");
            yield return new TestCaseData(
                new List<int>() { }, new List<int> { 1, 2, 3 }, new List<int> { }).SetCategory("Empty and not empty");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 1, 2, 3 }, new List<int> { }).SetCategory("Equals");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 3, 2, 1 }, new List<int> { }).SetCategory("Equals but different order");
            yield return new TestCaseData(
                new List<int>() { 3, 2, 1 }, new List<int> { 2, 1, 3 }, new List<int> { }).SetCategory("Equals but different order");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 1, 2, 3, 4 }, new List<int> { }).SetCategory("Proper subset");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 4, 3, 2, 1 }, new List<int> { }).SetCategory("Proper subset");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3, 4 }, new List<int> { 1, 4, 2 }, new List<int> { 3 }).SetCategory("Proper superset");
            yield return new TestCaseData(
                new List<int>() { 4, 3, 2, 1 }, new List<int> { 1, 4, 2 }, new List<int> { 3 }).SetCategory("Proper superset");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 1, 4, 2 }, new List<int> { 3 }).SetCategory("Overlaps");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 3, 4, 5 }, new List<int> { 1, 2 }).SetCategory("Overlaps");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 4, 5, 6 }, new List<int> { 1, 2, 3 }).SetCategory("Not overlaps");
            yield return new TestCaseData(
                new List<int>() { 1, 3, 2 }, new List<int> { 6, 5, 4 }, new List<int> { 1, 2, 3 }).SetCategory("Not overlaps");
        }

        [TestCaseSource(nameof(ExceptTestData))]
        [Test]
        public void SetShouldExcept(List<int> first, List<int> second, List<int> result)
        {
            foreach (var item in first)
            {
                this.set.Add(item);
            }

            this.set.ExceptWith(second);

            Assert.IsTrue(this.set.SetEquals(result));
        }

        private static IEnumerable<TestCaseData> IntersectTestData()
        {
            yield return new TestCaseData(
                new List<int>() { }, new List<int> { }, new List<int> { }).SetCategory("Empty");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { }, new List<int> { }).SetCategory("Empty and not empty");
            yield return new TestCaseData(
                new List<int>() { }, new List<int> { 1, 2, 3 }, new List<int> { }).SetCategory("Empty and not empty");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 1, 2, 3 }, new List<int> { 1, 2, 3 }).SetCategory("Equals");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 3, 2, 1 }, new List<int> { 1, 2, 3 }).SetCategory("Equals but different order");
            yield return new TestCaseData(
                new List<int>() { 3, 2, 1 }, new List<int> { 2, 1, 3 }, new List<int> { 1, 2, 3 }).SetCategory("Equals but different order");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 1, 2, 3, 4 }, new List<int> { 1, 2, 3 }).SetCategory("Proper subset");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 4, 3, 2, 1 }, new List<int> { 1, 2, 3 }).SetCategory("Proper subset");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3, 4 }, new List<int> { 1, 4, 2 }, new List<int> { 1, 2, 4 }).SetCategory("Proper superset");
            yield return new TestCaseData(
                new List<int>() { 4, 3, 2, 1 }, new List<int> { 1, 4, 2 }, new List<int> { 1, 2, 4 }).SetCategory("Proper superset");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 1, 4, 2 }, new List<int> { 1, 2 }).SetCategory("Overlaps");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 3, 4, 5 }, new List<int> { 3 }).SetCategory("Overlaps");
            yield return new TestCaseData(
                new List<int>() { 1, 2, 3 }, new List<int> { 4, 5, 6 }, new List<int> { }).SetCategory("Not overlaps");
            yield return new TestCaseData(
                new List<int>() { 1, 3, 2 }, new List<int> { 6, 5, 4 }, new List<int> { }).SetCategory("Not overlaps");
        }

        [TestCaseSource(nameof(IntersectTestData))]
        [Test]
        public void SetShouldIntersect(List<int> first, List<int> second, List<int> result)
        {
            foreach (var item in first)
            {
                this.set.Add(item);
            }

            this.set.IntersectWith(second);

            Assert.IsTrue(this.set.SetEquals(result));
        }

        private class DefaultComparer<T> : IComparer<T>
            where T : IComparable<T>
        {
            public int Compare(T first, T second)
            {
                return first.CompareTo(second);
            }
        }
    }
}