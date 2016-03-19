// ReSharper disable InconsistentNaming

using Tetris.Engine.Extensions;

namespace Tetris.Engine.Test
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class BoardManagerTests : TestBase
    {
        [Test]
        [TestCase(0, false)]
        [TestCase(1, false)]
        [TestCase(2, true)]
        public void IsRowFull(int row, bool expectedResult)
        {
            var board = new []
            {
                new[] { false, false, false },
                new[] { true, false, true },
                new[] { true, true, true }
            };
            var gameManager = new BoardManager(board);
            Console.WriteLine(gameManager.IsRowFull(row));
            Assert.That(gameManager.IsRowFull(row), Is.EqualTo(expectedResult));
        }

        [Test]
        public void CanSpawnBlock_when_area_is_free()
        {
            var board = new[]
            {
                new[] { true, true, true, true, true, true },
                new[] { true, false, false, false, false, true },
                new[] { true, false, false, false, false, true },
                new[] { true, false, false, false, false, true },
                new[] { true, false, false, false, false, true }
            };

            var manager = new BoardManager(board);

            Assert.IsTrue(manager.CanSpawnBlock());
        }

        [Test]
        public void CanSpawnBlock_when_area_is_blocked_in_top_left()
        {
            var board = new[]
            {
                new[] { true, true, false, false, false, true },
                new[] { true, false, false, false, false, true },
                new[] { true, true, true, true, true, true }
            };

            var gameManager = new BoardManager(board);
            Assert.IsFalse(gameManager.CanSpawnBlock());
        }

        [Test]
        public void CanSpawnBlock_when_area_is_blocked_in_mid_left()
        {
            var board = new[]
            {
                new[] { true, false, false, false, false, true },
                new[] { true, false, true, false, false, true },
                new[] { true, true, true, true, true, true }
            };

            var gameManager = new BoardManager(board);
            Assert.IsFalse(gameManager.CanSpawnBlock());
        }

        [Test]
        public void CanSpawnBlock_when_area_is_blocked_in_low_left()
        {
            var board = new[]
            {
                new[] { true, false, false, false, false, true },
                new[] { true, true, false, false, false, true },
                new[] { true, true, true, true, true, true }
            };

            var gameManager = new BoardManager(board);
            Assert.IsFalse(gameManager.CanSpawnBlock());
        }

        [Test]
        public void Spawn_returns_goes_to_gameover_if_new_block_cannot_be_spawned()
        {
            var board = new[]
            {
                new[] { true, false, false, false, false, true },
                new[] { true, false, true, false, false, true },
                new[] { true, false, false, false, false, true },
                new[] { true, true, false, false, false, true }
            };

            var gameManager = new BoardManager(board);
            gameManager.SpawnBlock();

            Assert.IsTrue(gameManager.GameState.IsGameOver());
        }

        [Test]
        public void Spawn_set_active_block()
        {
            var board = new[]
            {
                new[] { true, false, false, false, false, true },
                new[] { true, false, false, false, false, true },
                new[] { true, false, false, false, false, true },
                new[] { true, false, false, false, false, true }
            };

            var gameManager = new BoardManager(board);

            Assert.IsNull(gameManager.ActiveBlock);

            gameManager.SpawnBlock();

            Assert.IsFalse(gameManager.GameState.IsGameOver());
            Assert.IsNotNull(gameManager.ActiveBlock);
        }

        [Test]
        public void CollapseRow_1_x_1()
        {
            var board = this.ReverseRows(new[] {
                new[] { true }
            });
            var exspected = this.ReverseRows(new[] {
                new[] { false }
            });

            var gameManager = new BoardManager(board);
            board = gameManager.CollapseRow(0).GetBoard();

            this.AssertBoard(board, exspected);
        }

        [Test]
        public void CollapseRow_1_x_2_top()
        {
            var board = this.ReverseRows(new[] {
                new[] { true },
                new[] { true }
            });
            var exspected = this.ReverseRows(new[] {
                new[] { false },
                new[] { true }
            });

            var gameManager = new BoardManager(board);
            board = gameManager.CollapseRow(1).GetBoard();

            this.AssertBoard(board, exspected);
        }

        [Test]
        public void CollapseRow_1_x_2_buttom()
        {
            var board = this.ReverseRows(new[] {
                new[] { true },
                new[] { true }
            });
            var exspected = this.ReverseRows(new[] {
                new[] { false },
                new[] { true }
            });

            var gameManager = new BoardManager(board);
            board = gameManager.CollapseRow(0).GetBoard();

            this.AssertBoard(board, exspected);
        }

        [Test]
        public void CollapseRow_2_x_1()
        {
            var board = this.ReverseRows(new[] {
                new[] { true, true }
            });
            var exspected = this.ReverseRows(new[] {
                new[] { false, false }
            });

            var gameManager = new BoardManager(board);
            board = gameManager.CollapseRow(0).GetBoard();

            this.AssertBoard(board, exspected);
        }

        [Test]
        public void CollapseRow_2_x_2()
        {
            var board = this.ReverseRows(new[] {
                new[] { false, true },
                new[] { false, true }
            });
            var exspected = this.ReverseRows(new[] {
                new[] { false, false },
                new[] { false, true }
            });

            var gameManager = new BoardManager(board);
            board = gameManager.CollapseRow(1).GetBoard();

            this.AssertBoard(board, exspected);
        }

        [Test]
        public void CollapseRow_4_x_2()
        {
            var board = this.ReverseRows(new[] {
                new[] { true, false },
                new[] { false, true },
                new[] { true, true },
                new[] { false, true }
            });
            var exspected = this.ReverseRows(new[] {
                new[] { false, false },
                new[] { true, false },
                new[] { false, true },
                new[] { false, true }
            });

            var gameManager = new BoardManager(board);
            board = gameManager.CollapseRow(1).GetBoard();

            this.AssertBoard(board, exspected);
        }

        [Test]
        public void CollapseRow_3_x_3()
        {
            var board = this.ReverseRows(new []
            {
                new[] { false, true, false },
                new[] { true, true, true },
                new[] { true, false, true }
            });
            var exspected = this.ReverseRows(new[] {
                new[] { false, false, false },
                new[] { false, true, false },
                new[] { true, false, true }
            });

            var gameManager = new BoardManager(board);
            board = gameManager.CollapseRow(1).GetBoard();

            this.AssertBoard(board, exspected);
        }

        [Test]
        public void CheckBoard_collapse_none_rows()
        {
            var board = this.ReverseRows(new[]
            {
                new[] { false, true, false },
                new[] { true, false, true },
                new[] { true, false, false }
            });
            var exspected = this.ReverseRows(new[] {
                new[] { false, true, false },
                new[] { true, false, true },
                new[] { true, false, false }
            });

            var gameManager = new BoardManager(board);
            board = gameManager.CheckBoard().GetBoard();

            this.AssertBoard(board, exspected);
        }

        [Test]
        public void CheckBoard_collapse_one_row()
        {
            var board = this.ReverseRows(new[]
            {
                new[] { false, true, false },
                new[] { true, true, true },
                new[] { true, false, true }
            });
            var exspected = this.ReverseRows(new[] {
                new[] { false, false, false },
                new[] { false, true, false },
                new[] { true, false, true }
            });

            var gameManager = new BoardManager(board);
            board = gameManager.CheckBoard().GetBoard();

            this.AssertBoard(board, exspected);
        }

        [Test]
        public void CheckBoard_collapse_two_rows()
        {
            var board = this.ReverseRows(new[]
            {
                new[] { false, true, false },
                new[] { true, true, true },
                new[] { true, true, true }
            });
            var exspected = this.ReverseRows(new[] {
                new[] { false, false, false },
                new[] { false, false, false },
                new[] { false, true, false }
            });

            var gameManager = new BoardManager(board);
            board = gameManager.CheckBoard().GetBoard();

            this.AssertBoard(board, exspected);
        }

        [Test]
        public void SpawnBlock_i_block_is_placed_correctly()
        {
            var board = 
                @"0000000000
                  0000000000
                  0000000000
                  0000000000
                  0000000000".StringToBoolMatrix(5);
            var exspected =
                @"0001111000
                  0000000000
                  0000000000
                  0000000000
                  0000000000".StringToBoolMatrix(5);

            var gameManager = new BoardManager(board);
            gameManager.SpawnBlock(BlockType.I);
            gameManager.Lockblock();
            board = gameManager.CheckBoard().GetBoard();

            this.AssertBoard(board, exspected);
        }

        [Test]
        public void SpawnBlock_o_block_is_placed_correctly()
        {
            var board =
                @"0000000000
                  0000000000
                  0000000000
                  0000000000
                  0000000000".StringToBoolMatrix(5);
            var exspected =
                @"0000110000
                  0000110000
                  0000000000
                  0000000000
                  0000000000".StringToBoolMatrix(5);

            var gameManager = new BoardManager(board);
            gameManager.SpawnBlock(BlockType.O);
            gameManager.Lockblock();
            board = gameManager.CheckBoard().GetBoard();

            this.AssertBoard(board, exspected);
        }

        [Test]
        public void SpawnBlock_t_block_is_placed_correctly()
        {
            var board =
                @"0000000000
                  0000000000
                  0000000000
                  0000000000
                  0000000000".StringToBoolMatrix(5);
            var exspected =
                @"0001110000
                  0000100000
                  0000000000
                  0000000000
                  0000000000".StringToBoolMatrix(5);

            var gameManager = new BoardManager(board);
            gameManager.SpawnBlock(BlockType.T);
            gameManager.Lockblock();
            board = gameManager.CheckBoard().GetBoard();

            this.AssertBoard(board, exspected);
        }

        [Test]
        public void SpawnBlock_j_block_is_placed_correctly()
        {
            var board =
                @"0000000000
                  0000000000
                  0000000000
                  0000000000
                  0000000000".StringToBoolMatrix(5);
            var exspected =
                @"0001110000
                  0000010000
                  0000000000
                  0000000000
                  0000000000".StringToBoolMatrix(5);

            var gameManager = new BoardManager(board);
            gameManager.SpawnBlock(BlockType.J);
            gameManager.Lockblock();
            board = gameManager.CheckBoard().GetBoard();

            this.AssertBoard(board, exspected);
        }

        [Test]
        public void SpawnBlock_l_block_is_placed_correctly()
        {
            var board =
                @"0000000000
                  0000000000
                  0000000000
                  0000000000
                  0000000000".StringToBoolMatrix(5);
            var exspected =
                @"0001110000
                  0001000000
                  0000000000
                  0000000000
                  0000000000".StringToBoolMatrix(5);

            var gameManager = new BoardManager(board);
            gameManager.SpawnBlock(BlockType.L);
            gameManager.Lockblock();
            board = gameManager.CheckBoard().GetBoard();

            this.AssertBoard(board, exspected);
        }

        [Test]
        public void SpawnBlock_s_block_is_placed_correctly()
        {
            var board =
                @"0000000000
                  0000000000
                  0000000000
                  0000000000
                  0000000000".StringToBoolMatrix(5);
            var exspected =
                @"0000110000
                  0001100000
                  0000000000
                  0000000000
                  0000000000".StringToBoolMatrix(5);

            var gameManager = new BoardManager(board);
            gameManager.SpawnBlock(BlockType.S);
            gameManager.Lockblock();
            board = gameManager.CheckBoard().GetBoard();

            this.AssertBoard(board, exspected);
        }

        [Test]
        public void SpawnBlock_z_block_is_placed_correctly()
        {
            var board =
                @"0000000000
                  0000000000
                  0000000000
                  0000000000
                  0000000000".StringToBoolMatrix(5);
            var exspected =
                @"0001100000
                  0000110000
                  0000000000
                  0000000000
                  0000000000".StringToBoolMatrix(5);

            var gameManager = new BoardManager(board);
            gameManager.SpawnBlock(BlockType.Z);
            gameManager.Lockblock();
            board = gameManager.CheckBoard().GetBoard();

            this.AssertBoard(board, exspected);
        }
    }
}
