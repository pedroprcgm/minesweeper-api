using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MineSweeper.Application.Interfaces;
using MineSweeper.Application.ViewModels;
using System;
using System.Net;
using System.Threading.Tasks;

namespace MineSweeper.Services.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GamesController : ControllerBase
    {
        private readonly ILogger<GamesController> _logger;
        private readonly IGameAppService _service;

        public GamesController(ILogger<GamesController> logger,
                               IGameAppService service)
        {
            _logger = logger;
            _service = service;
        }

        /// <summary>
        /// Create a game customizing number of rows, cols and mines
        /// </summary>
        /// <param name="game"></param>
        /// <returns>Created game id</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateGame([FromBody]GameViewModel game)
        {
            try
            {
                Guid _gameId = await _service.CreateGame(game);

                return Ok(_gameId);
            }
            catch (Exception e)
            {
                if (e is ArgumentException)
                    return BadRequest();

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Get game information
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Game details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(GameDetailViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetGame(Guid id)
        {
            try
            {
                GameDetailViewModel game = await _service.GetById(id);

                return Ok(game);
            }
            catch (Exception e)
            {
                if (e is ArgumentException)
                    return BadRequest();

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Pause a game
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A bool indicating if the action was applied</returns>
        [HttpPut("{id}/pause")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PauseGame(Guid id)
        {
            try
            {
                bool _result = await _service.PauseGame(id);

                return Ok(_result);
            }
            catch (Exception e)
            {
                if (e is ArgumentException)
                    return BadRequest();

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Resume a game
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A bool indicating if the action was applied</returns>
        [HttpPut("{id}/resume")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ResumeGame(Guid id)
        {
            try
            {
                bool _result = await _service.ResumeGame(id);

                return Ok(_result);
            }
            catch (Exception e)
            {
                if (e is ArgumentException)
                    return BadRequest();

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }


        /// <summary>
        /// Endpoint to visit a cell informating game id, number of row and col
        /// </summary>
        /// <param name="id"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns>The result of the action to visit a cell</returns>
        [HttpPut("{id}/rows/{row}/cols/{col}/visit-cell")]
        [ProducesResponseType(typeof(VisitCellResultViewModel), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> VisitCell(Guid id, int row, int col)
        {
            try
            {
                VisitCellResultViewModel _result = await _service.VisitCell(id, row, col);

                return Ok(_result);
            }
            catch (Exception e)
            {
                if (e is ArgumentException)
                    return BadRequest();

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Endpoint to flag a cell with question mark or red flag
        /// </summary>
        /// <param name="id"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns>A bool indicating if the action was applied</returns>
        [HttpPut("{id}/rows/{row}/cols/{col}/flag")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> FlagCell(Guid id, int row, int col, FlagCellViewModel flag)
        {
            try
            {
                bool _result = await _service.FlagCell(id, row, col, flag);

                return Ok(_result);
            }
            catch (Exception e)
            {
                if (e is ArgumentException)
                    return BadRequest();

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
