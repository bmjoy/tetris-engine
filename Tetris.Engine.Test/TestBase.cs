namespace Tetris.Engine.Test
{
    using System;
    using System.Linq;
    using System.Text;

    using NUnit.Framework;

    using Tetris.Engine.Extensions;

    public abstract class TestBase
    {
        protected void AssertBoard(bool[][] board, bool[][] exspected)
        {
            this.PrintBoardDifferences(board, exspected);

            Assert.AreEqual(board.GetLength(0), exspected.GetLength(0), "rows");
            Assert.AreEqual(board[0].Length, exspected[0].Length, "columns");

            for (var row = 0; row < board.GetLength(0); row++)
            {
                for (var column = 0; column < board[0].Length; column++)
                {
                    Assert.That(board[row][column], Is.EqualTo(exspected[row][column]));
                }
            }
        }

        protected bool[][] ReverseRows(bool[][] gameBoard)
        {
            var clone = gameBoard.DeepClone();
            return clone.Reverse().ToArray();
        }

        protected void PrintBoardDifferences(bool[][] gameBoard, bool[][] expectedBoard)
        {
            Console.WriteLine(this.ReverseRows(gameBoard).MatrixToString());

            Console.WriteLine();

            Console.WriteLine(this.ReverseRows(expectedBoard).MatrixToString());
        }
    }
}
