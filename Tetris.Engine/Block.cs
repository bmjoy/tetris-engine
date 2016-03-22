namespace Tetris.Engine
{
    using System;
    using System.Linq;

    public class Block
    {
        private BlockType blockType;
        private int rotationIndex;

        public Block(Position position) : this(GetRandomBlockType(), position)
        {
        }

        public Block(BlockType type, Position position)
        {
            this.rotationIndex = 0;
            this.Falling = false;
            this.Position = position;
            this.blockType = type;
            this.BlockMatrix = this.blockType.Rotation(this.rotationIndex);
            this.BlockMatrixSize = this.blockType.BlockDimension();
            this.BlockRotations = this.blockType.BlockRotations();
        }

        public bool[][] BlockMatrix { get; private set; }
        public Position Position { get; private set; }
        public bool Falling { get; private set; }

        public virtual bool Placed { get; set; }
        public int BlockMatrixSize { get; private set; }
        public int BlockRotations { get; private set; }

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
                case Engine.Move.RotateRight:
                    {
                        this.BlockMatrix = this.blockType.Rotation(++this.rotationIndex);
                        break;
                    }
                case Engine.Move.RotateLeft:
                    {
                        this.BlockMatrix = this.blockType.Rotation(--this.rotationIndex);
                        break;
                    }
                case Engine.Move.None:
                    {
                        break;
                    }

                default: throw new NotImplementedException(move.ToString("G"));
            }
        }

        public Block Clone()
        {
            return new Block(this.blockType, new Position { Column = this.Position.Column, Row = this.Position.Row })
                {
                    BlockMatrix = this.BlockMatrix,
                    Falling = this.Falling,
                    rotationIndex = this.rotationIndex,
                    BlockRotations = this.BlockRotations,
                    BlockMatrixSize = this.BlockMatrixSize,
                };
        }

        internal void Merge(Block block)
        {
            this.BlockMatrix = block.BlockMatrix;
            this.BlockMatrixSize = block.BlockMatrixSize;
            this.BlockRotations = block.BlockRotations;
            this.Falling = block.Falling;
            this.Position = new Position { Column = block.Position.Column, Row = block.Position.Row };
            this.blockType = block.blockType;
        }

        internal static BlockType GetRandomBlockType()
        {
            var blockTypes = Enum.GetValues(typeof(BlockType)).Cast<BlockType>().ToArray();

            return blockTypes[new Random().Next(0, blockTypes.Length)];
        }
    }
}
