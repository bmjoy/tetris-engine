namespace Tetris.Engine
{
    using System;
    using System.Linq;

    public class Block
    {
        private BlockType blockType;
        private int rotationIndex;

        public Block()
        {
            this.rotationIndex = 0;
            this.Falling = false;
            this.Position = new Position();
            this.blockType = this.GetRandomBlockType();
        }

        public bool[][] BlockMatrix { get; private set; }
        public Position Position { get; private set; }
        public bool Falling { get; private set; }

        public bool Placed { get; set; }

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
                case Engine.Move.Rotate:
                    {
                        this.BlockMatrix = this.blockType.GetRotations(this.rotationIndex++);
                        break;
                    }
            }
        }

        internal BlockType GetRandomBlockType()
        {
            var blockTypes = Enum.GetValues(typeof(BlockType)).Cast<BlockType>().ToArray();

            return blockTypes[new Random(1).Next(0, blockTypes.Length)];
        }

        internal Block Clone()
        {
            return new Block
                {
                    BlockMatrix = this.BlockMatrix,
                    Falling = true,
                    Position = this.Position,
                    blockType = this.blockType
                };
        }

        internal void Merge(Block block)
        {
            this.BlockMatrix = block.BlockMatrix;
            this.Falling = block.Falling;
            this.Position = block.Position;
            this.blockType = block.blockType;
        }
    }
}
