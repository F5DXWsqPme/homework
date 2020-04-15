using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Global namespace.
/// </summary>
namespace Solution
{
    /// <summary>
    /// Class with implementation calculator state.
    /// </summary>
    public class CalculatorState
    {
        private decimal resultNumber;
        private decimal newNumber;
        private decimal sign;
        private string operatorString;
        private bool errorFlag;
        private bool equalFlag;
        private int displaySize;
        private int dotPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculatorState"/> class.
        /// </summary>
        /// <param name="displaySize">Output string length.</param>
        /// <exception cref="ArgumentException">Throws when display size less than 2.</exception>
        public CalculatorState(int displaySize)
        {
            int minimalDisplaySize = 2;
            if (displaySize < minimalDisplaySize)
            {
                throw new ArgumentException("Wrong dispaly size");
            }

            this.resultNumber = 0;
            this.newNumber = 0;
            this.errorFlag = false;
            this.displaySize = displaySize;
            this.operatorString = string.Empty;
            this.dotPosition = 0;
            this.sign = 1;
        }

        /// <summary>
        /// Gets string for display.
        /// </summary>
        /// <returns>String for display.</returns>
        public string GetOutputString()
        {
            if (Math.Abs(this.resultNumber) >= (decimal)Math.Pow(10, this.displaySize - 1))
            {
                this.errorFlag = true;
            }

            if (this.errorFlag)
            {
                string errorString = "ERROR";
                return errorString.Substring(0, Math.Min(this.displaySize, errorString.Length));
            }

            decimal value;

            if (this.operatorString == string.Empty)
            {
                value = this.resultNumber;
            }
            else
            {
                value = this.newNumber;
            }

            int numberOfIntegerDigits = (int)Math.Max(Math.Ceiling(Math.Log10(Math.Abs((double)value))), 1);
            int precision = Math.Min(Math.Max(this.displaySize - numberOfIntegerDigits - 2, 0), 28);

            value = Math.Round(value, precision, MidpointRounding.AwayFromZero);

            string valueString = value.ToString("G");

            if (!valueString.StartsWith("-"))
            {
                valueString = "+" + valueString;
            }

            if (this.dotPosition == 1)
            {
                valueString += ",";
            }

            return valueString.Substring(0, Math.Min(this.displaySize, valueString.Length));
        }

        /// <summary>
        /// Update calculatorr state function.
        /// </summary>
        /// <param name="clickedButtonText">Clicked button text.</param>
        /// <exception cref="ArgumentException">Throws when clickedButtonText unknown.</exception>
        public void Update(string clickedButtonText)
        {
            if (int.TryParse(clickedButtonText, out int digit))
            {
                this.UpdateDigit(digit, clickedButtonText);
            }
            else
            {
                this.equalFlag = false;

                if (clickedButtonText == "C")
                {
                    this.resultNumber = 0;
                    this.newNumber = 0;
                    this.dotPosition = 0;
                    this.errorFlag = false;
                    this.operatorString = string.Empty;
                    this.sign = 1;
                    this.equalFlag = false;
                }
                else if (clickedButtonText == ".")
                {
                    if (this.dotPosition != 0)
                    {
                        this.errorFlag = true;
                    }

                    this.dotPosition = 1;
                }
                else if (clickedButtonText == "+/-")
                {
                    if (this.operatorString == string.Empty)
                    {
                        this.resultNumber = -this.resultNumber;
                    }
                    else
                    {
                        this.newNumber = -this.newNumber;
                    }

                    this.sign = -this.sign;
                }
                else
                {
                    this.UpdateOperator(clickedButtonText);
                }
            }
        }

        private void UpdateDigit(int digit, string clickedButtonText)
        {
            if (this.equalFlag)
            {
                this.resultNumber = 0;
                this.equalFlag = false;
            }

            if (clickedButtonText.Length != 1)
            {
                throw new ArgumentException("Unknown button");
            }

            Func<decimal, decimal> transform;

            if (this.dotPosition == 0)
            {
                transform = (decimal number) => (10 * number) + (this.sign * digit);
            }
            else
            {
                this.dotPosition++;
                transform = (decimal number) => number + (this.sign * digit * (decimal)Math.Pow(10, 1 - this.dotPosition));
            }

            if (this.operatorString == string.Empty)
            {
                this.resultNumber = transform(this.resultNumber);
            }
            else
            {
                this.newNumber = transform(this.newNumber);
            }
        }

        private void UpdateOperator(string clickedButtonText)
        {
            if (this.operatorString != string.Empty)
            {
                switch (this.operatorString)
                {
                    case "+":
                        this.resultNumber += this.newNumber;
                        break;
                    case "-":
                        this.resultNumber -= this.newNumber;
                        break;
                    case "*":
                        this.resultNumber *= this.newNumber;
                        break;
                    case "/":
                        this.resultNumber /= this.newNumber;
                        break;
                }
            }

            this.newNumber = 0;
            this.dotPosition = 0;
            this.operatorString = clickedButtonText;
            this.sign = 1;

            if (this.operatorString == "=")
            {
                this.operatorString = string.Empty;
                this.equalFlag = true;
            }
            else if (this.operatorString != "+" &&
                     this.operatorString != "-" &&
                     this.operatorString != "*" &&
                     this.operatorString != "/")
            {
                throw new ArgumentException("Unknown button");
            }
        }
    }
}
