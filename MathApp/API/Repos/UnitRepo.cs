using MathApp.Backend.API.Interfaces;
using MathApp.Backend.Data.Enteties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MathApp.Backend.API.Repos
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

        public async Task<Unit> AddUnit(Unit unit)
        {
            await _context.Units.AddAsync(unit);
            _context.SaveChanges();
            return unit;
        }

        public async Task<Unit> AddUnit(string name, string? description, int EducationID, List<Definition>? definitions)
        {
            var unit = new Unit { name = name, description = description, educationLevelId = EducationID, definitions = definitions };
            await _context.Units.AddAsync(unit);
            _context.SaveChanges();

            return unit;
        }
/*
        public async Task<IEnumerable<Unit>> AddUnits(IEnumerable<Unit> units)
        {
            await _context.Units.AddRangeAsync(units);
            _context.SaveChanges();
            return units;
        }
  */      
        public async Task<bool> RemoveUnit(int ID)
        {
            var Units = await _context.Units.ToListAsync();
            for (int i = 0; i < Units.Count; i++)
            {
                if (Units[i].Id == ID)
                {
                    _context.Remove(Units[i]);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> RemoveUnitByName(string name)
        {
            var unit = await _context.Units.FirstOrDefaultAsync(un => un.name == name);

            if (unit!=null)
            {
               // var unit = await GetUnitByName(name);

                _context.Remove(unit);
                await _context.SaveChangesAsync();
                return true;
            }else
            {
                return false;
            }
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

        public async Task<Unit> EditUnit(int Id, string name, string? description, int EducationID)
        {
            var unit = await _context.Units.FirstOrDefaultAsync(un => un.Id == Id);
            if (unit == null)
            {
                return null;
            }

            unit.name = name;
            unit.description = description;
            unit.educationLevelId = EducationID;
            
            await _context.SaveChangesAsync();
            
            return new Unit() {Id= Id,name= name, description=description,educationLevelId= EducationID };
        }
    }
}
