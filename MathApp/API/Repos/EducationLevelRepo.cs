using MathApp.Backend.API.Interfaces;
using MathApp.Backend.Data.Enteties;

using Microsoft.EntityFrameworkCore;

namespace MathApp.Backend.API.Repos
{
    public class EducationLevelRepo : IEducationLevelRepo
    {
        private readonly DataBase _context;

        public EducationLevelRepo(DataBase context) 
        {
            _context = context;

        }
        public async Task<IEnumerable<EducationLevel>> GetAllEducationLevels()
        {
            var levels = await _context.educationLevels.ToListAsync();
            return levels;
        }
        public async Task<EducationLevel>? GetEducationLevelByID(int IDNumber)
        {
            var level = await _context.educationLevels.FindAsync(IDNumber);        
            return level;
        }
        public async Task<EducationLevel>? GetEducationLevelsbyName(string name)
        {
            // var level = await _context.educationLevels.FindAsync(name);
            var levels = await _context.educationLevels.ToListAsync();
            if (levels == null)
                return null;
            int id=-1;

            foreach(var lvl in levels)
            {
                if (lvl.name == name)
                {
                    id = lvl.Id;
                    break;
                }

            }
            if(id==-1)      
                return null;
            
            var level = await _context.educationLevels.FindAsync(id);
            return level;
        }

        public async Task<string>? GetEducationLevelName(int IDNumber)
        {
            var level = await _context.educationLevels.FindAsync(IDNumber);
            if (level == null)
                return null;
            return level.name;
        }
    }
}
