namespace BattleshipGameApi.GameModels
{
    /// <summary>
    /// Specifies the current operational state of the game engine.
    /// </summary>
    public enum GameEngineStatus
    {
        Unknown = 0,
        Initializing = 1,
        InProgress = 2,
        Completed = 3
    }
}
