using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Solution
{
    /// <summary>
    /// Main window class.
    /// </summary>
    public partial class MainWindow : Form
    {
        private GameState state;
        private GameButton[,] buttons;
        private int fieldSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow(int fieldSize)
        {
            this.fieldSize = fieldSize;

            try
            {
                this.state = new GameState(fieldSize);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Wrong arguments", "Error");
                Environment.Exit(0);
            }

            this.InitializeComponent();

            this.buttons = new GameButton[fieldSize, fieldSize];

            var size = this.ClientSize;

            double offsetPart = 0.1;
            double offsetX = size.Width * offsetPart / (fieldSize + 1);
            double offsetY = size.Height * offsetPart / (fieldSize + 1);

            double buttonsPart = 1 - offsetPart;
            int sizeX = (int)(size.Width * buttonsPart / fieldSize);
            int sizeY = (int)(size.Height * buttonsPart / fieldSize);

            for (int row = 0; row < fieldSize; row++)
            {
                for (int column = 0; column < fieldSize; column++)
                {
                    this.buttons[row, column] = new GameButton((row * fieldSize) + column);

                    this.buttons[row, column].Location =
                        new System.Drawing.Point(
                            (int)((offsetX * (column + 1)) + (sizeX * column)),
                            (int)((offsetY * (row + 1)) + (sizeY * row)));
                    this.buttons[row, column].Name = "button";
                    this.buttons[row, column].Size = new System.Drawing.Size(sizeX, sizeY);
                    this.buttons[row, column].TabIndex = (row * fieldSize) + column;
                    this.buttons[row, column].Text = "?";
                    this.buttons[row, column].UseVisualStyleBackColor = true;
                    this.buttons[row, column].BackColor = Color.Gray;
                    this.buttons[row, column].Click += new System.EventHandler(this.ButtonClicked);

                    this.Controls.Add(this.buttons[row, column]);
                }
            }
        }

        /// <summary>
        /// Button click callback.
        /// </summary>
        /// <param name="sender">Sender object (NoFocusButton).</param>
        /// <param name="arguments">Callback arguments.</param>
        private void ButtonClicked(object sender, EventArgs arguments)
        {
            GameButton button = sender as GameButton;

            var allChanges = this.state.Update(button.Index, out bool isWin);

            foreach (var change in allChanges)
            {
                this.buttons[change.cellIndex / this.fieldSize, change.cellIndex % this.fieldSize].Text =
                    change.changedCell.State switch
                    {
                        GameState.CellState.Active => "?",
                        _ => change.changedCell.Value.ToString(),
                    };

                this.buttons[change.cellIndex / this.fieldSize, change.cellIndex % this.fieldSize].BackColor =
                    change.changedCell.State switch
                    {
                        GameState.CellState.Active => Color.Gray,
                        GameState.CellState.Guessed => Color.Green,
                        GameState.CellState.Pressed => Color.Yellow,
                    };
            }

            if (isWin)
            {
                MessageBox.Show("You won", "Win");
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Button without focus.
        /// </summary>
        private class GameButton : Button
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="GameButton"/> class.
            /// </summary>
            /// <param name="index">Cell index.</param>
            public GameButton(int index)
            {
                this.Index = index;
                this.SetStyle(ControlStyles.Selectable, false);
            }

            /// <summary>
            /// Cell index in game.
            /// </summary>
            public int Index
            {
                get;
                private set;
            }
        }
    }
}
