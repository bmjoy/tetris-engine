using System;

namespace Tetris.Engine
{
    public class GameStats
    {
        public int OneRowClearings { get; private set; }
        public int TwoRowsClearings { get; private set; }
        public int ThreeRowsClearings { get; private set; }
        public int FourRowsClearings { get; private set; }
        public int BlocksSpawned { get; private set; }

        public GameStats()
        {
            OneRowClearings = 0;
            TwoRowsClearings = 0;
            ThreeRowsClearings = 0;
            FourRowsClearings = 0;
            BlocksSpawned = 0;
        }

        public void NewSpawn()
        {
            BlocksSpawned++;
        }

        public void NewRowClearings(int clearedRows)
        {
            switch (clearedRows)
            {
                case 0: break;
                case 1: OneRowClearings++; break;
                case 2: TwoRowsClearings++; break;
                case 3: ThreeRowsClearings++; break;
                case 4: FourRowsClearings++; break;

                default: throw new ArgumentOutOfRangeException(nameof(clearedRows));
            }
        }
    }
}
