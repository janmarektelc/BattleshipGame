using System.ComponentModel.DataAnnotations;

namespace BattleshipGameApi.Models
{
    /// <summary>
    /// New game description.
    /// </summary>
    public class NewGameDto
    {
        /// <summary>
        /// Player 1 name.
        /// </summary>
        public required string Player1Name { get; set; }

        /// <summary>
        /// Player 2 name.
        /// </summary>
        public required string Player2Name { get; set; }

        /// <summary>
        /// Game map size. Must be between 10 and 20.
        /// </summary>
        [Range(10, 20, ErrorMessage = "Map size must be between 10 and 20.")]
        public required int MapSize { get; set; } = 10;
    }
}
