﻿namespace Tetris.Engine.AI
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
            if (manager.ActiveBlock == null)
            {
                return new Move();
            }

            var moves = this.GetMoves(manager);

            return moves.FirstOrDefault() ?? new Move();
        }

        public IOrderedEnumerable<Move> GetMoves(BoardManager manager)
        {
            var moves = new List<Move>();
            for (var rotation = 0; rotation < manager.ActiveBlock.BlockRotations; rotation++)
            {
                for (var column = -1; column < manager.NumberOfColumns; column++)
                {
                    var tempManager = new BoardManager(manager.GameBoard.DeepClone(), manager.ActiveBlock.Clone());
                    var tempBlock = tempManager.ActiveBlock.Clone();
                    tempBlock.Move(Tetris.Engine.Move.Down);
                    tempBlock.Move(Tetris.Engine.Move.Down);
                    tempBlock.Move(Tetris.Engine.Move.Down);

                    if (rotation != 0)
                    {
                        for (var i = 0; i < rotation; i++)
                        {
                            tempBlock.Move(Tetris.Engine.Move.RotateRight);
                        }
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
                        }

                        tempManager.Lockblock();
                        tempManager.CheckBoard();
                        var canSpawnBlock = tempManager.CanSpawnBlock();
                        moves.Add(
                            new Move
                                {
                                    GameboardWidth = manager.NumberOfColumns,
                                    ColumnOffSet = column - manager.ActiveBlock.Position.Column,
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
