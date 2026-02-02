using API.Enteties;
using API.Interfaces;
using MathApp.Enteties;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace API.Repos
{
    public class UserProgressRepo: IUserProgressRepo
    {
        private readonly DataBase _context;

        public UserProgressRepo(DataBase context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserProgress>>? GetUserProgress()
        {
            var userProgresses = await _context.UserProgresses.ToListAsync();

            return userProgresses;
        }
        public async Task<IEnumerable<UserProgress>>? GetUserProgressByUserId(int userID)
        {
            var uProg = await _context.UserProgresses.ToListAsync();

            List<UserProgress> userProgresses = new List<UserProgress>();

            foreach (var u in uProg)
            {
                if(u.AccountId == userID)
                {
                    userProgresses.Add(u);      
                }
            }
            return userProgresses;
        }

        public async Task<UserProgress>? GetUserProgressByUserIdUnitIdType(int userId,int unitId,string type)
        {
            var uProg = await _context.UserProgresses.ToListAsync();

            foreach (var u in uProg)
            {
                if (u.AccountId == userId && u.UnitId==unitId && u.type == type)
                {
                    return u;
                }
            }
            return null;
        }

        public async Task AddProgress(UserProgress userProgress)
        {
            await _context.AddAsync(userProgress);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProgress(int id, string type, int all, int good)
        {
            var pages = await _context.UserProgresses.Where(up => up.Id == id).ExecuteUpdateAsync(setters => setters
            .SetProperty(up => up.type, type)
            .SetProperty(pg => pg.all, all)
            .SetProperty(pg => pg.good, good));

            await _context.SaveChangesAsync();
        }
    }
}
