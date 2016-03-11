namespace Tetris.Engine.AI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Tetris.Engine.AI.Algorithms;
    using Tetris.Engine.Extensions;

    public class Engine
    {
        private readonly IAlgorithm algorithm;

        public Engine(IAlgorithm algorithm)
        {
            this.algorithm = algorithm;
        }

        public Move GetNextMove(BoardManager manager)
        {
            if (manager.GameState.IsGameOver())
            {
                // start over
            }

            if (manager.GameState.IsPaused())
            {
                return new Move();
            }

            var moves = this.GetMoves(manager);

            foreach (var move in moves)
            {
                
                Console.WriteLine(string.Format("move fitness {0} isvalid {1}", move.Fitness, move.IsValid));
            }

            return moves.FirstOrDefault();
        }

        public IOrderedEnumerable<Move> GetMoves(BoardManager manager)
        {
            var moves = new List<Move>();
            for (var rotation = 0; rotation < 5; rotation++)
            {
                var rows = 0;
                for (var column = -1; column < manager.NumberOfColumns; column++)
                {
                    var tempManager = new BoardManager(manager.GameBoard.DeepClone(), manager.GameState, manager.ActiveBlock);
                    var tempBlock = tempManager.ActiveBlock.Clone();

                    for (var i = 1; i <= rotation; i++)
                    {
                        tempBlock.Move(Tetris.Engine.Move.Rotate);
                    }

                    tempBlock.Position.Column = column;

                    if (tempManager.CheckBlock(tempBlock))
                    {
                        while (true)
                        {
                            tempManager.ActiveBlock.Merge(tempBlock);

                            tempBlock.Move(Tetris.Engine.Move.Down);
                            if (!tempManager.CheckBlock(tempBlock))
                            {
                                break;
                            }

                            rows++;
                        }

                        tempManager.Lockblock();
                        tempManager.CheckBoard();
                        var canSpawnBlock = tempManager.CanSpawnBlock();
                        Console.WriteLine("after block " + column + " rotation " + rotation);
                        Console.WriteLine(tempManager.GameBoard.MatrixToString());
                        moves.Add(
                            new Move
                                {
                                    Column = column,
                                    Rows = rows,
                                    Fitness = canSpawnBlock ? this.algorithm.CalculateFitness(tempManager.GameBoard) : int.MaxValue,
                                    IsValid = true,
                                    Rotation = rotation
                                });
                    }
                }
            }

            return moves.OrderByDescending(x => x.IsValid).ThenBy(x => x.Fitness);
        }
    }
}
