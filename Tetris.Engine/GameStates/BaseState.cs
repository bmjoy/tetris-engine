namespace Tetris.Engine.GameStates
{
    using Tetris.Engine.GameStates.Interfaces;

    public abstract class BaseState : IGameState
    {
        public virtual bool IsGameOver()
        {
            return false;
        }

        public bool IsPaused()
        {
            return false;
        }
    }
}
