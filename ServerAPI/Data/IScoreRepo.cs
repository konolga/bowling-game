using System.Threading.Tasks;
using ServerAPI.Infrastructure;
using System.Collections.Generic;

namespace ServerAPI.Data
{
    public interface IScoreRepo
    {
        Task<Score> AddScore(Score score);
        Task<List<Score>> GetScoreByUserId(int id);
        Task DeleteScore(int id);
    }
}