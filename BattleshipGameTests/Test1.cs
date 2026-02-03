using BattleshipGameApi.Constants;
using BattleshipGameApi.GameModels;

namespace BattleshipGameTests
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void IsShipSunk()
        {
            var map = new GameMap(10);
            map.PlaceShip(ShipShapes.Type1, 1, 1);

            map.Cells[2, 1] = CellState.Hit;
            map.Cells[3, 1] = CellState.Hit;
            map.Cells[3, 2] = CellState.Hit;
            map.Cells[4, 1] = CellState.Hit;

            Assert.IsFalse(map.IsShipSunk(4, 1));

            map.Cells[1, 1] = CellState.Hit;
            Assert.IsTrue(map.IsShipSunk(4, 1));
        }
    }
}
