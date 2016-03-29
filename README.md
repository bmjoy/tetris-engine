# Tetris Engine
C# Tetris Engine written to be pluggable for both manually play and automatic AI play.  
Supporting 3rd party AI algorithms by using the AI engine's `IAlgorithm` interface.

[![Build Status](https://travis-ci.org/cbpetersen/tetris-ai-engine-dot-net.svg?branch=master)](https://travis-ci.org/cbpetersen/tetris-ai-engine-dot-net)


# Usage
### Manual play

```
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

```
var gameManager = new GameManager(20, 10);
var ai = new AI.Engine(new Tsitsiklis(new TsitsiklisWeights(100, 200)));

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
