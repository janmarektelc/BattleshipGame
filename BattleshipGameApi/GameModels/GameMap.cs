namespace BattleshipGameApi.GameModels
{
    /// <summary>
    /// Represents a two-dimensional, square game map composed of cells with associated states.
    /// </summary>
    public class GameMap
    {
        /// <summary>
        /// Gets the number of elements contained in the collection.
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Gets the two-dimensional array representing the current state of all cells on the map.
        /// </summary>
        public CellState[,] Cells { get; }

        /// <summary>
        /// Initializes a new instance of the GameMap class with the specified map size.
        /// </summary>
        /// <param name="size">The length of one side of the square map, in cells.</param>
        public GameMap(int size)
        {
            Size = size;
            Cells = new CellState[size, size];
        }

        /// <summary>
        /// Writes a visual representation of the game map to the console output.
        /// </summary>
        public void PrintMap()
        {
            for (int y = 0; y < this.Size; y++)
            {
                for (int x = 0; x < this.Size; x++)
                {
                    char cellChar = this.Cells[x, y] switch
                    {
                        CellState.Empty => '~',
                        CellState.Ship => 'S',
                        CellState.Hit => 'X',
                        CellState.Blocked => '.',
                        _ => '?'
                    };
                    Console.Write(cellChar);
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Attempts to place the specified ship shape at a random valid position and orientation on the game map.
        /// </summary>>
        /// <param name="shape">The shape of the ship to place.</param>
        /// <param name="maxAttempts">The maximum number of placement attempts before the method fails. Must be greater than zero. The default is 100.</param>
        /// <exception cref="InvalidOperationException">Thrown if the ship cannot be placed after the specified number of attempts.</exception>
        public void PlaceShipRandomly(ShipShape shape, int maxAttempts = 100)
        {
            var random = new Random();
            var shapeToPlace = shape;
            switch (random.Next(0, 4))
            {
                case 1:
                    shapeToPlace = shape.Rotate90();
                    break;
                case 2:
                    shapeToPlace = shape.Rotate180();
                    break;
                case 3:
                    shapeToPlace = shape.Rotate270();
                    break;
            }

            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                int x = random.Next(0, this.Size);
                int y = random.Next(0, this.Size);
                if (this.CanPlaceShip(shapeToPlace, x, y))
                {
                    this.PlaceShip(shapeToPlace, x, y);
                    return;
                }
            }
            throw new InvalidOperationException("Failed to place ship after maximum attempts.");
        }

        /// <summary>
        /// Determines whether the specified coordinates are within the bounds of the game map.
        /// </summary>
        /// <param name="x">The x-coordinate to test.</param>
        /// <param name="y">The Return y-coordinate to test.</param>
        /// <returns>Return true if the specified coordinates are inside the bounds of the map, otherwise false.</returns>
        public bool IsInside(int x, int y) => x >= 0 && x < this.Size && y >= 0 && y < this.Size;

        /// <summary>
        /// Determines whether the ship located at the specified coordinates has been sunk.
        /// </summary>
        /// <param name="x">The x-coordinate of the ship's location.</param>
        /// <param name="y">The y-coordinate of the ship's location.</param>
        /// <returns>Returns true if the ship at the specified coordinates is sunk, otherwise, false.</returns>
        public bool IsShipSunk(int x, int y)
        {
            if (Cells[x, y] != CellState.Hit) return false;

            return IsShipSunk(x, y, new List<(int, int)>());
        }
        private bool IsShipSunk(int x, int y, List<(int, int)> doNotProcess)
        {
            doNotProcess.Add((x, y));

            List<(int, int)> cellsWithHit = new List<(int, int)>();
            foreach (var (neighbourX, neighbourY) in NeighborsIncludingSelf(x, y))
            {
                if (doNotProcess.Contains((neighbourX, neighbourY)))
                    continue;
                if (this.IsInside(neighbourX, neighbourY))
                {
                    if (this.Cells[neighbourX, neighbourY] == CellState.Ship)
                    {
                        return false;
                    }
                    else if (this.Cells[neighbourX, neighbourY] == CellState.Hit)
                    {
                        cellsWithHit.Add((neighbourX, neighbourY));
                    }
                }
            }

            foreach (var (hitX, hitY) in cellsWithHit)
            {
                if (!IsShipSunk(hitX, hitY, doNotProcess))
                {
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// Determines whether all ships on the board have been sunk.
        /// </summary>
        /// <returns>Returns true if there are no remaining ship cells on the board, otherwise, false.</returns>
        public bool AreAllShipsSunk() => this.Cells.Cast<CellState>().All(c => c != CellState.Ship);

        /// <summary>
        /// Determines whether a ship of the specified shape can be placed at the given coordinates on the game map without overlapping existing ships or exceeding map boundaries.
        /// </summary>
        /// <param name="shape">The shape of the ship to place. Defines the relative cell positions occupied by the ship.</param>
        /// <param name="x">The x-coordinate of the top-left position where the ship's shape will be placed.</param>
        /// <param name="y">The y-coordinate of the top-left position where the ship's shape will be placed.</param>
        /// <returns>Returns true if the ship can be placed at the specified location without overlapping other ships or exceeding the map boundaries, otherwise false.</returns>
        private bool CanPlaceShip(ShipShape shape, int x, int y)
        {
            var cells = shape.Cells
                .Select(c => (x + c.dx, y + c.dy))
                .ToList();

            foreach (var (cellX, cellY) in cells)
            {
                if (!this.IsInside(cellX, cellY))
                    return false;

                if (this.Cells[cellX, cellY] != CellState.Empty)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Places a ship on the specified game map at the given coordinates using the provided ship shape.
        /// </summary>
        /// <param name="shape">The shape of the ship to place. Defines the relative cell positions occupied by the ship.</param>
        /// <param name="x">The x-coordinate of the starting position for the ship placement.</param>
        /// <param name="y">The y-coordinate of the starting position for the ship placement.</param>
        public void PlaceShip(ShipShape shape, int x, int y)
        {
            var cells = shape.Cells
                .Select(c => (x + c.dx, y + c.dy))
                .ToList();

            foreach (var (cellX, cellY) in cells)
            {
                this.Cells[cellX, cellY] = CellState.Ship;

                foreach (var (nx, ny) in NeighborsIncludingSelf(cellX, cellY))
                {
                    if (this.IsInside(nx, ny) && this.Cells[nx, ny] == CellState.Empty)
                        this.Cells[nx, ny] = CellState.Blocked;
                }
            }
        }

        /// <summary>
        /// Returns the coordinates of all neighboring cells, including the cell at the specified position
        /// </summary>
        /// <param name="x">The x-coordinate of the center cell.</param>
        /// <param name="y">The y-coordinate of the center cell.</param>
        /// <returns>An enumerable collection of (x, y) tuples representing the coordinates of the center cell and its eight immediate neighbors.</returns>
        private static IEnumerable<(int x, int y)> NeighborsIncludingSelf(int x, int y)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    yield return (x + dx, y + dy);
                }
            }
        }
    }
}
