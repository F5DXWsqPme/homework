namespace Solution
{
    using System;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    /// <summary>
    /// Messenger class.
    /// </summary>
    public interface IMessenger
    {
        /// <summary>
        /// Read line function.
        /// </summary>
        /// <returns>Task for waiting received message.</returns>
        public Task<string> Receive();

        /// <summary>
        /// Send message.
        /// </summary>
        /// <param name="data">Message for sending.</param>
        /// <returns>Task for waiting.</returns>
        public Task Send(string data);

        /// <summary>
        /// Close function.
        /// </summary>
        public void Close();
    }
}
