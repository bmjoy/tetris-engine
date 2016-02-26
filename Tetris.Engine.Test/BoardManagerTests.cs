// ReSharper disable InconsistentNaming
namespace Tetris.Engine.Test
{
    using System;
    using System.Text;

    using NUnit.Framework;

    [TestFixture]
    public class BoardManagerTests
    {
        [Test]
        [TestCase(0, false)]
        [TestCase(1, false)]
        [TestCase(2, true)]
        public void IsRowFull(int row, bool expectedResult)
        {
            var board = new []
            {
                new[] { false, false, false },
                new[] { true, false, true },
                new[] { true, true, true }
            };
            var gameManager = new BoardManager(board);
            Console.WriteLine(gameManager.IsRowFull(row));
            Assert.That(gameManager.IsRowFull(row), Is.EqualTo(expectedResult));
        }

        [Test]
        public void CollapseRow_1_x_1()
        {
            var board = this.ReverseRows(new[] {
                new[] { true }
            });
            var exspected = this.ReverseRows(new[] {
                new[] { false }
            });

            var gameManager = new BoardManager(board);
            board = gameManager.CollapseRow(0).GetBoard();

            this.AssertBoard(board, exspected);
        }

        [Test]
        public void CollapseRow_1_x_2_top()
        {
            var board = this.ReverseRows(new[] {
                new[] { true },
                new[] { true }
            });
            var exspected = this.ReverseRows(new[] {
                new[] { false },
                new[] { true }
            });

            var gameManager = new BoardManager(board);
            board = gameManager.CollapseRow(1).GetBoard();

            this.AssertBoard(board, exspected);
        }

        [Test]
        public void CollapseRow_1_x_2_buttom()
        {
            var board = this.ReverseRows(new[] {
                new[] { true },
                new[] { true }
            });
            var exspected = this.ReverseRows(new[] {
                new[] { false },
                new[] { true }
            });

            var gameManager = new BoardManager(board);
            board = gameManager.CollapseRow(0).GetBoard();

            this.AssertBoard(board, exspected);
        }

        [Test]
        public void CollapseRow_2_x_1()
        {
            var board = this.ReverseRows(new[] {
                new[] { true, true }
            });
            var exspected = this.ReverseRows(new[] {
                new[] { false, false }
            });

            var gameManager = new BoardManager(board);
            board = gameManager.CollapseRow(0).GetBoard();

            this.AssertBoard(board, exspected);
        }

        [Test]
        public void CollapseRow_2_x_2()
        {
            var board = this.ReverseRows(new[] {
                new[] { false, true },
                new[] { false, true }
            });
            var exspected = this.ReverseRows(new[] {
                new[] { false, false },
                new[] { false, true }
            });

            var gameManager = new BoardManager(board);
            board = gameManager.CollapseRow(1).GetBoard();

            this.AssertBoard(board, exspected);
        }

        [Test]
        public void CollapseRow_4_x_2()
        {
            var board = this.ReverseRows(new[] {
                new[] { true, false },
                new[] { false, true },
                new[] { true, true },
                new[] { false, true }
            });
            var exspected = this.ReverseRows(new[] {
                new[] { false, false },
                new[] { true, false },
                new[] { false, true },
                new[] { false, true }
            });

            var gameManager = new BoardManager(board);
            board = gameManager.CollapseRow(1).GetBoard();

            this.AssertBoard(board, exspected);
        }

        [Test]
        public void CollapseRow_3_x_3()
        {
            var board = this.ReverseRows(new []
            {
                new[] { false, true, false },
                new[] { true, true, true },
                new[] { true, false, true }
            });
            var exspected = this.ReverseRows(new[] {
                new[] { false, false, false },
                new[] { false, true, false },
                new[] { true, false, true }
            });

            var gameManager = new BoardManager(board);
            board = gameManager.CollapseRow(1).GetBoard();

            this.AssertBoard(board, exspected);
        }

        [Test]
        public void CheckBoard_collapse_none_rows()
        {
            var board = this.ReverseRows(new[]
            {
                new[] { false, true, false },
                new[] { true, false, true },
                new[] { true, false, false }
            });
            var exspected = this.ReverseRows(new[] {
                new[] { false, true, false },
                new[] { true, false, true },
                new[] { true, false, false }
            });

            var gameManager = new BoardManager(board);
            board = gameManager.CheckBoard().GetBoard();

            this.AssertBoard(board, exspected);
        }

        [Test]
        public void CheckBoard_collapse_one_row()
        {
            var board = this.ReverseRows(new[]
            {
                new[] { false, true, false },
                new[] { true, true, true },
                new[] { true, false, true }
            });
            var exspected = this.ReverseRows(new[] {
                new[] { false, false, false },
                new[] { false, true, false },
                new[] { true, false, true }
            });

            var gameManager = new BoardManager(board);
            board = gameManager.CheckBoard().GetBoard();

            this.AssertBoard(board, exspected);
        }

        [Test]
        public void CheckBoard_collapse_two_rows()
        {
            var board = this.ReverseRows(new[]
            {
                new[] { false, true, false },
                new[] { true, true, true },
                new[] { true, true, true }
            });
            var exspected = this.ReverseRows(new[] {
                new[] { false, false, false },
                new[] { false, false, false },
                new[] { false, true, false }
            });

            var gameManager = new BoardManager(board);
            board = gameManager.CheckBoard().GetBoard();

            this.AssertBoard(board, exspected);
        }

        private void AssertBoard(bool[][] board, bool[][] exspected)
        {
            this.PrintBoardDifferences(board, exspected);

            Assert.AreEqual(board.GetLength(0), exspected.GetLength(0), "rows");
            Assert.AreEqual(board[0].Length, exspected[0].Length, "columns");

            for (int row = 0; row < board.GetLength(0); row++)
            {
                for (int column = 0; column < board[0].Length; column++)
                {
                    Assert.That(board[row][column], Is.EqualTo(exspected[row][column]));
                }
            }
        }

        protected bool[][] ReverseRows(bool[][] gameBoard)
        {
            var a = (bool[][])gameBoard.Clone();
            for (var i = 0; i < gameBoard.GetLength(0); i++)
            {
                a[gameBoard.GetLength(0) - i - 1] = gameBoard[i];
            }
            return a;
        }

        protected void PrintBoardDifferences(bool[][] gameBoard, bool[][] expectedBoard)
        {
            PrintField(this.ReverseRows(gameBoard));

            Console.WriteLine();

            PrintField(this.ReverseRows(expectedBoard));
        }

        private static void PrintField(bool[][] gameBoard)
        {
            var fieldChars = new StringBuilder();
            for (int row = 0; row < gameBoard.GetLength(0); row++)
            {
                for (var column = 0; column < gameBoard[row].Length; column++)
                {
                    fieldChars.Append(gameBoard[row][column] ? "1" : "0");
                }

                fieldChars.AppendLine();
            }

            Console.WriteLine(fieldChars);
        }
    }
}
