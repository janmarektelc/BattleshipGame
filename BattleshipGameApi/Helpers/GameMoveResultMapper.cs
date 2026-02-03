using BattleshipGameApi.GameModels;
using BattleshipGameApi.Models;

namespace BattleshipGameApi.Helpers
{
    /// <summary>
    /// Provides extension methods for mapping move results between game engine and application-level representations.
    /// </summary>
    public static class GameMoveResultMapper
    {
        /// <summary>
        /// Converts a <see cref="GameEngineMoveResult"/> value to its corresponding <see cref="GameMoveResult"/> value.
        /// </summary>
        /// <param name="moveResult">The <see cref="GameEngineMoveResult"/> value to convert.</param>
        /// <returns>The <see cref="GameMoveResult"/> value that corresponds to the specified <paramref name="moveResult"/>.</returns>
        /// <exception cref="NotImplementedException">Thrown if <paramref name="moveResult"/> is not a recognized <see cref="GameEngineMoveResult"/> value.</exception>
        public static GameMoveResult ToGameMoveResult(this GameEngineMoveResult moveResult) => moveResult switch
        {
            GameEngineMoveResult.Miss => GameMoveResult.Miss,
            GameEngineMoveResult.Hit => GameMoveResult.Hit,
            GameEngineMoveResult.Sunk => GameMoveResult.Sunk,
            _ => throw new NotImplementedException()
        };
    }
}
