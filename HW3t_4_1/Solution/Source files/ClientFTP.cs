namespace Solution
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    /// <summary>
    /// FTP client class.
    /// </summary>
    public class ClientFTP : IDisposable
    {
        private TcpClient clientSocket;
        private StreamLoader loader;
        private StreamReader reader;
        private StreamWriter writer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientFTP"/> class.
        /// </summary>
        /// <param name="address">The DNS name of the remote host to which you intend to connect.</param>
        /// <param name="port">The port number of the remote host to which you intend to connect.</param>
        public ClientFTP(string address, int port)
        {
            this.clientSocket = new TcpClient(address, port);

            var stream = this.clientSocket.GetStream();

            this.writer = new StreamWriter(stream) { AutoFlush = true };

            this.reader = new StreamReader(stream);
            this.loader = new StreamLoader(this.reader);
        }

        /// <summary>
        /// List request to server.
        /// </summary>
        /// <param name="dirPath">Path to directory.</param>
        /// <returns>Dyrectory listing and exists flag.</returns>
        /// <exception cref="InvalidOperationException">Throws when client receive wrong response.</exception>
        public async Task<(List<(string, bool)>, bool)> ListRequestAsync(string dirPath)
        {
            await this.writer.WriteLineAsync($"1 {dirPath}");

            var response = await this.reader.ReadLineAsync();
            var responseStrings = response.Split(' ');

            responseStrings = responseStrings.Where((element) => !string.IsNullOrEmpty(element)).ToArray();

            if (responseStrings.Length < 1)
            {
                throw new InvalidOperationException("Wrong message");
            }

            var sizeString = responseStrings[0];

            if (int.TryParse(sizeString, out int size))
            {
                if (size == -1)
                {
                    return (null, false);
                }
                else
                {
                    if (responseStrings.Length != 1 + (size * 2))
                    {
                        throw new InvalidOperationException("Wrong message");
                    }

                    var list = new List<(string, bool)>();

                    for (int i = 0; i < size; i++)
                    {
                        if (bool.TryParse(responseStrings[(i * 2) + 2], out bool isDir))
                        {
                            list.Add((responseStrings[(i * 2) + 1], isDir));
                        }
                        else
                        {
                            throw new InvalidOperationException("Wrong message");
                        }
                    }

                    return (list, true);
                }
            }
            else
            {
                throw new InvalidOperationException("Wrong message");
            }
        }

        /// <summary>
        /// Get request to server.
        /// </summary>
        /// <param name="filePath">Path to file.</param>
        /// <returns>File and exists flag.</returns>
        public async Task<(string, bool)> GetRequestAsync(string filePath)
        {
            await this.writer.WriteLineAsync($"2 {filePath}");

            var sizeString = await this.loader.LoadUntilDelimeterAsync(' ', '\n');

            if (long.TryParse(sizeString, out long size))
            {
                if (size == -1)
                {
                    return (null, false);
                }

                var fileData = new char[size];
                int result = this.reader.Read(fileData);

                if (result == size)
                {
                    return (new string(fileData), true);
                }
                else
                {
                    throw new InvalidOperationException("Wrong message");
                }
            }
            else
            {
                throw new InvalidOperationException("Wrong message");
            }
        }

        /// <summary>
        /// Dispose function.
        /// </summary>
        public void Dispose()
        {
            this.clientSocket.Close();
        }
    }
}
