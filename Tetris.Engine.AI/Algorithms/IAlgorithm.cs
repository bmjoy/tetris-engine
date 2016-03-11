namespace Tetris.Engine.AI.Algorithms
{
    public interface IAlgorithm
    {
        int CalculateFitness(bool[][] gameBoard);
    }
}
