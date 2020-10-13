namespace Solution.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using NUnit.Framework;

    internal class FTPTest
    {
        private static int portOffset = 0;

        private ServerFTP server;
        private ClientFTP client;
        private string testDir;
        private CancellationTokenSource cancellationTokenSource;

        [SetUp]
        public void Setup()
        {
            int maxOffset = 100;
            int portStart = 10000;
            int currentPortOffset = Interlocked.Increment(ref portOffset);
            int currentPort = portStart + (currentPortOffset % maxOffset);

            this.cancellationTokenSource = new CancellationTokenSource();

            this.server = new ServerFTP(IPAddress.Loopback, currentPort);
            this.client = new ClientFTP("127.0.0.1", currentPort);

            this.testDir = Directory.GetCurrentDirectory() + "/TestDir/FTPTest";

            Directory.CreateDirectory(this.testDir);

            Task.Run(async () =>
            {
                await this.server.Run(this.cancellationTokenSource.Token);
            });
        }

        [TearDown]
        public void TearDown()
        {
            this.cancellationTokenSource.Cancel();
        }

        [Test]
        public async Task ListShouldGetMinusOne()
        {
            this.testDir += "/ListShouldGetMinusOne_DirectoryNotExists";

            var (_, flag) = await this.client.ListRequestAsync(this.testDir);

            Assert.AreEqual(false, flag);
        }

        [Test]
        public async Task ListShouldGetMinusOneEmpty()
        {
            var (_, flag) = await this.client.ListRequestAsync(string.Empty);

            Assert.AreEqual(false, flag);
        }

        [Test]
        public async Task ListShouldGetEmptyDir()
        {
            this.testDir += "/ListShouldGetEmptyDir";

            Directory.CreateDirectory(this.testDir);

            var (list, flag) = await this.client.ListRequestAsync(this.testDir);

            Assert.AreEqual(true, flag);
            Assert.AreEqual(0, list.Count);
        }

        [Test]
        public async Task ListShouldGetFilesAndDirs()
        {
            this.testDir += "/ListShouldGetFilesAndDirs";

            Directory.CreateDirectory(this.testDir);

            var filePath = this.testDir + "/file";

            File.Create(filePath);

            var dirPath = this.testDir + "/dir";

            Directory.CreateDirectory(dirPath);

            var (list, flag) = await this.client.ListRequestAsync(this.testDir);

            Assert.AreEqual(true, flag);

            list.Sort();

            Func<(string, bool), (string, bool)> function = element =>
                (element.Item1.Replace('\\', '/'), element.Item2);

            list = list.Select(function).ToList();

            dirPath = dirPath.Replace('\\', '/');
            filePath = filePath.Replace('\\', '/');

            var expected = new List<(string, bool)>() { (dirPath, true), (filePath, false) };

            Assert.AreEqual(expected, list);
        }

        [Test]
        public async Task GetShouldGetMinusOne()
        {
            this.testDir += "/GetShouldGetMinusOne";
            var testFile = this.testDir + "/FileNotExists";

            var flag = await this.client.GetRequestAsync(testFile, string.Empty);

            Assert.AreEqual(false, flag);
        }

        [Test]
        public async Task GetShouldGetMinusOneEmpty()
        {
            var flag = await this.client.GetRequestAsync(string.Empty, string.Empty);

            Assert.AreEqual(false, flag);
        }

        [Test]
        public async Task GetShouldGetEmpty()
        {
            this.testDir += "/GetShouldGetEmpty";
            var testFile = this.testDir + "/EmptyFile";

            Directory.CreateDirectory(this.testDir);
            File.Create(testFile).Close();

            var destinationFilePath = this.testDir + "/DestFile";

            var flag = await this.client.GetRequestAsync(testFile, destinationFilePath);

            Assert.AreEqual(true, flag);

            var destinationFile = File.OpenText(destinationFilePath);
            var result = destinationFile.ReadToEnd();

            Assert.AreEqual(string.Empty, result);
        }

        [Test]
        public async Task GetShouldGetFile()
        {
            this.testDir += "/GetShouldGetFile";
            var testFile = this.testDir + "/File";

            Directory.CreateDirectory(this.testDir);

            using (var fileStream = File.CreateText(testFile))
            {
                fileStream.Write("test");
            }

            var destinationFilePath = this.testDir + "/DestFile";

            var flag = await this.client.GetRequestAsync(testFile, destinationFilePath);

            Assert.AreEqual(true, flag);

            var destinationFile = File.OpenText(destinationFilePath);
            var result = destinationFile.ReadToEnd();

            Assert.AreEqual("test", result);
        }
    }
}
