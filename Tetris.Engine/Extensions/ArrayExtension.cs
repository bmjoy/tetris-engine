namespace Tetris.Engine.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static class ArrayExtension
    {
        public static bool[][] Split(this bool[] array, int size)
        {
            if (array.Length % size != 0)
            {
                throw new ArgumentOutOfRangeException("array");
            }

            var matrix = new List<bool[]>();
            var length = array.Length / size;

            for (var i = 0; i < size; i++)
            {
                matrix.Add(array.Skip(i * length).Take(length).ToArray());
            }

            return matrix.ToArray();
        }

        public static bool[][] DeepClone(this bool[][] array)
        {
            var list = new List<bool[]>();
            for (var i = 0; i < array.GetLength(0); i++)
            {
                list.Add(array[i].Cast<bool>().ToArray());
            }

            return list.ToArray();
        }

        public static string MatrixToString(this bool[][] gameBoard)
        {
            var fieldChars = new StringBuilder();
            for (var row = 0; row < gameBoard.GetLength(0); row++)
            {
                for (var column = 0; column < gameBoard[row].Length; column++)
                {
                    fieldChars.Append(gameBoard[row][column] ? "1" : "0");
                }

                fieldChars.AppendLine();
            }

            return fieldChars.ToString();
        }
    }
}
