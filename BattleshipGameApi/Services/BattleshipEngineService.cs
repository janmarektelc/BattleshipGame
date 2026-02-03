using BattleshipGameApi.GameModels;
using BattleshipGameApi.Interfaces;

namespace BattleshipGameApi.Services
{
    /// <summary>
    /// Battleship game engine service.
    /// </summary>
    internal class BattleshipEngineService : IBattleshipEngineService
    {
        private GameEngineStatus _currentStatus = GameEngineStatus.Unknown;

        /// <summary>
        /// Starts a new game session with the specified player names and map size.
        /// </summary>
        /// <param name="player1Name">The name of the first player. Cannot be null or empty.</param>
        /// <param name="player2Name">The name of the second player. Cannot be null or empty.</param>
        /// <param name="mapSize">The size of the game map. Must be a positive integer.</param>
        public void StartNewGame(string player1Name, string player2Name, int mapSize)
        {
            _currentStatus = GameEngineStatus.Initializing;

        }

        /// <summary>
        /// Attempts to make a move at the specified map coordinates.
        /// </summary>
        /// <param name="coordinateX">The zero-based horizontal coordinate of the move. Must be within the valid range of the game map.</param>
        /// <param name="coordinateY">The zero-based vertical coordinate of the move. Must be within the valid range of the game map.</param>
        /// <returns>A GameEngineMoveResult value that indicates the outcome of the attempted move, such as Miss, Hit or Sunk</returns>
        public GameEngineMoveResult MakeMove(int coordinateX, int coordinateY)
        {
            return GameEngineMoveResult.Miss;
        }

        /// <summary>
        /// Gets the current status of the game engine.
        /// </summary>
        /// <returns>Returns current status of the game engine.</returns>
        public GameEngineStatus GetGameStatus() => _currentStatus;
    }



}
