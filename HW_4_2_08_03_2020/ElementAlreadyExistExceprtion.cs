/// <summary>
/// Global namespace.
/// </summary>
namespace HW_4_2_08_03_2020
{
    /// <summary>
    /// Class with implementation exception element already exist.
    /// </summary>
    public class ElementAlreadyExistExceprtion : System.Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElementAlreadyExistExceprtion"/> class.
        /// </summary>
        public ElementAlreadyExistExceprtion()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementAlreadyExistExceprtion"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        public ElementAlreadyExistExceprtion(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementAlreadyExistExceprtion"/> class.
        /// </summary>
        /// <param name="message">Exception message.</param>
        /// <param name="inner">Inner exception.</param>
        public ElementAlreadyExistExceprtion(string message, System.Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElementAlreadyExistExceprtion"/> class.
        /// </summary>
        /// <param name="info">Sarialization information.</param>
        /// <param name="context">Streaming context.</param>
        protected ElementAlreadyExistExceprtion(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context)
        {
        }
    }
}
