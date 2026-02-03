namespace BattleshipGameApi.GameModels
{
    /// <summary>
    /// Represents the definition of a ship, including its shape and the number of instances to be used in a game.
    /// </summary>
    public class ShipDefinition
    {
        /// <summary>
        /// Gets or sets the shape of the ship.
        /// </summary>
        public required ShipShape Shape { get; set; }

        /// <summary>
        /// Gets or sets the number of ships used on game map.
        /// </summary>
        public int Count { get; set; }
    }
}
