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

    internal class ClientFTPTest
    {
        private static int portOffset = 0;

        private TestServer server;
        private ClientFTP client;
        private string testDir;

        [SetUp]
        public void Setup()
        {
            const int maxOffset = 100;
            const int portStart = 11000;
            int currentPortOffset = Interlocked.Increment(ref portOffset);
            int currentPort = portStart + (currentPortOffset % maxOffset);

            this.server = new TestServer(IPAddress.Loopback, currentPort);

            this.client = new ClientFTP("127.0.0.1", currentPort);

            this.testDir = Directory.GetCurrentDirectory() + "/TestDir/ClientFTPTest";

            Directory.CreateDirectory(this.testDir);
        }

        [TearDown]
        public void TearDown()
        {
            this.server.Close();
        }

        [Test]
        public async Task ListShouldSend1AndPath()
        {
            var request = string.Empty;

            var task = Task.Run(async () =>
            {
                await this.server.AcceptClientAsync();

                Volatile.Write(ref request, await this.server.ReadLineAsync());

                await this.server.WriteLineAsync("-1");
            });

            var result = await this.client.ListRequestAsync("dir");

            Assert.AreEqual("1 dir", Volatile.Read(ref request));

            await task;
        }

        [Test]
        public async Task GetShouldSend2AndPath()
        {
            var request = string.Empty;

            var task = Task.Run(async () =>
            {
                await this.server.AcceptClientAsync();

                Volatile.Write(ref request, await this.server.ReadLineAsync());

                await this.server.WriteLineAsync("-1");
            });

            var result = await this.client.GetRequestAsync("file", string.Empty);

            Assert.AreEqual("2 file", Volatile.Read(ref request));

            await task;
        }

        [Test]
        public async Task ListShouldReceiveNotExists()
        {
            await this.server.AcceptClientAsync();

            await this.server.WriteLineAsync("-1");

            var (_, flag) = await this.client.ListRequestAsync("dir");

            Assert.AreEqual(false, flag);
        }

        [Test]
        public async Task GetShouldReceiveNotExists()
        {
            await this.server.AcceptClientAsync();

            await this.server.WriteLineAsync("-1");

            var flag = await this.client.GetRequestAsync("file", string.Empty);

            Assert.AreEqual(false, flag);
        }

        [Test]
        public async Task ListShouldReceive()
        {
            await this.server.AcceptClientAsync();

            await this.server.WriteLineAsync("2 dir true file false");

            var (list, flag) = await this.client.ListRequestAsync("dir");

            Assert.AreEqual(true, flag);

            var expected = new List<(string, bool)>() { ("dir", true), ("file", false) };

            Assert.AreEqual(expected, list);
        }

        [Test]
        public async Task GetShouldReceiveFile()
        {
            this.testDir += "/GetShouldReceiveFile";

            Directory.CreateDirectory(this.testDir);

            await this.server.AcceptClientAsync();

            await this.server.WriteLineAsync($"4 test");

            var destinationFilePath = this.testDir + "/DestFile";

            var flag = await this.client.GetRequestAsync("file", destinationFilePath);

            Assert.AreEqual(true, flag);

            var destinationFile = File.OpenText(destinationFilePath);
            var result = destinationFile.ReadToEnd();

            Assert.AreEqual("test", result);
        }

        [Test]
        public async Task GetShouldThrowWhenMessageWrong()
        {
            this.testDir += "/GetShouldThrowWhenMessageWrong";

            await this.server.AcceptClientAsync();

            var task = Task.Run(async () =>
            {
                await this.server.WriteLineAsync(string.Empty);
                await this.server.WriteLineAsync("file_without_size");
            });

            var destinationFilePath = this.testDir + "/DestFile";

            for (int i = 0; i < 2; i++)
            {
                var exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                {
                    var result = await this.client.GetRequestAsync("file", destinationFilePath);
                });
            }

            await task;
        }

        [Test]
        public async Task ListShouldThrowWhenMessageWrong()
        {
            await this.server.AcceptClientAsync();

            var task = Task.Run(async () =>
            {
                await this.server.WriteLineAsync(string.Empty);
                await this.server.WriteLineAsync("dir true");
                await this.server.WriteLineAsync("dir_true");
                await this.server.WriteLineAsync("1 dir true file false");
                await this.server.WriteLineAsync("2 file false");
            });

            for (int i = 0; i < 5; i++)
            {
                var exception = Assert.ThrowsAsync<InvalidOperationException>(async () =>
                {
                    var result = await this.client.ListRequestAsync("dir");
                });
            }

            await task;
        }

        private class TestServer
        {
            private TcpListener listener;
            private StreamReader reader;
            private StreamWriter writer;
            private TcpClient client;

            public TestServer(IPAddress address, int port)
            {
                this.listener = new TcpListener(address, port);
                this.listener.Start();
            }

            public async Task AcceptClientAsync()
            {
                this.client = await this.listener.AcceptTcpClientAsync();
                this.reader = new StreamReader(this.client.GetStream());
                this.writer = new StreamWriter(this.client.GetStream()) { AutoFlush = true };
            }

            public async Task<string> ReadLineAsync() =>
                await this.reader.ReadLineAsync();

            public async Task WriteLineAsync(string data) =>
                await this.writer.WriteLineAsync(data);

            public void Close()
            {
                this.reader.Close();
                this.writer.Close();
                this.client.Close();
            }
        }
    }
}
