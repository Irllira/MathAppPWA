using DTO.DTOs;
using MathApp.Enteties;
using MathEducationWebApp.Components.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MathEducationWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationLevelController : ControllerBase
    {
        private readonly IEducationLevelRepo _edLevelRepo;

        public EducationLevelController(IEducationLevelRepo edLevelRepo) 
        { 
            _edLevelRepo = edLevelRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EducationLevelDTO>>> GetEducationLevels()
        {
            try
            {
                var edLevel = await _edLevelRepo.GetAllEducationLevels();

                if (edLevel == null)
                {
                    return NotFound();
                }
                var edLvlDTO = new List<EducationLevelDTO>();
                foreach (var edlv in edLevel)
                {
                    var lvl = new EducationLevelDTO() { name = edlv.name };
                    edLvlDTO.Add(lvl);
                }

                return Ok(edLvlDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

    //    [HttpPost]
       // public as
    }
}
