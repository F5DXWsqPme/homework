namespace Solution
{
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Stream loader class.
    /// </summary>
    public class StreamLoader
    {
        private StreamReader reader;

        /// <summary>
        /// Initializes a new instance of the <see cref="StreamLoader"/> class.
        /// </summary>
        /// <param name="reader">Reader for loading.</param>
        public StreamLoader(StreamReader reader)
        {
            this.reader = reader;
        }

        /// <summary>
        /// Load stream until delimeter after delimeters.
        /// </summary>
        /// <param name="delimeters">Delimeters.</param>
        /// <returns>String until delimeters.</returns>
        public async Task<string> LoadUntilDelimeterAsync(params char[] delimeters)
        {
            char[] symbol = new char[1];
            int result = await this.reader.ReadAsync(symbol);

            while (result == 1)
            {
                bool isDelimeter = false;

                for (int i = 0; i < delimeters.Length; i++)
                {
                    if (symbol[0] == delimeters[i])
                    {
                        isDelimeter = true;
                    }
                }

                if (!isDelimeter)
                {
                    break;
                }

                result = await this.reader.ReadAsync(symbol);
            }

            var resultString = string.Empty;

            while (result == 1)
            {
                bool isDelimeter = false;

                for (int i = 0; i < delimeters.Length; i++)
                {
                    if (symbol[0] == delimeters[i])
                    {
                        isDelimeter = true;
                    }
                }

                if (isDelimeter)
                {
                    break;
                }

                resultString += symbol[0];
                result = await this.reader.ReadAsync(symbol);
            }

            return resultString;
        }
    }
}
