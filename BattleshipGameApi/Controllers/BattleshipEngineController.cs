using BattleshipGameApi.Helpers;
using BattleshipGameApi.Interfaces;
using BattleshipGameApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BattleshipGameApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BattleshipEngineController : Controller
    {
        private readonly IBattleshipEngineService _battleshipEngineService;

        public BattleshipEngineController(IBattleshipEngineService battleshipEngineService)
        {
            this._battleshipEngineService = battleshipEngineService;
        }

        /// <summary>
        /// Starts new game with the given parameters.
        /// </summary>
        /// <param name="newGameDto">New game description</param>
        /// <returns>Returns Ok if new game started.</returns>
        [HttpPost("StartNewGame")]
        public IActionResult StartNewGame(NewGameDto newGameDto)
        {
            this._battleshipEngineService.StartNewGame(newGameDto.Player1Name, newGameDto.Player2Name, newGameDto.MapSize);

            return Ok();
        }

        /// <summary>
        /// Attempts to make a move at the specified map coordinates.
        /// </summary>
        /// <param name="coordinateX">The zero-based horizontal coordinate of the move. Must be within the valid range of the game board.</param>
        /// <param name="coordinateY">The zero-based vertical coordinate of the move. Must be within the valid range of the game board.</param>
        /// <returns>A GameMoveResult value that indicates the outcome of the attempted move, such as Miss, Hit or Sunk</returns>
        [HttpPost("MakeMove")]
        public ActionResult<GameMoveResult> MakeMove([Range(0, 20)] int coordinateX, [Range(0, 20)] int coordinateY)
        {
            var result = this._battleshipEngineService.MakeMove(coordinateX, coordinateY);

            return Ok(result.ToGameMoveResult());
        }
    }
}
