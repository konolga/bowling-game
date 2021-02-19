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
{   [ApiController]
    [Route("api/[controller]")]
    public class ScoresController : ControllerBase
    {
        private readonly IScoresRepo _repo;
        private readonly ILogger<ScoresController> _logger;
        public ScoresController(IScoresRepo repo, ILogger<ScoresController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateScore([FromBody] Score score)
        {
            await _repo.AddScore(score);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetScoreByUserId(int id)
        {
            var scores =  new List<Score>();
            scores = await _repo.GetScoreByUserId(id);
            return Ok(scores);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteScore(int id)
        {
            await _repo.DeleteScore(id);
            return NoContent();
        }
    }
}
