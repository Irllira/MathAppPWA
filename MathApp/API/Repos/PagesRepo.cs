using API.Enteties;
using MathApp.Backend.API.Interfaces;
using MathApp.Backend.Data.Enteties;

using Microsoft.EntityFrameworkCore;

namespace MathApp.Backend.API.Repos
{
    public class PagesRepo : IPagesRepo
    {
        private readonly DataBase _context;

        public PagesRepo(DataBase context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Pages>> GetAllPages()
        {
            var pages = await _context.Pages.ToArrayAsync();
            return pages;
        }

        public async Task<IEnumerable<Pages>> GetPagesByUnit(int unitID)
        {
            var pages = await _context.Pages.ToArrayAsync();
            var response = new List<Pages>();
            foreach (var page in pages)
            {
                if(page.UnitID == unitID)
                    response.Add(page);
            }
            return response;
        }

        public async Task<Pages> AddPage(Pages page)
        {
            await _context.Pages.AddAsync(page);
            _context.SaveChanges();
            return page; 
        }

        public async Task<Pages> UpdatePage(int id, string name ,string link, int unitId )
        {
            var page = await _context.Pages.FirstOrDefaultAsync(pg => pg.Id == id);
            if (page == null)
            {
                return null;
            }
            page.Name = name;
            page.Link = link;
            page.UnitID = unitId;

            await _context.SaveChangesAsync();
            return new Pages() { Link = link, Name = name, Id = id, UnitID = unitId };
        }

        public async Task<bool> DeletePage(int id)
        {
            var Pages = await _context.Pages.ToListAsync();
            for (int i=0;i<Pages.Count();i++)
            {
                if (Pages[i].Id == id)
                {
                    _context.Remove(Pages[i]);
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
