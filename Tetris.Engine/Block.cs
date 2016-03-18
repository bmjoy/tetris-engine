namespace Tetris.Engine
{
    using System;
    using System.Linq;

    public class Block
    {
        private BlockType blockType;
        private int rotationIndex;

        public Block(Position position)
        {
            this.rotationIndex = 0;
            this.Falling = false;
            this.Position = position;
            this.blockType = this.GetRandomBlockType();
            this.BlockMatrix = this.blockType.GetRotations(this.rotationIndex);
            this.BlockMatrixSize = BlockMatrix.GetLength(0);
        }

        public Block(BlockType type, Position position)
        {
            this.rotationIndex = 0;
            this.Falling = false;
            this.Position = position;
            this.blockType = type;
            this.BlockMatrix = this.blockType.GetRotations(this.rotationIndex);
            this.BlockMatrixSize = BlockMatrix.GetLength(0);
        }

        public bool[][] BlockMatrix { get; private set; }
        public Position Position { get; private set; }
        public bool Falling { get; private set; }

        public virtual bool Placed { get; set; }
        public int BlockMatrixSize { get; private set; }

        public void Move(Move move)
        {
            switch (move)
            {
                case Engine.Move.Left:
                    {
                        this.Position.Column--;
                        break;
                    }
                case Engine.Move.Right:
                    {
                        this.Position.Column++;
                        break;
                    }
                case Engine.Move.Fall:
                    {
                        this.Falling = true;
                        break;
                    }
                case Engine.Move.Down:
                    {
                        this.Position.Row--;
                        break;
                    }
                case Engine.Move.Rotate:
                    {
                        this.BlockMatrix = this.blockType.GetRotations(this.rotationIndex++);
                        break;
                    }

                default: throw new NotImplementedException(move.ToString("G"));
            }
        }

        internal BlockType GetRandomBlockType()
        {
            var blockTypes = Enum.GetValues(typeof(BlockType)).Cast<BlockType>().ToArray();

            return blockTypes[new Random(1).Next(0, blockTypes.Length)];
        }

        internal Block Clone()
        {
            return new Block(this.blockType, this.Position)
                {
                    BlockMatrix = this.BlockMatrix,
                    Falling = true,
                };
        }

        internal void Merge(Block block)
        {
            this.BlockMatrix = block.BlockMatrix;
            this.BlockMatrixSize = block.BlockMatrixSize;
            this.Falling = block.Falling;
            this.Position = new Position { Column = block.Position.Column, Row = block.Position.Row };
            this.blockType = block.blockType;
        }
    }
}
