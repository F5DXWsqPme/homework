namespace Solution.Tests
{
    using NUnit.Framework;
    using System.IO;
    using System.Linq;

    public class HasherTest
    {
        [Test]
        public void HasherShouldGetHash()
        {
            var testDir = "TestDir/HasherShouldGetDifferentHashes";

            Directory.CreateDirectory(testDir);

            var (success, hash) = Hasher.EvaluateHash(testDir);

            Assert.IsTrue(success);
            Assert.AreEqual(128 / 8, hash.ToArray().Length);
        }

        [Test]
        public void HasherShouldGetHashWithFileAndDir()
        {
            var testDir = "TestDir/HasherShouldGetHashWithFileAndDir";

            Directory.CreateDirectory(testDir);
            Directory.CreateDirectory(testDir + "/dir");
            File.CreateText(testDir + "/file").Close();

            var (success, hash) = Hasher.EvaluateHash(testDir);

            Assert.IsTrue(success);
            Assert.AreEqual(128 / 8, hash.ToArray().Length);
        }
    }
}