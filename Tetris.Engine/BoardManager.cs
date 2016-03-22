using System;

namespace Tetris.Engine
{
    using System.Linq;

    public class BoardManager
    {
        private readonly int rows;
        private readonly int columns;

        public virtual Block ActiveBlock { get; private set; }
        public virtual bool[][] GameBoard { get; private set; }
        public virtual GameStats GameStats { get; }

        public virtual int NumberOfColumns
        {
            get
            {
                return this.columns;
            }
        }

        public BoardManager(bool[][] gameBoard) : this (gameBoard, null)
        {
        }

        public BoardManager(bool[][] gameBoard, Block activeBlock)
        {
            this.GameBoard = gameBoard;
            this.rows = gameBoard.GetLength(0);
            this.ActiveBlock = activeBlock;
            this.columns = gameBoard[0].Length;
            this.GameStats = new GameStats();
        }

        public bool CanSpawnBlock()
        {
            if (this.ActiveBlock != null)
            {
                return false;
            }
            var leftSpawnArea = (this.columns - 4) / 2;

            for (var row = this.rows - 1; row >= this.rows -2; row--)
            {
                if (this.GameBoard[row].Skip(leftSpawnArea).Take(2).Any(x => x))
                {
                    return false;
                }
            }

            return true;
        }

        public Block SpawnBlock()
        {
            return this.SpawnBlock(Block.GetRandomBlockType());
        }

        public Block SpawnBlock(BlockType type)
        {
            if (!this.CanSpawnBlock())
            {
                throw new InvalidOperationException("can't spawn a new block");
            }

            this.ActiveBlock = new Block(type , new Position { Column = (this.columns - type.BlockDimension()) / 2, Row = this.rows - type.BlockDimension() + 1 });
            GameStats.NewSpawn();

            return this.ActiveBlock;
        }

        public BoardManager CheckBoard()
        {
            var clearedRows = 0;
            for (var rowIndex = 0; rowIndex < this.rows; rowIndex++)
            {
                if (this.IsRowFull(rowIndex))
                {
                    clearedRows++;
                    this.CollapseRow(rowIndex--);
                }
            }
            GameStats.NewRowClearings(clearedRows);

            return this;
        }

        public bool[][] GetBoard()
        {
            return this.GameBoard;
        }

        public bool Move(Move move)
        {
            if (move == Engine.Move.None)
            {
                return true;
            }

            if (this.ActiveBlock == null)
            {
                return false;
            }

            var tempMove = this.ActiveBlock.Clone();
            tempMove.Move(move);

            var validMove = this.CheckBlock(tempMove);
            if (!validMove && (move == Engine.Move.Down || move == Engine.Move.Fall))
            {
                Lockblock();
                CheckBoard();
                return true;
            }
            else if (!validMove)
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
            for (var row = block.Position.Row; row < block.Position.Row + block.BlockMatrixSize; row++)
            {
                for (var column = block.Position.Column; column < block.Position.Column + block.BlockMatrixSize; column++)
                {
                    if (!block.BlockMatrix[row - block.Position.Row][column - block.Position.Column])
                    {
                        continue;
                    }

                    if (row < 0)
                    {
                        return false;
                    }

                    if (row >= this.rows)
                    {
                        return false;
                    }

                    if (column < 0)
                    {
                        return false;
                    }

                    if (column >= this.columns)
                    {
                        return false;
                    }

                    if (this.GameBoard[row][column])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        internal void Lockblock()
        {
            var block = this.ActiveBlock;
            this.ActiveBlock = null;

            for (var row = block.Position.Row; row < block.Position.Row + block.BlockMatrixSize && row < this.rows; row++)
            {
                for (var column = block.Position.Column; column < block.Position.Column + block.BlockMatrixSize && column < this.columns; column++)
                {
                    if (row < 0)
                    {
                        continue;
                    }

                    if (row >= this.rows)
                    {
                        continue;
                    }

                    if (column < 0)
                    {
                        continue;
                    }

                    if (column >= this.columns)
                    {
                        continue;
                    }

                    if (this.GameBoard[row][column])
                    {
                        continue;
                    }

                    this.GameBoard[row][column] = this.GameBoard[row][column] | block.BlockMatrix[row - block.Position.Row][column - block.Position.Column];
                }
            }
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
