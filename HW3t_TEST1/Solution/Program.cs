namespace Solution
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Class with main program.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Main function in program.
        /// </summary>
        /// <param name="args">Program arguments.</param>
        private static async Task Main(string[] args)
        {
            IMessenger messenger;

            if (args.Length == 1)
            {
                if (int.TryParse(args[0], out int port))
                {
                    messenger = new Server(port);
                }
                else
                {
                    Console.WriteLine("Wrong arguments");
                    return;
                }
            }
            else if (args.Length == 2)
            {
                if (int.TryParse(args[1], out int port))
                {
                    messenger = new Client(args[0], port);
                }
                else
                {
                    Console.WriteLine("Wrong arguments");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Wrong arguments");
                return;
            }

            await Run(messenger);

            messenger.Close();
        }

        private static async Task Run(IMessenger messenger)
        {
            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;

            using (cancellationToken.Register(() => messenger.Close()))
            {
                var taskSender = Task.Run(async () =>
                {
                    try
                    {
                        while (!cancellationToken.IsCancellationRequested)
                        {
                            var data = Console.ReadLine();

                            await messenger.Send(data);

                            if (data == "exit")
                            {
                                cancellationTokenSource.Cancel();
                                break;
                            }
                        }
                    }
                    catch
                    {
                        cancellationTokenSource.Cancel();
                        messenger.Close();
                    }
                });

                var taskReceiver = Task.Run(async () =>
                {
                    try
                    {
                        while (!cancellationToken.IsCancellationRequested)
                        {
                            var data = await messenger.Receive();

                            if (data == "exit")
                            {
                                cancellationTokenSource.Cancel();
                                break;
                            }

                            Console.WriteLine(data);
                        }
                    }
                    catch
                    {
                        cancellationTokenSource.Cancel();
                        messenger.Close();
                    }
                });

                await taskReceiver;
            }
        }
    }
}
