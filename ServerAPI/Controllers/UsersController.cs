using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Data;
using ServerAPI.Infrastructure;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepo _repo;
        private readonly ILogger<UsersController> _logger;
        public UsersController(IUsersRepo repo, ILogger<UsersController> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromForm] string username)
        {
            if (await _repo.GetUserByUsername(username) != null) return Ok();
            var user = new User()
            {
                Username = username
            };
            await _repo.CreateUser(user);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromBody] User user, int id)
        {
            if (await _repo.UpdateUser(user))
                return Ok();

            return BadRequest($"Updating user id {id} failed on save");
        }

        [HttpGet("top/{topNumber}")]
        public async Task<IActionResult> GetTopUsers(int topNumber)
        {
            List<User> users = await _repo.GetTopUsers(topNumber);
            return Ok(users);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _repo.DeleteUser(id);
            return NoContent();
        }
    }
}
