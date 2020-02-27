namespace HW_2_3_24_02_2020
{
    internal class Scanner
    {
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
