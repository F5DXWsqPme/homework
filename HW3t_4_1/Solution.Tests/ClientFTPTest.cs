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

        private TcpListener listener;
        private ClientFTP client;

        [SetUp]
        public void Setup()
        {
            int maxOffset = 100;
            int portStart = 11000;
            int currentPortOffset = Interlocked.Increment(ref portOffset);
            int currentPort = portStart + (currentPortOffset % maxOffset);

            this.listener = new TcpListener(IPAddress.Loopback, currentPort);
            this.listener.Start();

            this.client = new ClientFTP("127.0.0.1", currentPort);
        }

        [Test]
        public async Task ListShouldSend1AndPath()
        {
            string request = string.Empty;

            var task = Task.Run(() =>
            {
                var socket = this.listener.AcceptSocket();
                var stream = new NetworkStream(socket);
                var reader = new StreamReader(stream);

                Volatile.Write(ref request, reader.ReadLine());

                var writer = new StreamWriter(stream) { AutoFlush = true };

                writer.WriteLine("-1");
            });

            var result = await this.client.ListRequestAsync("dir");

            Assert.AreEqual("1 dir", Volatile.Read(ref request));

            await task;
        }

        [Test]
        public async Task GetShouldSend2AndPath()
        {
            string request = string.Empty;

            var task = Task.Run(() =>
            {
                var socket = this.listener.AcceptSocket();
                var stream = new NetworkStream(socket);
                var reader = new StreamReader(stream);

                Volatile.Write(ref request, reader.ReadLine());

                var writer = new StreamWriter(stream) { AutoFlush = true };

                writer.WriteLine("-1");
            });

            var result = await this.client.GetRequestAsync("file");

            Assert.AreEqual("2 file", Volatile.Read(ref request));

            await task;
        }

        [Test]
        public async Task ListShouldReceiveNotExists()
        {
            var task = Task.Run(() =>
            {
                var socket = this.listener.AcceptSocket();
                var stream = new NetworkStream(socket);
                var writer = new StreamWriter(stream) { AutoFlush = true };

                writer.WriteLine("-1");
            });

            var (_, flag) = await this.client.ListRequestAsync("dir");

            Assert.AreEqual(false, flag);

            await task;
        }

        [Test]
        public async Task GetShouldReceiveNotExists()
        {
            var task = Task.Run(() =>
            {
                var socket = this.listener.AcceptSocket();
                var stream = new NetworkStream(socket);
                var writer = new StreamWriter(stream) { AutoFlush = true };

                writer.WriteLine("-1");
            });

            var (_, flag) = await this.client.GetRequestAsync("file");

            Assert.AreEqual(false, flag);

            await task;
        }

        [Test]
        public async Task ListShouldReceive()
        {
            var task = Task.Run(() =>
            {
                var socket = this.listener.AcceptSocket();
                var stream = new NetworkStream(socket);
                var writer = new StreamWriter(stream) { AutoFlush = true };

                writer.WriteLine("2 dir true file false");
            });

            var (list, flag) = await this.client.ListRequestAsync("dir");

            Assert.AreEqual(true, flag);

            var expected = new List<(string, bool)>() { ("dir", true), ("file", false) };

            Assert.AreEqual(expected, list);

            await task;
        }

        [Test]
        public async Task GetShouldReceive()
        {
            var task = Task.Run(() =>
            {
                var socket = this.listener.AcceptSocket();
                var stream = new NetworkStream(socket);
                var writer = new StreamWriter(stream) { AutoFlush = true };

                writer.WriteLine("10 file\n file");
            });

            var (file, flag) = await this.client.GetRequestAsync("file");

            Assert.AreEqual(true, flag);
            Assert.AreEqual("file\n file", file);

            await task;
        }

        [Test]
        public void GetShouldThrowWhenMessageWrong()
        {
            Task.Run(() =>
            {
                var socket = this.listener.AcceptSocket();
                var stream = new NetworkStream(socket);
                var writer = new StreamWriter(stream) { AutoFlush = true };

                writer.WriteLine(string.Empty);
                writer.WriteLine("file_without_size");
                writer.WriteLine("file without size");
                writer.WriteLine("11 small_file");
                writer.WriteLine("9 big_file");
            });

            for (int i = 0; i < 5; i++)
            {
                var exception = Assert.ThrowsAsync<AggregateException>(async () =>
                {
                    var result = await this.client.GetRequestAsync("file");
                });

                Assert.IsTrue(exception.InnerException is InvalidOperationException);
            }
        }

        [Test]
        public void ListShouldThrowWhenMessageWrong()
        {
            Task.Run(() =>
            {
                var socket = this.listener.AcceptSocket();
                var stream = new NetworkStream(socket);
                var writer = new StreamWriter(stream) { AutoFlush = true };

                writer.WriteLine(string.Empty);
                writer.WriteLine("dir true");
                writer.WriteLine("dir_true");
                writer.WriteLine("1 dir true file false");
                writer.WriteLine("2 file false");
            });

            for (int i = 0; i < 5; i++)
            {
                var exception = Assert.ThrowsAsync<AggregateException>(async () =>
                {
                    var result = await this.client.ListRequestAsync("dir");
                });

                Assert.IsTrue(exception.InnerException is InvalidOperationException);
            }
        }
    }
}
