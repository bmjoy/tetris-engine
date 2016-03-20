namespace Tetris.Engine.AI
{
    using System;
    using System.Collections.Generic;

    public class Move
    {
        private Tetris.Engine.Move[] moves;

        internal int Rotation { get; set; }

        internal int GameboardWidth { get; set; }

        internal int ColumnOffSet { get; set; }

        public int Fitness { get; set; }

        public bool IsValid { get; set; }

        public Tetris.Engine.Move[] Moves
        {
            get
            {
                if (this.moves == null)
                {
                    var list = new List<Tetris.Engine.Move>();
                    list.Add(Tetris.Engine.Move.None);
                    list.Add(Tetris.Engine.Move.None);
                    list.Add(Tetris.Engine.Move.None);

                    for (var i = 0; i < this.Rotation; i++)
                    {
                        list.Add(Tetris.Engine.Move.RotateRight);
                    }

                    var dir = this.ColumnOffSet < 0 ? Tetris.Engine.Move.Left : Tetris.Engine.Move.Right;
                    for (int i = 0; i < Math.Abs(this.ColumnOffSet); i++)
                    {
                        list.Add(dir);
                    }

                    this.moves = list.ToArray();
                }

                return this.moves;
            }
        }
    }
}
