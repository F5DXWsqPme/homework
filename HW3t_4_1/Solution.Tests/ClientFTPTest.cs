namespace Solution.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
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
            var task = Task.Run(async () =>
            {
                await this.server.AcceptClientAsync();

                await this.server.WriteLineAsync("-1");
            });

            var (_, flag) = await this.client.ListRequestAsync("dir");

            Assert.AreEqual(false, flag);

            await task;
        }

        [Test]
        public async Task GetShouldReceiveNotExists()
        {
            var task = Task.Run(async () =>
            {
                await this.server.AcceptClientAsync();

                await this.server.WriteLineAsync("-1");
            });

            var flag = await this.client.GetRequestAsync("file", string.Empty);

            Assert.AreEqual(false, flag);

            await task;
        }

        [Test]
        public async Task ListShouldReceive()
        {
            var task = Task.Run(async () =>
            {
                await this.server.AcceptClientAsync();

                await this.server.WriteLineAsync("2 dir true file false");
            });

            var (list, flag) = await this.client.ListRequestAsync("dir");

            Assert.AreEqual(true, flag);

            var expected = new List<(string, bool)>() { ("dir", true), ("file", false) };

            Assert.AreEqual(expected, list);

            await task;
        }

        [Test]
        public async Task GetShouldReceiveFile()
        {
            this.testDir += "/GetShouldReceiveFile";

            Directory.CreateDirectory(this.testDir);

            var filePath = this.testDir + "/file";

            using (var file = File.CreateText(filePath))
            {
                await file.WriteAsync("test");
            }

            var task = Task.Run(async () =>
            {
                await this.server.AcceptClientAsync();

                var information = new FileInfo(filePath);

                await this.server.WriteAsync($"{information.Length} ");

                using (var file = File.OpenRead(filePath))
                {
                    await this.server.CopyFileAsync(file);
                }
            });

            var destinationFilePath = this.testDir + "/DestFile";

            /*
            var flag = await this.client.GetRequestAsync("file", destinationFilePath);

            Assert.AreEqual(true, flag);

            var destinationFile = File.OpenText(destinationFilePath);
            var result = destinationFile.ReadToEnd();

            Assert.AreEqual("file", result);
            */

            await task;
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
            private NetworkStream stream;
            private TcpClient client;

            public TestServer(IPAddress address, int port)
            {
                this.listener = new TcpListener(address, port);
                this.listener.Start();
            }

            public async Task AcceptClientAsync()
            {
                this.client = await this.listener.AcceptTcpClientAsync();
                this.stream = this.client.GetStream();
                this.reader = new StreamReader(this.stream);
                this.writer = new StreamWriter(this.stream) { AutoFlush = true };
            }

            public async Task<string> ReadLineAsync() =>
                await this.reader.ReadLineAsync();

            public async Task WriteLineAsync(string data) =>
                await this.writer.WriteLineAsync(data);

            public async Task WriteAsync(string data) =>
                await this.writer.WriteAsync(data);

            public async Task CopyFileAsync(FileStream file)
            {
                await file.CopyToAsync(this.stream);
                await this.stream.FlushAsync();
                await this.writer.WriteLineAsync(string.Empty);
            }
        }
    }
}
