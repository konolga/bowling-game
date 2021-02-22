using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ServerAPI.Data;
using ServerAPI.Infrastructure;

namespace ServerAPI.Data
{
    public class UsersRepo : IUsersRepo
    {
        private readonly DataContext _context;
        public UsersRepo(DataContext context)
        {
            _context = context;
        }
        public async Task CreateUser(User user)
        {

            try
            {
                var reply = await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception($"Adding user {user.Username} failed on save");
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



        public async Task DeleteUser(string id)
        {
            try
            {
                User user = await _context.Users.FindAsync(id);
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                throw new Exception($"Failed to get user username: {username}");
            }
        }
    }
}
