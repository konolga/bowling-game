using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServerAPI.Infrastructure;


namespace ServerAPI.Data
{

    public class ScoresRepo : IScoresRepo
    {
        private readonly DataContext _context;
        public ScoresRepo(DataContext context)
        {
            _context = context;
        }
        public async Task AddScore(Score score)
        {
            try
            {
                score.CreationTime = DateTime.Now;
                await _context.Scores.AddAsync(score);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception($"Saving user {score.Id} failed on save");
            }
        }

        public async Task<List<Score>> GetScoreByUserId(int id)
        {
            try
            {
                return await _context.Scores.Where(s => s.UserId == id).ToListAsync();
            }
            catch
            {
                throw new Exception($"Failed to get score id: {id}");
            }
        }

        public IEnumerable<object> GetTopResults(int topNumber)
        {

            try
            { 
                return (from u in _context.Users
                           from s in _context.Scores
                           where u.Id == s.UserId
                           orderby s.TotalScore descending
                           select new
                           {
                               u.Username,
                               s.TotalScore
                           }).Take(10).ToList();
            }
            catch (Exception)
            {
                throw new Exception($"Getting top {topNumber} players failed");
            }

        }

        public async Task<bool> UpdateScore(Score score)
        {
            try
            {
                _context.Entry(score).State = EntityState.Modified;
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                throw new Exception($"Updating score {score.Id} failed on save");
            }
        }

        public async Task DeleteScore(int id)
        {
            try
            {
                Score score = await _context.Scores.FindAsync(id);
                _context.Scores.Remove(score);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception($"Failed to delete score id: {id}");
            }
        }

    }
}