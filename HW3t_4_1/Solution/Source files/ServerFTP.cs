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
            using (cancellationToken.Register(() => this.listener.Stop()))
            {
                try
                {
                    var tasks = new Queue<Task>();

                    while (!cancellationToken.IsCancellationRequested)
                    {
                        // This cycle is needed for handling requests in parallel mode.
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
                catch (InvalidOperationException)
                {
                }
            }
        }

        private async Task ProcessRequestAsync()
        {
            using var client = await this.listener.AcceptTcpClientAsync();
            using var reader = new StreamReader(client.GetStream());
            using var writer = new StreamWriter(client.GetStream()) { AutoFlush = true };

            while (!reader.EndOfStream)
            {
                var request = await reader.ReadLineAsync();
                int firstSpace = request.IndexOf(' ');

                if (firstSpace < 1)
                {
                    await writer.WriteLineAsync("Wrong request");
                }
                else
                {
                    var requestIdString = request.Substring(0, firstSpace);
                    var pathString = request.Substring(firstSpace + 1);

                    if (requestIdString == "1")
                    {
                        var result = await this.ProcessListRequestAsync(pathString);

                        await writer.WriteLineAsync(result);
                    }
                    else if (requestIdString == "2")
                    {
                        await this.ProcessGetRequestAsync(pathString, client.GetStream(), writer);
                    }
                    else
                    {
                        await writer.WriteLineAsync("Wrong request");
                    }
                }
            }
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

        private async Task ProcessGetRequestAsync(string filePath, NetworkStream stream, StreamWriter writer)
        {
            if (!File.Exists(filePath))
            {
                await writer.WriteLineAsync("-1");
                return;
            }

            var information = new FileInfo(filePath);

            await writer.WriteAsync($"{information.Length} ");

            using (var file = File.OpenRead(filePath))
            {
                await file.CopyToAsync(writer.BaseStream);
            }

            await stream.FlushAsync();

            await writer.WriteLineAsync(string.Empty);
        }
    }
}
