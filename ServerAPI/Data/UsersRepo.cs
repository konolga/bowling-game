using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServerAPI.Data;
using ServerAPI.Infrastructure;

namespace ServerAPI.Data
{
    [ApiController]
    [Route("[users]")]
    public class UserRepo : IUsersRepo
    {
        private readonly DataContext _context;
        public UserRepo(DataContext context)
        {
            _context = context;
        }
        public async Task CreateUser(User user)
        {

            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception($"Updating user {user.Id} failed on save");
            }
        }

        public async Task<bool> UpdateUser(User user)
        {
            try
            {
                _context.Entry(user).State = EntityState.Modified;
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                throw new Exception($"Updating user {user.Id} failed on save");
            }
        }

        public async Task<List<User>> GetTopUsers(int topNumber)
        {
            try
            {
                return await _context.Users
                .Join(_context.Scores
                .OrderByDescending(score => score.TotalScore)
                .Take(topNumber)
                .Select(score => score.UserId)
                .ToList(), u => u.Id, id => id, (u, id) => u)
                .ToListAsync();
            }
            catch
            {
                throw new Exception($"Getting top {topNumber} players failed");
            }

        }

        public async Task DeleteUser(string id)
        {
            try
            {
                User user = await _context.Users.FindAsync(id);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception($"Failed to delete user id: {id}");
            }
        }

        public async Task<User> GetUserByUsername(string username)
        {
            try
            {
               return await _context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();
            }
            catch
            {
                throw new Exception($"Failed to get user username: {username}");
            }
        }
    }
}
