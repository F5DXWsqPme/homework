namespace Solution
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    /// <summary>
    /// Client class.
    /// </summary>
    public class Client : IDisposable, IMessenger
    {
        private TcpClient clientSocket;
        private StreamReader reader;
        private StreamWriter writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <param name="address">The DNS name of the remote host to which you intend to connect.</param>
        /// <param name="port">The port number of the remote host to which you intend to connect.</param>
        public Client(string address, int port)
        {
            this.clientSocket = new TcpClient(address, port);

            var stream = this.clientSocket.GetStream();

            this.writer = new StreamWriter(stream) { AutoFlush = true };
            this.reader = new StreamReader(stream);
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
        /// Close client function.
        /// </summary>
        public void Close()
        {
            this.reader.Close();
            this.writer.Close();
            this.clientSocket.Close();
        }

        /// <summary>
        /// Dispose client function.
        /// </summary>
        public void Dispose()
        {
            this.Close();
        }
    }
}
