using System;
using Tetris.Engine.Extensions;
using Tetris.Engine.GameStates;
using Tetris.Engine.GameStates.Interfaces;

namespace Tetris.Engine
{
    public class GameManager
    {
        private readonly BoardManager boardManager;

        public virtual IGameState GameState { get; private set; }

        public GameManager(int height, int width)
        {
            var gameBoard = new bool[height][];
            for (var i = 0; i < height; i++)
            {
                gameBoard[i] = new bool[width];
            }

            this.boardManager = new BoardManager(gameBoard);
            this.GameState = new Playing();
        }

        public BoardManager BoardManager => this.boardManager;
        public GameStats GameStats => this.boardManager.GameStats;
        public Block ActiveBlock => this.boardManager.ActiveBlock;

        public void OnGameLoopStep()
        {
            if (this.GameState.IsPaused())
            {
                return;
            }

            if (this.GameState.IsGameOver())
            {
                return;
            }

            if (this.boardManager.ActiveBlock == null)
            {
                try
                {
                    boardManager.SpawnBlock();
                }
                catch (Exception)
                {
                    this.GameState = new GameOver();
                }
                return;
            }

            this.MoveBlock(Move.Down);
        }

        public bool MoveBlock(Move move)
        {
            return this.boardManager.Move(move);
        }
    }
}
