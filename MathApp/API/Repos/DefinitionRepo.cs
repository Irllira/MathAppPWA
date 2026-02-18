using MathApp.Backend.API.Interfaces;
using MathApp.Backend.Data.Enteties;
using Microsoft.EntityFrameworkCore;

namespace MathApp.Backend.API.Repos
{
    public class DefinitionRepo : IDefinitionRepo
    {
        private readonly DataBase _context;

        public DefinitionRepo(DataBase context) 
        {
            _context=context;
        }

        public async Task<Definition> AddDefinition(Definition definition)
        {
            var defs = await _context.Definitions.FirstOrDefaultAsync(def => def.Id == definition.Id);
            if (defs != null)
            {
                return null;
            }

            await _context.Definitions.AddAsync(definition);
            _context.SaveChanges();

            return definition;
        }

        public async Task<Definition> AddDefinition(string name, string type, string part1, string part2, int unitId)
        {
            var definition = new Definition { Name = name, Type = type, Part1 = part1, Part2 = part2, unitId = unitId };

            await _context.Definitions.AddAsync(definition);
            _context.SaveChanges();

            return definition;
        }
        public async Task<bool> EditDefinition(int Id, string name, string type, string p1, string p2, int unitID)
        {
            var defs = await _context.Definitions.FirstOrDefaultAsync(def => def.Id == Id);
            if (defs == null)
            {
                return false;
            }

            defs.Name = name;
            defs.Type = type;
            defs.Part1 = p1;
            defs.Part2 = p2;
            defs.unitId = unitID;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Definition>> GetAllDefinitions()
        {
            var definitions = await _context.Definitions.ToListAsync();
            return definitions;
        }

        public async Task<Definition> GetDefinitionbyId(int Id)
        {
            var definition = await _context.Definitions.FindAsync(Id);
            return definition;
        }

        public async Task<Definition> GetDefinitionbyName(string Name)
        {
            var definitions = await _context.Definitions.ToListAsync();
            foreach (var def in definitions) {
                if (Name == def.Name)
                {
                    return def;
                }
            }  
            return null;
        }

        public async Task<IEnumerable<Definition>> GetDefinitionsByUnit(int unitID)
        {
            var definitions = await _context.Definitions.ToListAsync();
            List<Definition> results = new List<Definition>();
            for(int i = 0;i<definitions.Count();i++)
            {
                if (definitions[i].unitId == unitID)
                    results.Add(definitions[i]);
            }
            return results;
        }
        public async Task<IEnumerable<Definition>> GetDefinitionsByUnit(Unit unit)
        {
            var definitions = await _context.Definitions.ToListAsync();
            List<Definition> results = new List<Definition>();
            for (int i = 0; i < definitions.Count(); i++)
            {
                if (definitions[i].unitId == unit.Id)
                    results.Add(definitions[i]);
            }
            return results;
        }

        public async Task<bool> RemoveDefinition(Definition definition)
        {
            var defs = await _context.Definitions.ToListAsync();
            if (defs.Contains(definition))
            {
                _context.Remove(definition);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public async Task<bool> RemoveDefinition(int Id)
        {
            var Definitions = await _context.Definitions.ToListAsync();
            for (int i = 0; i < Definitions.Count; i++)
            {
                if (Definitions[i].Id == Id)
                {
                    _context.Remove(Definitions[i]);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
