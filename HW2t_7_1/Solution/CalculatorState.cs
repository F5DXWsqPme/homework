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
        private string operatorString;
        private bool errorFlag;
        private int displaySize;
        private int dotPosition;

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculatorState"/> class.
        /// </summary>
        /// <param name="displaySize">Output string length.</param>
        public CalculatorState(int displaySize)
        {
            this.resultNumber = 0;
            this.newNumber = 0;
            this.errorFlag = false;
            this.displaySize = displaySize;
            this.operatorString = string.Empty;
            this.dotPosition = 0;
        }

        /// <summary>
        /// Gets string for display.
        /// </summary>
        /// <returns>String for display.</returns>
        public string GetOutputString()
        {
            if (Math.Abs(this.resultNumber) >= (decimal)Math.Pow(10, this.displaySize - 3))
            {
                this.errorFlag = true;
            }

            if (this.errorFlag)
            {
                string errorString = "ERROR";
                return errorString.Substring(0, Math.Min(this.displaySize, errorString.Length));
            }

            string format = "G"/* + this.displaySize.ToString("G")*/;
            string valueString;

            if (this.operatorString == string.Empty)
            {
                valueString = this.resultNumber.ToString(format);
            }
            else
            {
                valueString = this.newNumber.ToString(format);
            }

            if (!valueString.StartsWith("-"))
            {
                valueString = "+" + valueString;
            }

            if (this.dotPosition == 1)
            {
                valueString += ".";
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
                if (digit > 9 || digit < 0)
                {
                    throw new ArgumentException("Unknown button");
                }

                Func<decimal, decimal> transform;

                if (this.dotPosition == 0)
                {
                    transform = (decimal number) => (10 * number) + digit;
                }
                else
                {
                    this.dotPosition++;
                    transform = (decimal number) => number + (digit * (decimal)Math.Pow(10, 1 - this.dotPosition));
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
            else
            {
                if (clickedButtonText == "C")
                {
                    this.resultNumber = 0;
                    this.newNumber = 0;
                    this.dotPosition = 0;
                    this.errorFlag = false;
                    this.operatorString = string.Empty;
                }
                else if (clickedButtonText == ".")
                {
                    if (this.dotPosition != 0)
                    {
                        this.errorFlag = true;
                    }

                    this.dotPosition = 1;
                }
                else
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

                    if (this.operatorString == "=")
                    {
                        this.operatorString = string.Empty;
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
    }
}
