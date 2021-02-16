using System;

namespace ServerAPI.Infrastructure
{
    public class Score
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TotalScore { get; set; }
        public DateTime CreationTime { get; set; }
    }
}