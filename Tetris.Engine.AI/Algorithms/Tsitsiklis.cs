namespace Tetris.Engine.AI.Algorithms
{
    using Tetris.Engine.AI.Algorithms.Weights;

    public class Tsitsiklis : IAlgorithm
    {
        private readonly int height;
        private readonly int holes;

        public Tsitsiklis(TsitsiklisWeights tsitsiklisWeights)
        {
            this.height = tsitsiklisWeights.Height;
            this.holes = tsitsiklisWeights.Holes;
        }

        public int CalculateFitness(bool[][] gameBoard)
        {
            var fitness = 0;
            var maxHeight = 0;
            for (var column = 0; column < gameBoard[0].Length; column++)
            {
                var reachedTopColumn = false;

                for (var row = gameBoard.GetLength(0) - 1; row >= 0; row--)
                {
                    var field = gameBoard[row][column];
                    if (reachedTopColumn && !field)
                    {
                        fitness += this.holes;
                    }

                    if (field)
                    {
                        reachedTopColumn = true;

                        if (row + 1 > maxHeight)
                        {
                            maxHeight = row + 1;
                        }
                    }
                }
            }

            fitness += maxHeight * this.height;

            return fitness;
        }
    }
}
