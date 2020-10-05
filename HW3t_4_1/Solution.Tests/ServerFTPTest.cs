namespace Solution.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;
    using System.Threading.Tasks;
    using NUnit.Framework;

    internal class ServerFTPTest
    {
        private static int portOffset = 0;

        private TcpClient client;
        private StreamReader reader;
        private StreamWriter writer;
        private ServerFTP server;
        private CancellationTokenSource cancellationTokenSource;
        private string testDir;

        [SetUp]
        public void Setup()
        {
            int maxOffset = 100;
            int portStart = 12000;
            int currentPortOffset = Interlocked.Increment(ref portOffset);
            int currentPort = portStart + (currentPortOffset % maxOffset);

            this.cancellationTokenSource = new CancellationTokenSource();

            this.server = new ServerFTP(IPAddress.Loopback, currentPort);
            this.client = new TcpClient("127.0.0.1", currentPort);

            var stream = this.client.GetStream();

            this.writer = new StreamWriter(stream) { AutoFlush = true };
            this.reader = new StreamReader(stream);

            this.testDir = Directory.GetCurrentDirectory() + "/TestDir/ServerFTPTest";

            Directory.CreateDirectory(this.testDir);
        }

        [Test]
        public async Task ListShouldReturnMinusOneEmpty()
        {
            var response = string.Empty;

            var task = Task.Run(() =>
            {
                this.writer.WriteLine("1 ");

                Volatile.Write(ref response, this.reader.ReadLine());

                this.cancellationTokenSource.Cancel();
                this.client.Close();
            });

            await this.server.Run(this.cancellationTokenSource.Token);

            await task;

            Assert.AreEqual("-1", Volatile.Read(ref response));
        }

        [Test]
        public async Task GetShouldReturnMinusOneEmpty()
        {
            var response = string.Empty;

            var task = Task.Run(() =>
            {
                this.writer.WriteLine("2 ");

                Volatile.Write(ref response, this.reader.ReadLine());

                this.cancellationTokenSource.Cancel();
                this.client.Close();
            });

            await this.server.Run(this.cancellationTokenSource.Token);

            await task;

            Assert.AreEqual("-1", Volatile.Read(ref response));
        }

        [Test]
        public async Task ListShouldReturnMinusOne()
        {
            this.testDir += "/ListShouldReturnMinusOne_NotExists";
            var response = string.Empty;

            var task = Task.Run(() =>
            {
                this.writer.WriteLine($"1 {this.testDir}");

                Volatile.Write(ref response, this.reader.ReadLine());

                this.cancellationTokenSource.Cancel();
                this.client.Close();
            });

            await this.server.Run(this.cancellationTokenSource.Token);

            await task;

            Assert.AreEqual("-1", Volatile.Read(ref response));
        }

        [Test]
        public async Task GetShouldReturnMinusOne()
        {
            this.testDir += "/GetShouldReturnMinusOne_NotExists";
            var filePath = $"{this.testDir}/NotExistsFile";
            var response = string.Empty;

            var task = Task.Run(() =>
            {
                this.writer.WriteLine($"2 {filePath}");

                Volatile.Write(ref response, this.reader.ReadLine());

                this.cancellationTokenSource.Cancel();
                this.client.Close();
            });

            await this.server.Run(this.cancellationTokenSource.Token);

            await task;

            Assert.AreEqual("-1", Volatile.Read(ref response));
        }

        [Test]
        public async Task ListShouldReturnEmpty()
        {
            this.testDir += "/ListShouldReturnEmpty";

            Directory.CreateDirectory(this.testDir);

            var response = string.Empty;

            var task = Task.Run(() =>
            {
                this.writer.WriteLine($"1 {this.testDir}");

                Volatile.Write(ref response, this.reader.ReadLine());

                this.cancellationTokenSource.Cancel();
                this.client.Close();
            });

            await this.server.Run(this.cancellationTokenSource.Token);

            await task;

            Assert.AreEqual("0 ", Volatile.Read(ref response));
        }

        [Test]
        public async Task GetShouldReturnEmpty()
        {
            this.testDir += "/GetShouldReturnEmpty";
            var filePath = this.testDir + "/File";

            Directory.CreateDirectory(this.testDir);
            File.Create(filePath).Close();

            var response = string.Empty;

            var task = Task.Run(() =>
            {
                this.writer.WriteLine($"2 {filePath}");

                Volatile.Write(ref response, this.reader.ReadLine());

                this.cancellationTokenSource.Cancel();
                this.client.Close();
            });

            await this.server.Run(this.cancellationTokenSource.Token);

            await task;

            Assert.AreEqual("0 ", Volatile.Read(ref response));
        }

        [Test]
        public async Task ListShouldReturnFilesAndDirectories()
        {
            this.testDir += "/ListShouldReturnFilesAndDirectories";

            Directory.CreateDirectory(this.testDir);

            Directory.CreateDirectory(this.testDir + "/Dir");
            File.Create(this.testDir + "/File").Close();

            var response = string.Empty;

            var task = Task.Run(() =>
            {
                this.writer.WriteLine($"1 {this.testDir}");

                Volatile.Write(ref response, this.reader.ReadLine());

                this.cancellationTokenSource.Cancel();
                this.client.Close();
            });

            await this.server.Run(this.cancellationTokenSource.Token);

            await task;

            response = Volatile.Read(ref response).Replace('\\', '/');

            this.testDir = this.testDir.Replace('\\', '/');

            var firstExpected = $"2 {this.testDir}/File false {this.testDir}/Dir true";
            var secondExpected = $"2 {this.testDir}/Dir true {this.testDir}/File false";

            var firstEqual = firstExpected == response;
            var secondEqual = secondExpected == response;

            Assert.IsTrue(firstEqual || secondEqual);
        }

        [Test]
        public async Task GetShouldReturnFile()
        {
            this.testDir += "/GetShouldReturnFile";

            Directory.CreateDirectory(this.testDir);

            var filePath = this.testDir + "/File";

            using (var fileStream = File.CreateText(filePath))
            {
                fileStream.Write("test");
            }

            var response = string.Empty;

            var task = Task.Run(() =>
            {
                this.writer.WriteLine($"2 {filePath}");

                Volatile.Write(ref response, this.reader.ReadLine());

                this.cancellationTokenSource.Cancel();
                this.client.Close();
            });

            await this.server.Run(this.cancellationTokenSource.Token);

            await task;

            Assert.AreEqual("4 test", Volatile.Read(ref response));
        }

        [Test]
        public async Task ServerShouldReturnRequestWrong()
        {
            var requests = new List<string>()
            {
                "wrong_message",
                "wrong message",
                "0 File",
                "3 Dir",
                "1",
                "2",
            };

            var task = Task.Run(async () =>
            {
                await this.server.Run(this.cancellationTokenSource.Token);
            });

            for (int i = 0; i < requests.Count; i++)
            {
                this.writer.WriteLine(requests[i]);
            }

            for (int i = 0; i < requests.Count; i++)
            {
                Assert.AreEqual("Wrong request", this.reader.ReadLine());
            }

            this.cancellationTokenSource.Cancel();
            this.client.Close();

            await task;
        }
    }
}
