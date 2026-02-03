using BattleshipGameApi.GameModels;

namespace BattleshipGameApi.Interfaces
{
    /// <summary>
    /// Battleship game engine service interface.
    /// </summary>
    public interface IBattleshipEngineService
    {
        /// <summary>
        /// Starts a new game session with the specified player names and map size.
        /// </summary>
        /// <param name="player1Name">The name of the first player. Cannot be null or empty.</param>
        /// <param name="player2Name">The name of the second player. Cannot be null or empty.</param>
        /// <param name="mapSize">The size of the game map. Must be a positive integer.</param>
        void StartNewGame(string player1Name, string player2Name, int mapSize);

        /// <summary>
        /// Attempts to make a move at the specified map coordinates.
        /// </summary>
        /// <param name="coordinateX">The zero-based horizontal coordinate of the move. Must be within the valid range of the game board.</param>
        /// <param name="coordinateY">The zero-based vertical coordinate of the move. Must be within the valid range of the game board.</param>
        /// <returns>A GameEngineMoveResult value that indicates the outcome of the attempted move, such as Miss, Hit or Sunk</returns>
        GameEngineMoveResult MakeMove(int coordinateX, int coordinateY);

        /// <summary>
        /// Gets the current status of the game engine.
        /// </summary>
        /// <returns>Returns current status of the game engine.</returns>
        GameEngineStatus GetGameStatus();
    }
}
