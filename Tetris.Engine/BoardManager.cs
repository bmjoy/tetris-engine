namespace Tetris.Engine
{
    using System.Linq;

    public class BoardManager
    {
        private readonly bool[][] gameBoard;
        private readonly int rows;
        private readonly int columns;

        public BoardManager(bool[][] gameBoard)
        {
            this.gameBoard = gameBoard;
            this.rows = gameBoard.GetLength(0);
            this.columns = gameBoard[0].Length;
        }

        public BoardManager CheckBoard()
        {
            for (var rowIndex = 0; rowIndex < this.rows; rowIndex++)
            {
                if (this.IsRowFull(rowIndex))
                {
                    this.CollapseRow(rowIndex--);
                }
            }

            return this;
        }

        public bool[][] GetBoard()
        {
            return this.gameBoard;
        }

        public bool IsValidMove()
        {
            return true;
        }

        internal bool IsRowFull(int row)
        {
            return this.gameBoard[row].All(x => x);
        }

        internal BoardManager CollapseRow(int rowToCollapse)
        {
            // Move rows down in array, which deletes the current row
            for (var rowIndex = rowToCollapse; rowIndex < this.rows -1; rowIndex++)
            {
                this.gameBoard[rowIndex] = this.gameBoard[rowIndex + 1];
            }

            // Make sure top line is cleared
            this.gameBoard[this.rows -1] = new bool[this.columns];

            return this;
        }
    }
}
