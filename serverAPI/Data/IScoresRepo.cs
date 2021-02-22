using System.Threading.Tasks;
using ServerAPI.Infrastructure;
using System.Collections.Generic;

namespace ServerAPI.Data
{
    public interface IScoresRepo
    {
        Task AddScore(Score score);
        IEnumerable<object> GetTopResults(int topNumber);
        Task<List<Score>> GetScoreByUserId(int id);
        Task DeleteScore(int id);
    }
}