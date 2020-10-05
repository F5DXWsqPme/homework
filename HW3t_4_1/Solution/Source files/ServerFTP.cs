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
    /// FTP server class.
    /// </summary>
    public class ServerFTP
    {
        private TcpListener listener;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServerFTP"/> class.
        /// </summary>
        /// <param name="address">Server port.</param>
        /// <param name="port">IP address.</param>
        public ServerFTP(IPAddress address, int port)
        {
            this.listener = new TcpListener(address, port);
            this.listener.Start();
        }

        /// <summary>
        /// Cycle function.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task for waiting.</returns>
        public async Task Run(CancellationToken cancellationToken = default(CancellationToken))
        {
            var tasks = new Queue<Task>();

            while (!cancellationToken.IsCancellationRequested)
            {
                if (this.listener.Pending())
                {
                    tasks.Enqueue(this.ProcessRequestAsync());
                }
                else
                {
                    Thread.Sleep(10);
                }
            }

            while (tasks.Count > 0)
            {
                var task = tasks.Dequeue();

                await task;
            }
        }

        private async Task ProcessRequestAsync()
        {
            var socket = await this.listener.AcceptSocketAsync();
            var stream = new NetworkStream(socket);
            var reader = new StreamReader(stream);

            while (!reader.EndOfStream)
            {
                var request = await reader.ReadLineAsync();

                var firstSpace = request.IndexOf(' ');

                var result = string.Empty;

                if (firstSpace < 1)
                {
                    result = "Wrong request";
                }
                else
                {
                    var requestIdString = request.Substring(0, firstSpace);
                    var pathString = request.Substring(firstSpace + 1);

                    result = requestIdString switch
                    {
                        "1" => await this.ProcessListRequestAsync(pathString),
                        "2" => await this.ProcessGetRequestAsync(pathString),
                        _ => "Wrong request",
                    };
                }

                var writer = new StreamWriter(stream) { AutoFlush = true };

                await writer.WriteLineAsync(result);
            }

            socket.Close();
        }

        private async Task<string> ProcessListRequestAsync(string dirPath)
        {
            return await Task.Run(() =>
            {
                if (!Directory.Exists(dirPath))
                {
                    return "-1";
                }

                var result = string.Empty;

                var files = Directory.EnumerateFiles(dirPath);
                foreach (var file in files)
                {
                    result += $" {file} false";
                }

                var directories = Directory.EnumerateDirectories(dirPath);
                foreach (var dir in directories)
                {
                    result += $" {dir} true";
                }

                int size = files.Count() + directories.Count();

                if (size == 0)
                {
                    return "0 ";
                }

                return $"{size}{result}";
            });
        }

        private async Task<string> ProcessGetRequestAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                return "-1";
            }

            var result = await File.ReadAllTextAsync(filePath);

            return $"{result.Length} {result}";
        }
    }
}
