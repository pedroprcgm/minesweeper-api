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

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateGame([FromBody]GameViewModel game)
        {
            await _service.CreateGame(game);

            return Ok();
        }
    }
}
