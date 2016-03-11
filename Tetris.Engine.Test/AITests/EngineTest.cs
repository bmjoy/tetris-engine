namespace Tetris.Engine.Test.AITests
{
    using NUnit.Framework;

    using Tetris.Engine.AI;
    using Tetris.Engine.AI.Algorithms;
    using Tetris.Engine.AI.Algorithms.Weights;
    using Tetris.Engine.Extensions;
    using Tetris.Engine.GameStates;

    [TestFixture]
    public class EngineTest : TestBase
    {
        [Test]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000", BlockType.L, 1)]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000", BlockType.S, 5)]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000001100000
                    0000000000110000", BlockType.S, 5)]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000", BlockType.O, 2)]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000001", BlockType.O, 2)]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111111111101111", BlockType.O, 3)]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0101010101010110", BlockType.O, 3)]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111111111111100", BlockType.O, 1)]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0101010101010110
                    0101010101010110
                    0101010101010110
                    1111111111111100", BlockType.O, 9)]
        public void GetNextMoveTest(string input, BlockType newBlockType, int expeceted)
        {
            var boolMatrix = this.ReverseRows(input.StringToBoolMatrix(8));
            var weights = new TsitsiklisWeights(1, 3);
            var algorithm = new Tsitsiklis(weights);
            var blockPosition = new Position { Column = boolMatrix[0].Length / 2, Row = boolMatrix.GetLength(0) -4 };
            var manager = new BoardManager(boolMatrix, new Playing(), new Block(newBlockType, blockPosition));
            var engine = new Engine(algorithm);
            var bestMove = engine.GetNextMove(manager);

            Assert.AreEqual(expeceted, bestMove.Fitness);
        }
    }
}
