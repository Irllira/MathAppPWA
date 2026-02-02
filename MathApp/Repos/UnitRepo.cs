using MathApp.Enteties;
using MathEducationWebApp.Components.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MathEducationWebApp.Components.Nowy_folder
{
    public class UnitRepo : IUnitRepo
    {
        private readonly DataBase _context;

        public UnitRepo(DataBase context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Unit>> GetAllUnit()
        {
            var Units = await _context.Units.ToListAsync();
            return Units;
        }

        public async Task<Unit>? GetUnitByID(int ID)
        {
            var unit = await _context.Units.FindAsync(ID);
            return unit;
        }

        public async Task<Unit>? GetUnitByName(string Name)
        {
            var units = await _context.Units.ToListAsync();
            foreach(var un in units)
            {
                if(un.name == Name)
                    return un;
            }      
            return null;
        }

        public async Task AddUnit(Unit unit)
        {
            await _context.Units.AddAsync(unit);
            _context.SaveChanges();
        }

        public async Task  AddUnit(string name, string? description, int EducationID, List<Definition>? definitions)
        {
            await _context.Units.AddAsync(new Unit { name = name, description = description, educationLevelId = EducationID, definitions = definitions });
            _context.SaveChanges();
        }

        public async Task AddUnits(IEnumerable<Unit> units)
        {
            await _context.Units.AddRangeAsync(units);
            _context.SaveChanges();
        }
        
        public async Task RemoveUnit(int ID)
        {
            var Units = await _context.Units.ToListAsync();
            for (int i = 0; i < Units.Count; i++)
            {
                if (Units[i].Id == ID)
                {
                    _context.Remove<Unit>(Units[i]);
                    _context.SaveChanges();
                    return;
                }
            }
        }

        public async Task RemoveUnitByName(string name)
        {
            var unit = await GetUnitByName(name);
           
            _context.Remove<Unit>(unit);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Unit>> GetUnitsbyEdLevel(int edLevID)
        {
            var Units = await _context.Units.ToListAsync();
            var resoults= new List<Unit>();

            foreach(var u in Units)
            {
                if(u.educationLevelId == edLevID)
                    resoults.Add(u);
            }

            return resoults;
        }

        public async Task EditUnit(int Id, string name, string? description, int EducationID)
        {
           // var un= GetUnitByID(Id).Result;
            var un = await _context.Units.Where(unit => unit.Id == Id).ExecuteUpdateAsync(setters => setters
            .SetProperty(unit => unit.name, name)
            .SetProperty(unit => unit.description, description)
            .SetProperty(unit => unit.educationLevelId, EducationID));//Update(un);
            
 
            await _context.SaveChangesAsync();

            
        }
    }
}
