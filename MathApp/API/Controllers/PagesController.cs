using API.Enteties;
using DTO.DTOs;
using MathApp.Backend.API.Interfaces;
using MathApp.Backend.Data.Enteties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MathApp.Backend.API.Controllers
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
                return BadRequest();
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
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<PagesDTO>> AddPage([FromBody] PagesDTO page)
        {
            try
            {
                var p = new Pages() { Link = page.link, Name = page.Name, UnitID = page.UnitID };
                var res = await _pagesRepo.AddPage(p);
                if (res == null)
                    return NotFound();

                PagesDTO pagesDTO = new PagesDTO() { link = res.Link, Name = res.Name, UnitID =res.UnitID, Id = res.Id };

                return CreatedAtAction(nameof(GetPages), new { id = pagesDTO.Id }, pagesDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        [HttpPost("UpdatePage")]
        public async Task<ActionResult<PagesDTO>> UpdatePage([FromBody] PagesDTO page)
        {
            try
            {
                var res = await _pagesRepo.UpdatePage(page.Id, page.Name, page.link, page.UnitID);
                if(res ==null)
                    return NotFound();

                PagesDTO pagesDTO = new PagesDTO() { link = res.Link, Name = res.Name, UnitID = res.UnitID, Id = res.Id };
                return CreatedAtAction(nameof(GetPages), new { id = pagesDTO.Id }, pagesDTO);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        [HttpDelete("DeletePage/{pageId}")]
        public async Task<ActionResult> DeletePage(int pageId)
        {
            try
            {
                var res = await _pagesRepo.DeletePage(pageId);
                if(res == false)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }
    }
}
