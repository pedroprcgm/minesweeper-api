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
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserAppService _service;

        public UsersController(ILogger<UsersController> logger,
                               IUserAppService service)
        {
            _logger = logger;
            _service = service;
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

    }
}
