namespace Tetris.Engine.Test.AITests
{
    using System.Linq;

    using NUnit.Framework;

    using Tetris.Engine.AI;
    using Tetris.Engine.AI.Algorithms;
    using Tetris.Engine.AI.Algorithms.Weights;
    using Tetris.Engine.Extensions;

    using Move = Tetris.Engine.Move;

    [TestFixture]
    public class MoveTests : TestBase
    {
        [Test]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111111111110000", BlockType.I, new [] { Move.Right, Move.Right, Move.Right, Move.Right, Move.Right, Move.Right })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111111111100001", BlockType.I, new[] { Move.Right, Move.Right, Move.Right, Move.Right, Move.Right })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111111111000011", BlockType.I, new[] { Move.Right, Move.Right, Move.Right, Move.Right })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111111110000111", BlockType.I, new[] { Move.Right, Move.Right, Move.Right })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111111100001111", BlockType.I, new[] { Move.Right, Move.Right })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111111000011111", BlockType.I, new[] { Move.Right })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111110000111111", BlockType.I, new Move[0])]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000111111111111", BlockType.I, new[] { Move.Left, Move.Left, Move.Left, Move.Left, Move.Left, Move.Left })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1000011111111111", BlockType.I, new[] { Move.Left, Move.Left, Move.Left, Move.Left, Move.Left })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1100001111111111", BlockType.I, new[] { Move.Left, Move.Left, Move.Left, Move.Left })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1110000111111111", BlockType.I, new[] { Move.Left, Move.Left, Move.Left })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111000011111111", BlockType.I, new[] { Move.Left, Move.Left })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111100001111111", BlockType.I, new[] { Move.Left })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0011111111111111
                    0011111111111111", BlockType.O, new[] { Move.Left, Move.Left, Move.Left, Move.Left, Move.Left, Move.Left, Move.Left })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1001111111111111
                    1001111111111111", BlockType.O, new[] { Move.Left, Move.Left, Move.Left, Move.Left, Move.Left, Move.Left })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1100111111111111
                    1100111111111111", BlockType.O, new[] { Move.Left, Move.Left, Move.Left, Move.Left, Move.Left })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1110011111111111
                    1110011111111111", BlockType.O, new[] { Move.Left, Move.Left, Move.Left, Move.Left })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111001111111111
                    1111001111111111", BlockType.O, new[] { Move.Left, Move.Left, Move.Left })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111100111111111
                    1111100111111111", BlockType.O, new[] { Move.Left, Move.Left })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111110011111111
                    1111110011111111", BlockType.O, new[] { Move.Left })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111111001111111
                    1111111001111111", BlockType.O, new Move[0])]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111111111111100
                    1111111111111100", BlockType.O, new[] { Move.Right, Move.Right, Move.Right, Move.Right, Move.Right, Move.Right, Move.Right })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111111111111001
                    1111111111111001", BlockType.O, new[] { Move.Right, Move.Right, Move.Right, Move.Right, Move.Right, Move.Right })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111111111110011
                    1111111111110011", BlockType.O, new[] { Move.Right, Move.Right, Move.Right, Move.Right, Move.Right })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111111111100111
                    1111111111100111", BlockType.O, new[] { Move.Right, Move.Right, Move.Right, Move.Right })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111111111001111
                    1111111111001111", BlockType.O, new[] { Move.Right, Move.Right, Move.Right })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111111110011111
                    1111111110011111", BlockType.O, new[] { Move.Right, Move.Right })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111111100111111
                    1111111100111111", BlockType.O, new[] { Move.Right })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0011111111111111
                    1011111111111111
                    1011111111111111", BlockType.L, new[] { Move.RotateRight, Move.Left, Move.Left, Move.Left, Move.Left, Move.Left, Move.Left })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1001111111111111
                    1101111111111111
                    1101111111111111", BlockType.L, new[] { Move.RotateRight, Move.Left, Move.Left, Move.Left, Move.Left, Move.Left })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1100111111111111
                    1110111111111111
                    1110111111111111", BlockType.L, new[] { Move.RotateRight, Move.Left, Move.Left, Move.Left, Move.Left })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1110011111111111
                    1111011111111111
                    1111011111111111", BlockType.L, new[] { Move.RotateRight, Move.Left, Move.Left, Move.Left })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111001111111111
                    1111101111111111
                    1111101111111111", BlockType.L, new[] { Move.RotateRight, Move.Left, Move.Left })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111100111111111
                    1111110111111111
                    1111110111111111", BlockType.L, new[] { Move.RotateRight, Move.Left })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111110011111111
                    1111111011111111
                    1111111011111111", BlockType.L, new[] { Move.RotateRight })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111111111111100
                    1111111111111110
                    1111111111111110", BlockType.L, new[] { Move.RotateRight, Move.Right, Move.Right, Move.Right, Move.Right, Move.Right, Move.Right, Move.Right, Move.Right })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111111111111001
                    1111111111111101
                    1111111111111101", BlockType.L, new[] { Move.RotateRight, Move.Right, Move.Right, Move.Right, Move.Right, Move.Right, Move.Right, Move.Right })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111111111110011
                    1111111111111011
                    1111111111111011", BlockType.L, new[] { Move.RotateRight, Move.Right, Move.Right, Move.Right, Move.Right, Move.Right, Move.Right })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111111111100111
                    1111111111110111
                    1111111111110111", BlockType.L, new[] { Move.RotateRight, Move.Right, Move.Right, Move.Right, Move.Right, Move.Right })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111111111001111
                    1111111111101111
                    1111111111101111", BlockType.L, new[] { Move.RotateRight, Move.Right, Move.Right, Move.Right, Move.Right })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111111110011111
                    1111111111011111
                    1111111111011111", BlockType.L, new[] { Move.RotateRight, Move.Right, Move.Right, Move.Right })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111111100111111
                    1111111110111111
                    1111111110111111", BlockType.L, new[] { Move.RotateRight, Move.Right, Move.Right })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111111001111111
                    1111111101111111
                    1111111101111111", BlockType.L, new[] { Move.RotateRight, Move.Right })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111110001111111
                    1111111011111111", BlockType.T, new Move[0])]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111110011111111
                    1111110011111111
                    1111111011111111", BlockType.T, new[] { Move.RotateRight })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111110001111111", BlockType.T, new[] { Move.RotateRight, Move.RotateRight })]
        [TestCase(@"0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    0000000000000000
                    1111111001111111
                    1111111001111111
                    1111111011111111", BlockType.T, new[] { Move.RotateRight, Move.RotateRight, Move.RotateRight })]
        public void MovesAreCorrect(string input, BlockType newBlockType, Move[] expeceted)
        {
            var boolMatrix = input.StringToBoolMatrix(8);
            var weights = new TsitsiklisWeights(1, 3);
            var algorithm = new Tsitsiklis(weights);
            var manager = new BoardManager(boolMatrix);
            manager.SpawnBlock(newBlockType);
            var engine = new Engine(algorithm);

            var bestMove = engine.GetNextMove(manager);
            var moves = bestMove.Moves;

            Assert.IsTrue(bestMove.IsValid);
            Assert.That(bestMove.Fitness, Is.AtMost(1), "Fitness");

            Assert.AreEqual(expeceted.Where(x => x == Move.Right).Count(), moves.Where(x => x == Move.Right).Count());
            Assert.AreEqual(expeceted.Where(x => x == Move.Left).Count(), moves.Where(x => x == Move.Left).Count());
            Assert.AreEqual(expeceted.Where(x => x == Move.RotateLeft).Count(), moves.Where(x => x == Move.RotateLeft).Count());
            Assert.AreEqual(expeceted.Where(x => x == Move.RotateRight).Count(), moves.Where(x => x == Move.RotateRight).Count());
        }
    }
}
