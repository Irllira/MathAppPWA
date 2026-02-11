using API.Enteties;
using API.Interfaces;
using DTO.DTOs;
using MathApp.Enteties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagesController : ControllerBase
    {
        private readonly IPagesRepo _pagesRepo;

        public PagesController(IPagesRepo pagesRepo)
        {
            _pagesRepo = pagesRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PagesDTO>>> GetPages()
        {
            try
            {
                var pages = await _pagesRepo.GetAllPages();

                if (pages == null)
                {
                    return NotFound();
                }

                var pagesDTO = new List<PagesDTO>();
                foreach (var page in pages)
                {
                    PagesDTO pg = new PagesDTO()
                    {
                        Id = page.Id,
                        Name = page.Name,
                        //Description = page.Description,
                        link = page.Link,
                        UnitID = page.UnitID
                    };
                    pagesDTO.Add(pg);
                }
                return Ok(pagesDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet ("ByUnitID/{UnitID}")]
        public async Task<ActionResult<IEnumerable<PagesDTO>>> GetPagesByUnitID([FromRoute]int UnitID)
        {
            try
            {
                var pages = await _pagesRepo.GetPagesByUnit(UnitID);

                if (pages == null)
                {
                    return NotFound();
                }

                var pagesDTO = new List<PagesDTO>();
                foreach (var page in pages)
                {
                    PagesDTO pg = new PagesDTO()
                    {
                        Id = page.Id,
                        Name = page.Name,
                       // Description = page.Description,
                        link = page.Link,
                        UnitID = page.UnitID
                    };
                    pagesDTO.Add(pg);
                }
                return Ok(pagesDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpPost]
        public async Task<ActionResult<PagesDTO>> AddPage([FromBody] PagesDTO page)
        {
            var p = new Pages() { Link=page.link, Name = page.Name, UnitID = page.UnitID};
            await _pagesRepo.AddPage(p);
            return CreatedAtAction(nameof(GetPages), new { id = p.Id }, p);
        }

        [HttpPost ("UpdatePage")]
        public async Task UpdatePage([FromBody] PagesDTO page)
        {
            //var p = new Pages() { link = page.link, Name = page.Name, UnitID = page.UnitID };
            await _pagesRepo.UpdatePage(page.Id,page.Name,page.link,page.UnitID, page.Description);         
        }

        [HttpDelete ("DeletePage/{pageId}")]
        public async Task DeletePage(int pageId)
        {
            await _pagesRepo.DeletePage(pageId);
        }
    }
}
