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
        private StreamReader reader;
        private StreamWriter writer;
        private NetworkStream stream;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientFTP"/> class.
        /// </summary>
        /// <param name="address">The DNS name of the remote host to which you intend to connect.</param>
        /// <param name="port">The port number of the remote host to which you intend to connect.</param>
        public ClientFTP(string address, int port)
        {
            this.clientSocket = new TcpClient(address, port);

            this.stream = this.clientSocket.GetStream();

            this.writer = new StreamWriter(this.stream) { AutoFlush = true };

            this.reader = new StreamReader(this.stream);
        }

        /// <summary>
        /// List request to server.
        /// </summary>
        /// <param name="dirPath">Path to directory.</param>
        /// <returns>Directory listing and exists flag.</returns>
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
        /// <param name="destinationPath">Path to destination file.</param>
        /// <returns>Exists flag.</returns>
        public async Task<bool> GetRequestAsync(string filePath, string destinationPath)
        {
            await this.writer.WriteLineAsync($"2 {filePath}");

            var sizeStringElements = this.LoadChars().TakeWhile(x => x != ' ' && x != '\n');
            var sizeArray = await sizeStringElements.ToArrayAsync();
            var sizeString = new string(sizeArray);

            if (long.TryParse(sizeString, out long size))
            {
                if (size == -1)
                {
                    return false;
                }

                using (var destinationFile = File.Create(destinationPath))
                {
                    if (size > 0)
                    {
                        const int bufferSize = 81920;
                        byte[] buffer = new byte[bufferSize];
                        long readed = 0;

                        long needLoad = size - readed;

                        while (needLoad > 0)
                        {
                            if (needLoad < bufferSize)
                            {
                                readed += await this.stream.ReadAsync(buffer, 0, (int)needLoad);

                                if (readed != size)
                                {
                                    throw new InvalidOperationException("Wrong message");
                                }

                                await destinationFile.WriteAsync(buffer, 0, (int)needLoad);
                            }
                            else
                            {
                                readed += await this.stream.ReadAsync(buffer);
                                await destinationFile.WriteAsync(buffer);
                            }

                            needLoad = size - readed;
                        }

                        if (readed != size)
                        {
                            throw new InvalidOperationException("Wrong message");
                        }
                    }
                }

                return true;
            }
            else
            {
                throw new InvalidOperationException("Wrong message");
            }
        }

        /// <summary>
        /// Close connection function.
        /// </summary>
        public void Close()
        {
            this.reader.Close();
            this.writer.Close();
            this.clientSocket.Close();
        }

        /// <summary>
        /// Dispose function.
        /// </summary>
        public void Dispose()
        {
            this.Close();
        }

        private async IAsyncEnumerable<char> LoadChars()
        {
            char[] symbol = new char[1];
            int result;

            while (true)
            {
                result = await this.reader.ReadAsync(symbol);

                if (result == 1)
                {
                    yield return symbol[0];
                }
                else
                {
                    break;
                }
            }
        }
    }
}
