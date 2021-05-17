using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MineSweeper.Application.Interfaces;
using MineSweeper.Application.ViewModels;

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
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateGame([FromBody]GameViewModel game)
        {
            try
            {
                await _service.CreateGame(game);

                return Ok();
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
        /// <returns></returns>
        [HttpPut("{id}/visit-cell/row/{row}/col/{col}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> VisitCell(Guid id, int row, int col)
        {
            try
            {
                await _service.VisitCell(id, row, col);

                return Ok();
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
