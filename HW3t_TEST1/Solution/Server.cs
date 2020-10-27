namespace Solution
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Server class.
    /// </summary>
    public class Server : IDisposable, IMessenger
    {
        private TcpListener listener;
        private StreamReader reader;
        private StreamWriter writer;
        private TcpClient client;

        /// <summary>
        /// Initializes a new instance of the <see cref="Server"/> class.
        /// </summary>
        /// <param name="port">Server port.</param>
        public Server(int port)
        {
            this.listener = new TcpListener(IPAddress.Any, port);
            this.listener.Start();

            this.client = this.listener.AcceptTcpClient();

            this.reader = new StreamReader(this.client.GetStream());
            this.writer = new StreamWriter(this.client.GetStream()) { AutoFlush = true };
        }

        /// <summary>
        /// Read line function.
        /// </summary>
        /// <returns>Task for waiting received message.</returns>
        public async Task<string> Receive() =>
            await this.reader.ReadLineAsync();

        /// <summary>
        /// Send message.
        /// </summary>
        /// <param name="data">Message for sending.</param>
        /// <returns>Task for waiting.</returns>
        public async Task Send(string data) =>
            await this.writer.WriteLineAsync(data);

        /// <summary>
        /// Close server function.
        /// </summary>
        public void Close()
        {
            this.reader.Close();
            this.writer.Close();
            this.client.Close();
            this.listener.Stop();
        }

        /// <summary>
        /// Dispose server function.
        /// </summary>
        public void Dispose()
        {
            this.Close();
        }
    }
}
