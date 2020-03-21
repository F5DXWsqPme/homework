using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// Global namespace.
/// </summary>
namespace HW2_6_2
{
    /// <summary>
    /// Class with map implementation.
    /// </summary>
    public class Map
    {
        private readonly bool[,] walls;

        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="fileName">Name of file for load.</param>
        /// <exception cref="ArgumentException">Throws when file not exist or file wrong.</exception>
        public Map(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new ArgumentException("Wrong file name.");
            }

            using StreamReader reader = File.OpenText(fileName);

            string[] lines = reader.ReadToEnd().Split('\n');

            if (lines.Length < 3)
            {
                throw new ArgumentException("Wrong file format.");
            }

            if (int.TryParse(lines[0], out int width) && int.TryParse(lines[1], out int heigth))
            {
                if (heigth + 2 != lines.Length)
                {
                    throw new ArgumentException("Wrong file format.");
                }

                this.walls = new bool[heigth, width];

                for (int i = 2; i < lines.Length; i++)
                {
                    lines[i] = lines[i].Trim('\r', '\t');

                    if (width != lines[i].Length)
                    {
                        throw new ArgumentException("Wrong file format.");
                    }

                    for (int j = 0; j < lines[i].Length; j++)
                    {
                        this.walls[i - 2, j] = lines[i][j] switch
                        {
                            'W' => true,
                            ' ' => false,
                            _ => throw new ArgumentException("Wrong file format."),
                        };
                    }
                }
            }
            else
            {
                throw new ArgumentException("Wrong file format.");
            }

            if (this.IsWall(0, 0))
            {
                throw new ArgumentException("Wrong file format.");
            }
        }

        /// <summary>
        /// Gets wall in coordinate.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        /// <returns>true-if wall, false-if otherwise.</returns>
        public bool IsWall(int x, int y)
            => this.walls[y, x];

        /// <summary>
        /// Gets map width and heigth.
        /// </summary>
        /// <param name="width">Width.</param>
        /// <param name="heigth">Heigth.</param>
        public void GetWidthAndHegth(out int width, out int heigth)
        {
            width = this.walls.GetLength(1);
            heigth = this.walls.GetLength(0);
        }
    }
}
