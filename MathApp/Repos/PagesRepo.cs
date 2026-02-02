using API.Enteties;
using API.Interfaces;
using MathApp.Enteties;
using Microsoft.EntityFrameworkCore;

namespace API.Repos
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

        public async Task AddPage(Pages page)
        {
            await _context.Pages.AddAsync(page);
            _context.SaveChanges();
        }

        public async Task UpdatePage(int id, string name ,string link, int unitId, string? description )
        {
            var pages = await _context.Pages.Where(pg => pg.Id == id).ExecuteUpdateAsync(setters => setters
            .SetProperty(pg => pg.Name, name)
            .SetProperty(pg => pg.link, link)
            .SetProperty(pg =>pg.Description, description)
            .SetProperty(pg => pg.UnitID, unitId));

            await _context.SaveChangesAsync();
        }

        public async Task DeletePage(int id)
        {
            var Pages = await _context.Pages.ToListAsync();
            for (int i=0;i<Pages.Count();i++)
            {
                if (Pages[i].Id == id)
                {
                    _context.Remove<Pages>(Pages[i]);
                    _context.SaveChanges();
                    return;
                }

            }
        }
    }
}
