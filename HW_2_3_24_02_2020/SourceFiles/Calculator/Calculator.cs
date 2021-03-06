﻿/// <summary>
/// Global namespace.
/// </summary>
namespace HW_2_3_24_02_2020
{
    /// <summary>
    /// Class with implementation of stack-calculator.
    /// </summary>
    public class Calculator
    {
        private Scanner scanner;
        private Evaluator evaluator;

        /// <summary>
        /// Initializes a new instance of the <see cref="Calculator"/> class.
        /// </summary>
        /// <param name="stack">Stack for evaluation.</param>
        public Calculator(IStack stack)
        {
            this.scanner = new Scanner();
            this.evaluator = new Evaluator(stack);
        }

        /// <summary>
        /// Evaluate result of expression in Reverse-Polish-Notation.
        /// </summary>
        /// <param name="input">Input string with expression.</param>
        /// <returns>Result of expression.</returns>
        /// <exception cref="System.ArgumentException">Throws when expreession dont correct.</exception>
        public double Evaluate(string input)
        {
            IQueue scannedTokens = this.scanner.CreateTokensQueue(input);

            return this.evaluator.Evaluate(scannedTokens);
        }
    }
}
