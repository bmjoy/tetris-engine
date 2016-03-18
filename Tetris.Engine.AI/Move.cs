namespace Tetris.Engine.AI
{
    using System.Collections.Generic;

    public class Move
    {
        private Tetris.Engine.Move[] moves;

        public Tetris.Engine.Move[] Moves
        {
            get
            {
                if (this.moves == null)
                {
                    var a = new List<Tetris.Engine.Move>();
                    for (var i = 0; i < this.Rotation; i++)
                    {
                        a.Add(Tetris.Engine.Move.RotateRight);
                    }

                    if (this.Column < 0)
                    {
                        for (var i = 0; i < this.Column; i++)
                        {
                            a.Add(Tetris.Engine.Move.Left);
                        }
                    }
                    else
                    {
                        for (var i = 0; i < this.Column; i++)
                        {
                            a.Add(Tetris.Engine.Move.Right);
                        }
                    }

                    for (var i = 0; i < this.Rows; i++)
                    {
                        a.Add(Tetris.Engine.Move.Down);
                    }

                    this.moves = a.ToArray();
                }

                return this.moves;
            }
        }
        public int Fitness { get; set; }
        public int Column { get; set; }
        public int Rotation { get; set; }
        public bool IsValid { get; set; }
        public int Rows { get; set; }
    }
}
