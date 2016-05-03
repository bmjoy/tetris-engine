# Tetris Engine
C# Tetris Engine written to be pluggable for both manually play and automatic AI play.  
Supporting 3rd party AI algorithms by using the AI engine's `IAlgorithm` interface.

[![Build Status](https://travis-ci.org/cbpetersen/tetris-engine.svg?branch=master)](https://travis-ci.org/cbpetersen/tetris-engine)


# Usage
### Manual play

```C#
var gameManager = new GameManager(20, 10);

// Game loop
while (!gameManager.GameState.IsGameOver())
{
    gameManager.OnGameLoopStep();

    // Simple user input.
    if (keyboard.Input == Left) {
        gameManager.MoveBlock(Move.Left);
    }
    ...
}
```

### AI Agent
See example console runner at `./Runners/Tetris.Engine.AIConsoleRunner`

```C#
var gameManager = new GameManager(20, 10);
var ai = new Engine(
    new TetrisAi(
        new TetrisAiWeights
            {
                ColumnTransitions = 0.8024363520000000f,
                LandingHeight = -0.1958289440000000f,
                NumberOfHoles = 5.0289489999999999f,
                RowTransitions = -0.4794300500000000f,
                RowsCleared = -2.0772042300000000f,
                WellSums = 0.4410647000000000f
            }));

IEnumerator moveIterator = null;
var blockNumber = -1;

// Game loop
while (!gameManager.GameState.IsGameOver())
{
    // Print state and sleep wait run loop logic.
    PrintState(gameManager);
    Thread.Sleep(50);

    gameManager.OnGameLoopStep();

    if (moveIterator != null && moveIterator.MoveNext())
    {
        gameManager.MoveBlock((Move)moveIterator.Current);
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
```
