using MathApp.Backend.API.Interfaces;
using MathApp.Backend.Data.Enteties;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace MathApp.Backend.API.Repos
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

        public async Task<UserProgress> AddProgress(UserProgress userProgress)
        {
            await _context.AddAsync(userProgress);
            await _context.SaveChangesAsync();
            return userProgress;
        }

        public async Task<UserProgress> UpdateProgress(int id, string type, int all, int good)
        {
            var prog = await _context.UserProgresses.FirstOrDefaultAsync(pg => pg.Id == id);
            if (prog == null)
            {
                return null;
            }
            prog.type = type;
            prog.all = all;
            prog.good = good;
            
            await _context.SaveChangesAsync();

            return new UserProgress() { type = type, Id = id, all = all, good = good };
        }
    }
}
