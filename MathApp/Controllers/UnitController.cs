using DTO.DTOs;
using MathApp.Enteties;
using MathEducationWebApp.Components.Interfaces;
using MathEducationWebApp.Components.Nowy_folder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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
                foreach (var unit in units) {
                    var edLvlName = _edLevelRepo.GetEducationLevelByID(unit.educationLevelId).Result;
                    UnitDTO un = new UnitDTO()
                    {
                        ID = unit.Id,
                        name = unit.name,
                        description = unit.description,
                        educationLevel = edLvlName.name.ToString()
                    };
                    unitsDTO.Add(un);
                }


                return Ok(unitsDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
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
                    // var edLvlName = _edLevelRepo.GetEducationLevelByID(unit.educationLevelId).Result;
                    UnitDTO un = new UnitDTO()
                    {
                        ID = unit.Id,
                        name = unit.name,
                        description = unit.description,
                        educationLevel = edLvlName//.name.ToString()
                    };
                    unitsDTO.Add(un);
                }


                return Ok(unitsDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        [HttpGet("ByName/{name}")]
        public async Task<ActionResult<IEnumerable<UnitDTO>>> GetUnitByName([FromRoute] string name)
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
                throw;
            }
        }



        [HttpPost]
        public async Task<ActionResult<AccountsPasswordsDTO>> AddUnit([FromBody] UnitDTO unit)
        {
            var edlvl = _edLevelRepo.GetEducationLevelsbyName(unit.educationLevel).Result;
            var un = new Unit() { name = unit.name, description = unit.description, educationLevelId = edlvl.Id};
            await _unitRepo.AddUnit(un);
            return CreatedAtAction(nameof(GetUnits), new { id = un.Id }, un);
        }

        [HttpDelete ("Delete/{unitname}")]
        public async Task DeleteUnit([FromRoute] string unitname)
        {
          
            await _unitRepo.RemoveUnitByName(unitname);

        }

        [HttpPost("Update")]
        public async Task EditUnit([FromBody] UnitDTO unit)
        {
            var edLvl = await _edLevelRepo.GetEducationLevelsbyName(unit.educationLevel);
            if (edLvl == null)
            {
                return;
            }
          
            await _unitRepo.EditUnit(unit.ID, unit.name, unit.description, edLvl.Id); //edLvl.Id); 
        }
    }
}
