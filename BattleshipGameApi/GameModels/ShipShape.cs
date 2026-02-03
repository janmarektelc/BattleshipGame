namespace BattleshipGameApi.GameModels
{
    /// <summary>
    /// Represents the relative cell positions that define the shape of a ship, supporting rotation operations in
    /// 90-degree increments.
    /// </summary>
    public class ShipShape
    {
        /// <summary>
        /// Gets the collection of cell offsets that define the shape relative to its origin.
        /// </summary>
        public IReadOnlyList<(int dx, int dy)> Cells { get; }

        /// <summary>
        /// Initializes a new instance of the ShipShape class using the specified cell offsets.
        /// </summary>
        /// <param name="cells">An enumerable collection of relative cell positions, where each tuple represents the offset (dx, dy) from the origin of the shape.</param>
        public ShipShape(IEnumerable<(int dx, int dy)> cells)
        {
            Cells = cells.ToList();
        }

        /// <summary>
        /// Returns a new ShipShape instance that represents this shape rotated 90 degrees clockwise.
        /// </summary>
        /// <returns>A ShipShape containing the cells of the original shape rotated 90 degrees clockwise. The returned instance
        /// does not modify the original shape.</returns>
        public ShipShape Rotate90()
        {
            return new ShipShape(
                Cells.Select(c => (c.dy, -c.dx))
            );
        }

        /// <summary>
        /// Returns a new ShipShape instance that represents this shape rotated 180 degrees clockwise.
        /// </summary>
        /// <returns>A ShipShape containing the cells of the original shape rotated 180 degrees clockwise. The returned instance
        /// does not modify the original shape.</returns>
        public ShipShape Rotate180()
        {
            return new ShipShape(
                Cells.Select(c => (-c.dx, -c.dy))
            );
        }

        /// <summary>
        /// Returns a new ShipShape instance that represents this shape rotated 270 degrees clockwise.
        /// </summary>
        /// <returns>A ShipShape containing the cells of the original shape rotated 270 degrees clockwise. The returned instance
        /// does not modify the original shape.</returns>
        public ShipShape Rotate270()
        {
            return new ShipShape(
                Cells.Select(c => (-c.dy, c.dx))
            );
        }
    }
}
