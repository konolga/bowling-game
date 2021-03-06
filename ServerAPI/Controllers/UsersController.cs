﻿using System.Threading.Tasks;
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
            var user = await _repo.GetUserByUsername(username);
            if (user == null)
            {
                user = new User() {Username = username};
                await _repo.CreateUser(user);
            };
           
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser([FromForm] User user, int id)
        {
            if (await _repo.UpdateUser(user))
                return Ok();

            return BadRequest($"Updating user id {id} failed on save");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await _repo.DeleteUser(id);
            return NoContent();
        }
    }
}
