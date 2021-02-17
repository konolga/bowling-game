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
    [Route("[scores]")]
    public class ScoresController : ControllerBase
    {
        private readonly IScoreRepo _repo;
        private readonly ILogger<ScoresController> _logger;
        public ScoresController(IScoreRepo repo, ILogger<ScoresController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpPost("{}")]
        public async Task<IActionResult> CreateScore(Score score)
        {
            await _repo.AddScore(score);
            return Ok();
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetScoreByUserId(int userId)
        {
            var scores =  new List<Score>();
            scores = await _repo.GetScoreByUserId(userId);
            return Ok(scores);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteScore(int id)
        {
            await _repo.DeleteScore(id);
            return NoContent();
        }
    }
}
