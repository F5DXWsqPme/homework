namespace HW_2_3_24_02_2020
{
    internal class Number : IToken
    {
        private double number;

        public Number(double number)
        {
            this.number = number;
        }

        public double Get()
        {
            return this.number;
        }
    }
}
