namespace Tetris.Engine
{
    using System;

    public class GameStats
    {
        public int OneRowClearings { get; private set; }
        public int TwoRowsClearings { get; private set; }
        public int ThreeRowsClearings { get; private set; }
        public int FourRowsClearings { get; private set; }
        public int TotalRowClearings { get; private set; }
        public int BlocksSpawned { get; private set; }
        public int Fitness
        {
            get
            {
                return (this.TotalRowClearings * 4) + this.BlocksSpawned;
            }
        }

        public GameStats()
        {
            this.OneRowClearings = 0;
            this.TwoRowsClearings = 0;
            this.ThreeRowsClearings = 0;
            this.FourRowsClearings = 0;
            this.TotalRowClearings = 0;
            this.BlocksSpawned = 0;
        }

        public void NewSpawn()
        {
            this.BlocksSpawned++;
        }

        public void NewRowClearings(int clearedRows)
        {
            switch (clearedRows)
            {
                case 0: break;
                case 1:
                    this.OneRowClearings++; break;
                case 2:
                    this.TwoRowsClearings++; break;
                case 3:
                    this.ThreeRowsClearings++; break;
                case 4:
                    this.FourRowsClearings++; break;

                default: throw new ArgumentOutOfRangeException(nameof(clearedRows));
            }

            this.TotalRowClearings += clearedRows;
        }
    }
}
