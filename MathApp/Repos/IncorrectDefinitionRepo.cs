using API.Enteties;
using API.Interfaces;
using MathApp.Enteties;
using Microsoft.EntityFrameworkCore;

namespace API.Repos
{
    public class IncorrectDefinitionRepo : IIncorrectDefinitionRepo
    {
        private readonly DataBase _context;

        public IncorrectDefinitionRepo(DataBase context)
        {
            _context = context;
        }
       public async Task<IEnumerable<IncorrectDefinition>> GetAllIncorrect()
        {
            var incorrect = await _context.IncorrectDefinitions.ToListAsync();
            return incorrect;
            //throw new NotImplementedException();
        }

       public async Task<IncorrectDefinition> GetIncorrectByID(int id)
        {
            var incorrect = await _context.IncorrectDefinitions.FindAsync(id);
            return incorrect;
        }

        public async   Task<IncorrectDefinition> GetIncorrectByContent(string s)
        {
            var incorrect = await _context.IncorrectDefinitions.ToListAsync();
            foreach(var inc in incorrect)
            {
                if(inc.Content==s)
                {
                    return inc;
                }
            }
            return null;
        }

        public async Task<IEnumerable<IncorrectDefinition>> GetIncorrectsByDefinition(int definitionId)
        {
            var pair = await _context.DefIncPair.ToListAsync();
            var incorrect = new List<IncorrectDefinition>();

            foreach (var p in pair)
            {
                if(p.DefinitionId== definitionId)
                {
                    var inc=GetIncorrectByID(p.IncorrectDefinitionId).Result;
                    incorrect.Add(inc);
                }
            }
            return incorrect;
        }

        public async Task<IEnumerable<DefIncPair>> GetAllPairs()
        {
            var pairs = await _context.DefIncPair.ToListAsync();
            return pairs;
        }
        public async Task<IEnumerable<DefIncPair>> GetPairsByDefinition(int definitionID)
        {
            var pairs = await _context.DefIncPair.ToListAsync();
            var response = new List<DefIncPair>();
            foreach (var pair in pairs)
            {
                if(pair.DefinitionId== definitionID)
                    response.Add(pair);
            }
            return response;
        }

        public async Task AddIncorrect(string contents)
        {
            await _context.IncorrectDefinitions.AddAsync(new IncorrectDefinition { Content= contents });
            await _context.SaveChangesAsync();
        }
        public async Task AddIncorrect(IncorrectDefinition def)
        {
            await _context.IncorrectDefinitions.AddAsync(def);
            await _context.SaveChangesAsync();
        }
        public async Task AddPair(int defId, int incId)
        {
            await _context.DefIncPair.AddAsync(new DefIncPair { DefinitionId = defId, IncorrectDefinitionId = incId });
            await _context.SaveChangesAsync();
        }

        public async Task DeletePair(int defId, int incId)
        {
            var pairs = _context.DefIncPair.ToListAsync().Result;
            foreach(var pair in pairs)
            {
                if (pair.IncorrectDefinitionId == incId && pair.DefinitionId == defId)
                {
                    _context.Remove<DefIncPair>(pair);
                    await _context.SaveChangesAsync();
                }
            }
        }

    }
}
