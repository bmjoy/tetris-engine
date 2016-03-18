namespace Tetris.Engine.Test
{
    using NUnit.Framework;

    using Tetris.Engine.Extensions;

    [TestFixture]
    public class StringExtensionTest
    {
        private static readonly bool[][] Expected = { new[] { true, false, true, false }, new [] { true, false, true, false }, new [] { true, false, true, false }, new [] { true, false, true, false } };

        [Test]
        [TestCase("/da1010101010101010")]
        [TestCase("1010101010101010")]
        [TestCase("10asdas10101010101010")]
        [TestCase(@"1
0101
01010101010")]
        public void StringToBoolMatrixTests(string input)
        {
            var result = input.StringToBoolMatrix(4);

            Assert.AreEqual(Expected.Length, result.Length, "row length");
            Assert.AreEqual(Expected[0].Length, result[0].Length, "column length");

            for (var rowIndex = 0; rowIndex < input.StringToBoolMatrix(4).Length; rowIndex++)
            {
                for (var columnIndex = 0; columnIndex < Expected[0].Length; columnIndex++)
                {
                    Assert.That(result[rowIndex][columnIndex], Is.EqualTo(Expected[rowIndex][columnIndex]));
                }
            }
        }
    }
}
