namespace Solution
{
    /// <summary>
    /// Class with implementation exception queue empty.
    /// </summary>
    public class QueueEmptyException : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueueEmptyException"/> class.
        /// </summary>
        public QueueEmptyException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueEmptyException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public QueueEmptyException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueEmptyException"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public QueueEmptyException(string message, System.Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueEmptyException"/> class.
        /// </summary>
        /// <param name="info">Sarialization information.</param>
        /// <param name="context">Streaming context.</param>
        protected QueueEmptyException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
