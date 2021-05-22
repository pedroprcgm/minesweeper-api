using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MineSweeper.Application.Interfaces;
using MineSweeper.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MineSweeper.Services.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserAppService _service;
        private readonly IGameAppService _serviceGame;

        public UsersController(ILogger<UsersController> logger,
                               IUserAppService service,
                               IGameAppService serviceGame
                               )
        {
            _logger = logger;
            _service = service;
            _serviceGame = serviceGame;
        }

        /// <summary>
        /// Create a user with email and password
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Boolean indicating success</returns>
        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateUser([FromBody]UserViewModel user)
        {
            try
            {
                bool result = await _service.CreateUser(user);

                return Ok(result);
            }
            catch (Exception e)
            {
                if (e is ArgumentException)
                    return BadRequest();

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Login user with email and password
        /// </summary>
        /// <param name="user"></param>
        /// <returns>The bearer token for user authorization</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login([FromBody]UserViewModel user)
        {
            try
            {
                string token = await _service.Login(user);

                return Ok(token);
            }
            catch (Exception e)
            {
                if (e is ArgumentException)
                    return BadRequest();

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Login user with email and password
        /// </summary>
        /// <returns>A list of games created by logged user</returns>
        [HttpGet("logged/games")]
        [Authorize]
        [ProducesResponseType(typeof(List<GameDetailViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> GetGamesByLoggedUser()
        {
            try
            {
                List<GameDetailViewModel> games = await _serviceGame.GetGamesByLoggedUser();

                return Ok(games);
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
