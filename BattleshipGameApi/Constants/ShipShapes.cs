using BattleshipGameApi.GameModels;

namespace BattleshipGameApi.Constants
{
    /// <summary>
    /// Provides predefined ship shapes.
    /// </summary>
    public static class ShipShapes
    {
        public static readonly ShipShape SingleCell = new ShipShape(new[] { (0, 0) });
        public static readonly ShipShape TwoCells = new ShipShape(new[] { (0, 0), (1, 0) });
        public static readonly ShipShape ThreeCells = new ShipShape(new[] { (0, 0), (1, 0), (2, 0) });
        public static readonly ShipShape LShape = new ShipShape(new[] { (0, 0), (1, 0), (1, 1) });
        public static readonly ShipShape CrossShape = new ShipShape(new[] { (1, 0), (0, 1), (1, 1), (2, 1), (1, 2) });
        public static readonly ShipShape Type1 = new ShipShape(new[] { (0, 0), (1, 0), (2, 0), (3, 0), (2, 1) });
    }
}
