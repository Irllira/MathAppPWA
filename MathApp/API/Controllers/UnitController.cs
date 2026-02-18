using DTO.DTOs;
using MathApp.Backend.API.Interfaces;
using MathApp.Backend.Data.Enteties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MathApp.Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitRepo _unitRepo;
        private readonly IEducationLevelRepo _edLevelRepo;

        public UnitController(IUnitRepo unitRepo, IEducationLevelRepo edLevelRepo) 
        { 
            _unitRepo = unitRepo;
            _edLevelRepo = edLevelRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnitDTO>>> GetUnits()
        {
            try
            {
                var units = await _unitRepo.GetAllUnit();

                if (units == null)
                {
                    return NotFound();
                }

                var unitsDTO = new List<UnitDTO>();
                foreach (var unit in units)
                {
                    var edLvlName = await _edLevelRepo.GetEducationLevelByID(unit.educationLevelId);
                    if (edLvlName != null)
                    {

                        UnitDTO un = new UnitDTO()
                        {
                            ID = unit.Id,
                            name = unit.name,
                            description = unit.description,
                            educationLevel = edLvlName.name.ToString()
                        };
                        unitsDTO.Add(un);
                    }
                }
                return Ok(unitsDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        [HttpGet ("ByEdLvl/{edLvlName}")]
        public async Task<ActionResult<IEnumerable<UnitDTO>>> GetUnitsByEdLvls([FromRoute]string edLvlName)
        {
            try
            {
                var edLvl = await _edLevelRepo.GetEducationLevelsbyName(edLvlName);
                if (edLvl == null)
                {
                    return NotFound();
                }

                var units = await _unitRepo.GetUnitsbyEdLevel(edLvl.Id);

                if (units == null)
                {
                    return NotFound();
                }

                var unitsDTO = new List<UnitDTO>();
                foreach (var unit in units)
                {
                    UnitDTO un = new UnitDTO()
                    {
                        ID = unit.Id,
                        name = unit.name,
                        description = unit.description,
                        educationLevel = edLvlName
                    };
                    unitsDTO.Add(un);
                }


                return Ok(unitsDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();

            }
        }


        [HttpGet("ByName/{name}")]
        public async Task<ActionResult<UnitDTO>> GetUnitByName([FromRoute] string name)
        {
            try
            {
                var unit = await _unitRepo.GetUnitByName(name);

                if (unit == null)
                {
                    return NotFound();
                }

                var edLvlName = await _edLevelRepo.GetEducationLevelName(unit.educationLevelId);
                if (edLvlName == null)
                {
                    return NotFound();
                }
                UnitDTO unitDTO = new UnitDTO()
                {
                    ID = unit.Id,
                    name = unit.name,
                    description = unit.description,
                    educationLevel = edLvlName
                };

                return Ok(unitDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        [HttpPost]
        public async Task<ActionResult<UnitDTO>> AddUnit([FromBody] UnitDTO unit)
        {
            try
            {
                var edlvl = await _edLevelRepo.GetEducationLevelsbyName(unit.educationLevel);
                if(edlvl == null)
                    return NotFound();

                var un = new Unit() { name = unit.name, description = unit.description, educationLevelId = edlvl.Id };
                var res = await _unitRepo.AddUnit(un);
                if(res == null)
                    return NotFound();

                var resDTO = new UnitDTO() { educationLevel = edlvl.name, ID = res.Id, name = res.name, description = res.description };

                return CreatedAtAction(nameof(GetUnits), new { id = resDTO.ID }, resDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }
        [HttpPost("Update")]
        public async Task<ActionResult<UnitDTO>> EditUnit([FromBody] UnitDTO unit)
        {
            try
            {
                var edLvl = await _edLevelRepo.GetEducationLevelsbyName(unit.educationLevel);
                if (edLvl == null)
                {
                    return NotFound();
                }

                var un = await _unitRepo.EditUnit(unit.ID, unit.name, unit.description, edLvl.Id);
                if (un == null)
                    return NotFound();

                var resDTO = new UnitDTO() { educationLevel = edLvl.name, ID = un.Id, name = un.name, description = un.description };

                return CreatedAtAction(nameof(GetUnits), new { id = resDTO.ID }, resDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        [HttpDelete("Delete/{unitname}")]
        public async Task<ActionResult> DeleteUnit([FromRoute] string unitname)
        {
            try
            {
                var res = await _unitRepo.RemoveUnitByName(unitname);
                if(res == false)
                    return NotFound();

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
