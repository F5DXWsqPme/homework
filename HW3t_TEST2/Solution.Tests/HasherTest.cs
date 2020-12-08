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

        [Test]
        public void HasherShouldGetEqualHash()
        {
            var testDir1 = "TestDir/HasherShouldGetDifferentHash1";
            var testDir2 = "TestDir/HasherShouldGetDifferentHash2";

            Directory.CreateDirectory(testDir1);
            Directory.CreateDirectory(testDir1 + "/dir");
            File.CreateText(testDir1 + "/file").Close();

            Directory.CreateDirectory(testDir2);
            Directory.CreateDirectory(testDir2 + "/dir");
            File.CreateText(testDir2 + "/file").Close();

            var (_, hash1) = Hasher.EvaluateHash(testDir1);
            var (_, hash2) = Hasher.EvaluateHash(testDir2);

            Assert.AreEqual(hash1, hash2);
        }

        [Test]
        public void HasherShouldGetNotEqualHash()
        {
            var testDir1 = "TestDir/HasherShouldGetDifferentHash1";
            var testDir2 = "TestDir/HasherShouldGetDifferentHash2";

            Directory.CreateDirectory(testDir1);
            Directory.CreateDirectory(testDir1 + "/dir");
            File.CreateText(testDir1 + "/file").Close();

            Directory.CreateDirectory(testDir2);
            Directory.CreateDirectory(testDir2 + "/dir");
            File.CreateText(testDir2 + "/otherFile").Close();

            var (_, hash1) = Hasher.EvaluateHash(testDir1);
            var (_, hash2) = Hasher.EvaluateHash(testDir2);

            Assert.AreNotEqual(hash1, hash2);
        }
    }
}
