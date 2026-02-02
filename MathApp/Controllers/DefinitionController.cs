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
    public class DefinitionController : ControllerBase
    {
        private readonly IDefinitionRepo _definitionRepo;
        private readonly IUnitRepo _unitRepo;

        public DefinitionController(IDefinitionRepo definitionRepo, IUnitRepo unitRepo)
        {
            _definitionRepo = definitionRepo;
            _unitRepo = unitRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DefinitionDTO>>> GetDefinitions()
        {
            try
            {
                var definitions = await _definitionRepo.GetAllDefinitions();

                if (definitions == null)
                {
                    return NotFound();
                }

                var definitonsDTO = new List<DefinitionDTO>();

                foreach (var definition in definitions)
                {
                    string? unitname = _unitRepo.GetUnitByID(definition.unitId).Result.name.ToString();
                    var def = new DefinitionDTO() {
                        ID = definition.Id,
                        name = definition.Name,
                        type = definition.Type,
                        part1 = definition.Part1,
                        part2 = definition.Part2,
                        UnitName = unitname
                    };
                    definitonsDTO.Add(def);
                }

                return Ok(definitonsDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;

            }
        }

        [HttpGet("DefinitionByUnit/{unitName}")]
        public async Task<ActionResult<IEnumerable<DefinitionDTO>>> GetDefinitionsByUnit([FromRoute] string unitName)
        {
            try
            {
                var unit = await _unitRepo.GetUnitByName(unitName);
                if (unit == null)
                {
                    return NotFound();
                }

                var definitions = await _definitionRepo.GetDefinitionsByUnit(unit);

                if (definitions == null)
                {
                    return NotFound();
                }

                var definitonsDTO = new List<DefinitionDTO>();

                foreach (var definition in definitions)
                {
                    var def = new DefinitionDTO()
                    {
                        ID = definition.Id,
                        name = definition.Name,
                        type = definition.Type,
                        part1 = definition.Part1,
                        part2 = definition.Part2,
                        UnitName = unitName
                    };
                    definitonsDTO.Add(def);
                }

                return Ok(definitonsDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;

            }
        }


        [HttpPost]
        public async Task<ActionResult<DefinitionDTO>> AddDefinition([FromBody] DefinitionDTO definition)
        {
            var unit = _unitRepo.GetUnitByName(definition.UnitName).Result;
            if (unit == null)
            {
                return NotFound(); 
            }
            var def = new Definition() { 
                Name = definition.name,
                Type = definition.type,
                Part1 = definition.part1,
                Part2 = definition.part2, 
                unitId = unit.Id };
            await _definitionRepo.AddDefinition(def);
            return CreatedAtAction(nameof(GetDefinitions), new { id = def.Id }, def);
        }

        [HttpDelete("Delete/{unitname}")]
        public void DeletDefinition([FromRoute] string unitname)
        {
            _definitionRepo.RemoveDefinition(unitname);

        }
        [HttpPost("Update")]
        public async Task UpdateUnit([FromBody] DefinitionDTO definition)
        {
            var unit = _unitRepo.GetUnitByName(definition.UnitName).Result;
            if (unit == null)
            {
                return;
            }

            await _definitionRepo.EditDefinition(
                definition.ID,
                definition.name,
                definition.type,
                definition.part1,
                definition.part2,
                unit.Id);
        }
    }
}
 