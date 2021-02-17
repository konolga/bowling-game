using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServerAPI.Data;
using ServerAPI.Infrastructure;
using System.Collections.Generic;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("[users]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepo _repo;
        private readonly ILogger<UsersController> _logger;
        public UsersController(IUsersRepo repo, ILogger<UsersController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpPost("{}")]
        public async Task<IActionResult> CreateUser(User user)
        {
            if (await _repo.GetUserByUsername(user.Username) == null)
            {
                return BadRequest("Username is already taken");
            }
            else
            {
                await _repo.CreateUser(user);
            }
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(User user)
        {
            if (await _repo.UpdateUser(user))
                return Ok();

            return BadRequest($"Updating user id {user.Id} failed on save");
        }

        [HttpGet("top")]
        public async Task<IActionResult> GetTopUsers(int topNumber)
        {
            var users =  new List<User>();
            users = await _repo.GetTopUsers(topNumber);
            return Ok(users);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _repo.DeleteUser(id);
            return NoContent();
        }
    }
}
