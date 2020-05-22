using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Solution
{
    /// <summary>
    /// Class with implementation calculator state.
    /// </summary>
    public class GameState
    {
        private int firstButton;
        private int secondButton;
        private int fieldSize;
        private Cell[] field;
        private int guessed;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameState"/> class.
        /// </summary>
        /// <param name="fieldSize">Fild width and height.</param>
        /// <exception cref="ArgumentException">Throws when field size wrong.</exception>
        public GameState(int fieldSize)
        {
            if (fieldSize <= 0 || fieldSize % 2 != 0)
            {
                throw new ArgumentException("Wrong field size");
            }

            this.guessed = 0;
            this.fieldSize = fieldSize;
            this.firstButton = -1;
            this.secondButton = -1;
            this.field = new Cell[fieldSize * fieldSize];

            for (int i = 0; i < this.field.Length; i++)
            {
                this.field[i] = new Cell();
            }

            int[] permutation = new int[fieldSize * fieldSize];

            for (int i = 0; i < permutation.Length / 2; i++)
            {
                this.field[i].Value = i;
            }

            for (int i = permutation.Length / 2; i < permutation.Length; i++)
            {
                this.field[i].Value = i - (permutation.Length / 2);
            }

            var random = new System.Random();
            for (int i = permutation.Length - 1; i >= 0; i--)
            {
                int j = random.Next(i + 1);

                Cell cellJ = this.field[j];
                this.field[j] = this.field[i];
                this.field[i] = cellJ;
            }
        }

        /// <summary>
        /// Cell in field state.
        /// </summary>
        public enum CellState
        {
            /// <summary>
            /// Active cell (start value).
            /// </summary>
            Active = 0,

            /// <summary>
            /// Pressed cell.
            /// </summary>
            Pressed,

            /// <summary>
            /// Guessed cell.
            /// </summary>
            Guessed,
        }

        /// <summary>
        /// Update game state function.
        /// </summary>
        /// <param name="clickedButtonIndex">Clicked button index.</param>
        /// <param name="IsWin">Win flag.</param>
        /// <returns>Changed cells with indexes.</returns>
        public (Cell changedCell, int cellIndex)[] Update(int clickedButtonIndex, out bool isWin)
        {
            isWin = false;

            if (this.field[clickedButtonIndex].State == CellState.Guessed)
            {
                if (this.secondButton != -1)
                {
                    return new (Cell changedCell, int cellIndex)[2]
                    {
                        (this.field[this.firstButton], this.firstButton),
                        (this.field[this.secondButton], this.secondButton),
                    };
                }
                else
                {
                    return new (Cell changedCell, int cellIndex)[0];
                }
            }
            else if (this.firstButton == -1)
            {
                this.firstButton = clickedButtonIndex;
                this.field[clickedButtonIndex].State = CellState.Pressed;

                return new (Cell changedCell, int cellIndex)[1]
                    {
                        (this.field[clickedButtonIndex], clickedButtonIndex),
                    };
            }
            else if (this.secondButton == -1)
            {
                if (this.firstButton != clickedButtonIndex)
                {
                    this.secondButton = clickedButtonIndex;
                    this.field[clickedButtonIndex].State = CellState.Pressed;
                }

                return new (Cell changedCell, int cellIndex)[1]
                    {
                        (this.field[clickedButtonIndex], clickedButtonIndex),
                    };
            }
            else
            {
                if (this.field[this.firstButton].Value == this.field[this.secondButton].Value)
                {
                    this.guessed += 2;
                    if (this.guessed >= this.fieldSize * this.fieldSize)
                    {
                        isWin = true;
                    }

                    this.field[this.firstButton].State = CellState.Guessed;
                    this.field[this.secondButton].State = CellState.Guessed;
                }
                else
                {
                    this.field[this.firstButton].State = CellState.Active;
                    this.field[this.secondButton].State = CellState.Active;
                }

                var result = new (Cell changedCell, int cellIndex)[3]
                    {
                        (this.field[clickedButtonIndex], clickedButtonIndex),
                        (this.field[this.firstButton], this.firstButton),
                        (this.field[this.secondButton], this.secondButton),
                    };

                this.secondButton = -1;
                this.firstButton = -1;

                if (this.field[clickedButtonIndex].State != CellState.Guessed)
                {
                    this.firstButton = clickedButtonIndex;
                    this.field[clickedButtonIndex].State = CellState.Pressed;
                }

                return result;
            }
        }

        /// <summary>
        /// Cell in field structure.
        /// </summary>
        public class Cell
        {
            /// <summary>
            /// Cell state.
            /// </summary>
            public CellState State
            {
                get;
                set;
            }

            /// <summary>
            /// Cell number value.
            /// </summary>
            public int Value
            {
                get;
                set;
            }
        }
    }
}
