namespace Tetris.Engine
{
    using System.Linq;

    using Tetris.Engine.GameStates;
    using Tetris.Engine.GameStates.Interfaces;

    public class BoardManager
    {
        private readonly int rows;
        private readonly int columns;

        public virtual IGameState GameState { get; private set; }
        public virtual Block ActiveBlock { get; private set; }
        public virtual bool[][] GameBoard { get; private set; }

        public BoardManager(bool[][] gameBoard)
        {
            this.GameBoard = gameBoard;
            this.GameState = new Paused();
            this.rows = gameBoard.GetLength(0);
            this.columns = gameBoard[0].Length;
        }

        public bool CanSpawnBlock()
        {
            if (this.ActiveBlock != null)
            {
                return false;
            }
            var leftSpawnArea = (this.columns - 4) / 2;

            for (var row = this.rows - 1; row >= this.rows -4; row--)
            {
                if (this.GameBoard[row].Skip(leftSpawnArea).Take(4).Any(x => x))
                {
                    return false;
                }
            }

            return true;
        }

        public Block SpawnBlock()
        {
            if (!this.CanSpawnBlock())
            {
                this.GameState = new GameOver();
            }

            this.ActiveBlock = new Block();

            return this.ActiveBlock;
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
            return this.GameBoard;
        }

        public bool Move(Move move)
        {
            var tempMove = this.ActiveBlock.Clone();
            tempMove.Move(move);

            if (!this.CheckBlock(tempMove))
            {
                return false;
            }

            this.ActiveBlock = tempMove;

            return true;
        }

        public bool IsValidMove(Move move)
        {
            var tempMove = this.ActiveBlock.Clone();
            tempMove.Move(move);

            return !this.CheckBlock(tempMove);
        }

        internal bool CheckBlock(Block block)
        {
            for (var row = block.Position.Row; row < block.Position.Row + 4; row++)
            {
                for (var column = block.Position.Column; column < block.Position.Column + 4; column++)
                {
                    if (this.GameBoard[row][column] && block.BlockMatrix[row - block.Position.Row][column - block.Position.Column])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        internal bool IsRowFull(int row)
        {
            return this.GameBoard[row].All(x => x);
        }

        internal BoardManager CollapseRow(int rowToCollapse)
        {
            // Move rows down in array, which deletes the current row
            for (var rowIndex = rowToCollapse; rowIndex < this.rows -1; rowIndex++)
            {
                this.GameBoard[rowIndex] = this.GameBoard[rowIndex + 1];
            }

            // Make sure top line is cleared
            this.GameBoard[this.rows -1] = new bool[this.columns];

            return this;
        }
    }
}
