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
                    if (numberString.Length != 0)
                    {
                        tokens.Put(new Number(double.Parse(numberString)));

                        numberString = new string(string.Empty);
                    }

                    if (!char.IsWhiteSpace(symbol))
                    {
                        tokens.Put(new Operator(symbol));
                    }
                }
            }

            return tokens;
        }
    }
}
