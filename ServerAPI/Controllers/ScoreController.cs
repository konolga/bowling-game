using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServerAPI.Data;
using ServerAPI.Infrastructure;



namespace ServerAPI.Controllers
{
    [ApiController]
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
        public async Task<IActionResult> CreateScore([FromForm] string score)
        {
            Score scoreToSave = null;
            try
            {
                scoreToSave = JsonSerializer.Deserialize<Score>(score);
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex, "Couldn't parse Score");
            }

            if (scoreToSave == null) return BadRequest();
            await _repo.AddScore(scoreToSave);
            return Ok();

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetScoreByUserId(int id)
        {
            var scores = await _repo.GetScoreByUserId(id);
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
