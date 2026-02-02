using MathApp.Enteties;
using MathEducationWebApp.Components.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MathEducationWebApp.Components.Nowy_folder
{
    public class DefinitionRepo : IDefinitionRepo
    {
        private readonly DataBase _context;

        public DefinitionRepo(DataBase context) 
        {
            _context=context;
        }

        public async Task AddDefinition(Definition definition)
        {
            await _context.Definitions.AddAsync(definition);
            _context.SaveChanges();
        }

        public async Task AddDefinition(string name, string type, string part1, string part2, int unitId)
        {
            await _context.Definitions.AddAsync(new Definition { 
                Name = name, 
                Type = type,
                Part1 = part1, 
                Part2 = part2, 
                unitId = unitId });
            _context.SaveChanges();
        }

        public async Task AddDefinitions(IEnumerable<Definition> definitions)
        {
            await _context.Definitions.AddRangeAsync(definitions);
            _context.SaveChanges();
        }

        public async Task EditDefinition(int Id, string name, string type, string p1, string p2, int unitID)
        {
            var un = await _context.Definitions.Where(def => def.Id == Id).ExecuteUpdateAsync(setters => setters
            .SetProperty(def => def.Name, name)
            .SetProperty(def => def.Type, type)
            .SetProperty(def => def.Part1, p1)
            .SetProperty(def => def.Part2, p2)
            .SetProperty(def=>def.unitId, unitID));

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Definition>> GetAllDefinitions()
        {
            var definitions = await _context.Definitions.ToListAsync();
            return definitions;
        }

        public Definition? GetDefinitionbyId(int Id)
        {
            var definition = _context.Definitions.Find(Id);
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

        public async Task RemoveDefinition(Definition definition)
        {
            _context.Remove<Definition>(definition);
            _context.SaveChanges();
        }

        public async Task RemoveDefinition(int Id)
        {
            var Definitions = await _context.Definitions.ToListAsync();
            for (int i = 0; i < Definitions.Count; i++)
            {
                if (Definitions[i].Id == Id)
                {
                    _context.Remove<Definition>(Definitions[i]);
                    _context.SaveChanges();
                    return;
                }
            }
        }
        public async Task RemoveDefinition(string name)
        {
            var definition = GetDefinitionbyName(name).Result;

            _context.Remove<Definition>(definition);
            _context.SaveChanges();
        }
    }
}
