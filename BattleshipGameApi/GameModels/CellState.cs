namespace BattleshipGameApi.GameModels
{
    /// <summary>
    /// Cell states on the game map.
    /// </summary>
    public enum CellState
    {
        Empty,
        Ship,
        Hit,
        Blocked // Cells where no other ships can be placed
    }
}
