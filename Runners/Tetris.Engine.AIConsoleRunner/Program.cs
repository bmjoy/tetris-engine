namespace Tetris.Engine.AIConsoleRunner
{
    using System;
    using System.Collections;
    using System.Threading;

    using Tetris.Engine.AI.Algorithms;
    using Tetris.Engine.AI.Algorithms.Weights;
    using Tetris.Engine.Extensions;

    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var gameManager = new GameManager(20, 10);
                var ai = new AI.Engine(new Tsitsiklis(new TsitsiklisWeights(100, 200)));

                IEnumerator moveIterator = null;
                var blockNumber = -1;
                while (!gameManager.GameState.IsGameOver())
                {
                    PrintState(gameManager);
                    Thread.Sleep(200);

                    gameManager.OnGameLoopStep();

                    if (moveIterator != null && moveIterator.MoveNext())
                    {
                        gameManager.MoveBlock((Move) moveIterator.Current);
                        continue;
                    }

                    // Make sure we only calculate best move once per spawned block.
                    if (blockNumber != gameManager.GameStats.BlocksSpawned)
                    {
                        var steps = ai.GetNextMove(gameManager.BoardManager);
                        moveIterator = steps.Moves.GetEnumerator();
                        blockNumber = gameManager.GameStats.BlocksSpawned;
                    }
                }

                Console.WriteLine("Game Over");
                Console.WriteLine();
                Console.WriteLine();
                Thread.Sleep(500);
            }
        }

        private static string BoardState(GameManager gameManager)
        {
            var tempBoardManager = gameManager.BoardManager;
            if (gameManager.ActiveBlock != null)
            {
                tempBoardManager = new BoardManager(gameManager.BoardManager.GameBoard.DeepClone(), gameManager.BoardManager.ActiveBlock.Clone());
            }

            return tempBoardManager.GameBoard.MatrixToString(gameManager.ActiveBlock);
        }

        private static void PrintState(GameManager gameManager)
        {
            Console.Clear();
            Console.WriteLine(BoardState(gameManager));
            Console.WriteLine();
            Console.WriteLine($"1: {gameManager.GameStats.OneRowClearings}");
            Console.WriteLine("2: {0}", gameManager.GameStats.TwoRowsClearings);
            Console.WriteLine("3: {0}", gameManager.GameStats.ThreeRowsClearings);
            Console.WriteLine("4: {0}", gameManager.GameStats.FourRowsClearings);
            Console.WriteLine("Spawned: {0}", gameManager.GameStats.BlocksSpawned);
        }
    }
}
