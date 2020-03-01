﻿/// <summary>
/// Global namespace.
/// </summary>
namespace HW_3_1_01_03_2020
{
    /// <summary>
    /// Class with imimplementation of tokens scanner.
    /// </summary>
    internal class Scanner
    {
        /// <summary>
        /// Split input string to tokens queue.
        /// </summary>
        /// <param name="input">Input string.</param>
        /// <returns>Tokens queue.</returns>
        public IQueue CreateTokensQueue(string input)
        {
            IQueue tokens = (IQueue)new QueueArray();
            string numberString = new string(string.Empty);

            foreach (char symbol in input)
            {
                if (char.IsDigit(symbol) || symbol == '.')
                {
                    numberString += symbol;
                }
                else
                {
                    this.TryToCreateNumber(numberString, tokens);
                    numberString = new string(string.Empty);

                    if (!char.IsWhiteSpace(symbol))
                    {
                        tokens.Put(new Operator(symbol));
                    }
                }
            }

            this.TryToCreateNumber(numberString, tokens);

            if (tokens.IsEmpty())
            {
                throw new System.Exception("Tokens queue not created (maybe you entered an empty string)");
            }

            return tokens;
        }

        /// <summary>
        /// Try to convert string to number token (<see cref="Number"/>) and put number to tokens queue.
        /// </summary>
        /// <param name="numberString">Input string.</param>
        /// <param name="tokens">Tokens queue.</param>
        private void TryToCreateNumber(string numberString, IQueue tokens)
        {
            if (numberString.Length != 0)
            {
                try
                {
                    tokens.Put(new Number(double.Parse(numberString)));
                }
                catch
                {
                    throw new System.Exception($"Wrong number '{numberString}'");
                }
            }
        }
    }
}
