using BattleshipGameApi.Constants;
using BattleshipGameApi.GameModels;
using BattleshipGameApi.Interfaces;

namespace BattleshipGameApi.Services
{
    /// <summary>
    /// Battleship game engine service.
    /// </summary>
    internal class BattleshipEngineService : IBattleshipEngineService
    {
        static readonly int MaxMapSize = 20;
        static readonly int MaxPlayers = 2;
        static readonly ShipDefinition[] ShipDefinitions = new[]
        {
            new ShipDefinition { Shape = ShipShapes.SingleCell, Count = 2 },
            new ShipDefinition { Shape = ShipShapes.TwoCells, Count = 2 },
            new ShipDefinition { Shape = ShipShapes.ThreeCells, Count = 1 },

            new ShipDefinition { Shape = ShipShapes.CrossShape, Count = 1 },
            new ShipDefinition { Shape = ShipShapes.Type1, Count = 1 },
        };

        private GameEngineStatus currentStatus = GameEngineStatus.Unknown;
        private int currentPlayerIndex = 0;
        private string[] players = new string[MaxPlayers];
        private GameMap[] gameMaps = new GameMap[MaxPlayers];

        /// <summary>
        /// Starts a new game session with the specified player names and map size.
        /// </summary>
        /// <param name="player1Name">The name of the first player. Cannot be null or empty.</param>
        /// <param name="player2Name">The name of the second player. Cannot be null or empty.</param>
        /// <param name="mapSize">The size of the game map. Must be a positive integer.</param>
        public void StartNewGame(string player1Name, string player2Name, int mapSize)
        {
            if (string.IsNullOrWhiteSpace(player1Name))
            {
                throw new ArgumentException("Player 1 name cannot be null or empty.", nameof(player1Name));
            }
            if (string.IsNullOrWhiteSpace(player2Name))
            {
                throw new ArgumentException("Player 2 name cannot be null or empty.", nameof(player2Name));
            }
            if (mapSize <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(mapSize), "Map size must be a positive integer.");
            }
            if (mapSize > MaxMapSize)
            {
                throw new ArgumentOutOfRangeException(nameof(mapSize), $"Map size cannot exceed {MaxMapSize}.");
            }

            currentStatus = GameEngineStatus.Initializing;
            try
            {
                this.players[0] = player1Name;
                this.players[1] = player2Name;
                this.gameMaps[0] = GenerateMap(mapSize, ShipDefinitions);
                this.gameMaps[1] = GenerateMap(mapSize, ShipDefinitions);
            }
            catch
            {
                currentStatus = GameEngineStatus.Unknown;
                throw;
            }
            currentStatus = GameEngineStatus.InProgress;
            currentPlayerIndex = 0;

            Console.WriteLine($"New game started between {player1Name} and {player2Name} on a {mapSize}x{mapSize} map.");
            Console.WriteLine($"{this.players[0]} (Player 1) Map:");
            this.gameMaps[0].PrintMap();

            Console.WriteLine();
            Console.WriteLine($"{this.players[1]} (Player 2) Map:");
            this.gameMaps[1].PrintMap();
            Console.WriteLine();

            Console.WriteLine($"It's now {this.players[currentPlayerIndex]}'s turn.");
        }

        /// <summary>
        /// Attempts to make a move at the specified map coordinates.
        /// </summary>
        /// <param name="coordinateX">The zero-based horizontal coordinate of the move. Must be within the valid range of the game map.</param>
        /// <param name="coordinateY">The zero-based vertical coordinate of the move. Must be within the valid range of the game map.</param>
        /// <returns>A GameEngineMoveResult value that indicates the outcome of the attempted move, such as Miss, Hit or Sunk</returns>
        public GameEngineMoveResult MakeMove(int coordinateX, int coordinateY)
        {
            if (currentStatus != GameEngineStatus.InProgress)
            {
                throw new InvalidOperationException("Cannot make a move when the game is not in progress.");
            }
            if (!this.gameMaps[currentPlayerIndex].IsInside(coordinateX, coordinateY))
            {
                MoveToNextPlayer();
                return GameEngineMoveResult.Miss;
            }

            if (this.gameMaps[currentPlayerIndex].Cells[coordinateX, coordinateY] == CellState.Ship)
            {
                this.gameMaps[currentPlayerIndex].Cells[coordinateX, coordinateY] = CellState.Hit;
                Console.WriteLine($"{this.players[currentPlayerIndex]} hit a ship at ({coordinateX}, {coordinateY})!");
                if (this.gameMaps[currentPlayerIndex].IsShipSunk(coordinateX, coordinateY))
                {
                    Console.WriteLine($"{this.players[currentPlayerIndex]} sunk a ship!");
                    if (this.gameMaps[currentPlayerIndex].AreAllShipsSunk())
                    {
                        currentStatus = GameEngineStatus.Completed;
                        Console.WriteLine($"{this.players[currentPlayerIndex]} wins the game!");
                    }
                    this.gameMaps[currentPlayerIndex].PrintMap();
                    return GameEngineMoveResult.Sunk;
                }
                this.gameMaps[currentPlayerIndex].PrintMap();
                return GameEngineMoveResult.Hit;
            }
            this.gameMaps[currentPlayerIndex].PrintMap();
            MoveToNextPlayer();
            return GameEngineMoveResult.Miss;
        }

        /// <summary>
        /// Gets the current status of the game engine.
        /// </summary>
        /// <returns>Returns current status of the game engine.</returns>
        public GameEngineStatus GetGameStatus() => currentStatus;

        private void MoveToNextPlayer()
        {
            currentPlayerIndex = (currentPlayerIndex + 1) % MaxPlayers;
            Console.WriteLine($"It's now {this.players[currentPlayerIndex]}'s turn.");
        }

        /// <summary>
        /// Generates a new game map of the specified size and populates it with ships based on the provided ship definitions.
        /// </summary>
        /// <param name="mapSize">The size of the map to generate.</param>
        /// <param name="shipDefinitions">A collection of ship definitions specifying the types and quantities of ships to place on the map.</param>
        /// <returns>A new GameMap instance containing the placed ships according to the specified definitions.</returns>
        private GameMap GenerateMap(int mapSize, IEnumerable<ShipDefinition> shipDefinitions)
        {
            var map = new GameMap(mapSize);

            foreach (var shipDefinition in shipDefinitions)
            {
                for (int i = 0; i < shipDefinition.Count; i++)
                {
                    map.PlaceShipRandomly(shipDefinition.Shape);
                }
            }

            return map;
        }
    }
}
