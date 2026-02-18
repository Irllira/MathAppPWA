using API.Enteties;
using DTO.DTOs;
using MathApp.Backend.API.Interfaces;
using MathApp.Backend.Data.Enteties;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MathApp.Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncorrectController : ControllerBase
    {
        private readonly IDefinitionRepo _definitionRepo;
        private readonly IIncorrectDefinitionRepo _incorrectRepo;

        public IncorrectController(IDefinitionRepo definitionRepo, IIncorrectDefinitionRepo incorrectRepo)
        {
            _definitionRepo = definitionRepo;
            _incorrectRepo = incorrectRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IncorrectDTO>>> GetAllIncorrect()
        {
            try
            {
                var incorrect = await _incorrectRepo.GetAllIncorrect();
                if (incorrect == null)
                {
                    return NotFound();
                }

                var incorrectDTO = new List<IncorrectDTO>();

                foreach (var incor in incorrect)
                {
                    var i = new IncorrectDTO()
                    {
                        Id = incor.Id,
                        content = incor.Content
                    };
                    incorrectDTO.Add(i);
                }

                return Ok(incorrectDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        [HttpGet("ById/{id}")]
        public async Task<ActionResult<IEnumerable<IncorrectDTO>>> GetByID([FromRoute] int id)
        {
            try
            {
                var incorrect = await _incorrectRepo.GetIncorrectByID(id);
                if (incorrect == null)
                {
                    return NotFound();
                }

                var incorrectDTO = new IncorrectDTO()
                {
                    Id = incorrect.Id,
                    content = incorrect.Content
                };
                return Ok(incorrectDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e); 
                return BadRequest();
            }
        }

        [HttpGet("ByContent/{content}")]
        public async Task<ActionResult<IEnumerable<IncorrectDTO>>> GetByContent([FromRoute] string content)
        {
            try
            {
                string real = content;
                if (content.Length !=1)
                {
                    real = System.Web.HttpUtility.UrlDecode(content);

                }
                var incorrect = await _incorrectRepo.GetIncorrectByContent(real);
                if (incorrect == null)
                {
                    return NotFound();
                }

                var incorrectDTO = new IncorrectDTO()
                {
                    Id = incorrect.Id,
                    content = incorrect.Content
                };


                return Ok(incorrectDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        [HttpGet("ByDefinition/{definitionId}")]
        public async Task<ActionResult<IEnumerable<IncorrectDTO>>> GetByDefinition([FromRoute] int definitionId)
        {
            try
            {
                var incorrect = await _incorrectRepo.GetIncorrectsByDefinition(definitionId);
                if (incorrect == null)
                {
                    return NotFound();
                }

                var incorrectDTO = new List<IncorrectDTO>();

                foreach (var incor in incorrect)
                {
                    var i = new IncorrectDTO()
                    {
                        Id = incor.Id,
                        content = incor.Content.ToString()
                    };
                    incorrectDTO.Add(i);
                }

                return Ok(incorrectDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }


 
        [HttpPost("NewIncorrect")]
        public async Task<ActionResult<IncorrectDTO>> AddIncorrect([FromBody] IncorrectDTO incorrect)
        {
            try
            {
                var inc = new IncorrectDefinition() { Content = incorrect.content };
                var added = await _incorrectRepo.AddIncorrect(inc);

                if (added == null)
                    return NotFound();
                IncorrectDTO res = new IncorrectDTO() { content = added.Content, Id = added.Id };

                return CreatedAtAction(nameof(GetAllIncorrect), new { id = res.Id }, res);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        [HttpPost("AddPair")]
        public async Task<ActionResult<DefIncPairDTO>> AddPair(DefIncPairDTO pair)
        {
            try
            {
                var added = await _incorrectRepo.AddPair(pair.DefinitionID, pair.IncorrectID);
                if (added == null)
                    return NotFound();
                var res = new DefIncPairDTO() {DefinitionID = added.DefinitionId, Id = added.Id, IncorrectID = added.IncorrectDefinitionId };

                return CreatedAtAction(nameof(GetAllIncorrect), new { id = res.Id }, res);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();

            }
        }

        
        [HttpDelete("DeletePair/{definitionId}/{incorrectid}")]
        public async Task<ActionResult> DeletePair([FromRoute] int definitionId, [FromRoute] int incorrectid)
        {
            try
            {
                var res = await _incorrectRepo.DeletePair(definitionId, incorrectid);

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

        [HttpDelete("DeletePairByDefinition/{definitionId}")]
        public async Task<ActionResult> DeletePairByDefinition([FromRoute] int definitionId)
        {
            try
            { 
                var pairs = await _incorrectRepo.GetAllPairs();
                if (pairs == null)
                    return NotFound();

                bool deleted = false;
                foreach (var pair in pairs)
                {
                    if (pair.DefinitionId == definitionId)
                    {
                        deleted =await _incorrectRepo.DeletePair(pair.DefinitionId, pair.IncorrectDefinitionId);
                    }
                }
                if (!deleted)
                    return NotFound();
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        [HttpDelete("DeletePairByIncorrect/{incorrectid}")]
        public async Task<ActionResult> DeletePairByIncorrect([FromRoute] int definitionId)
        {
            try
            {
                var pairs = _incorrectRepo.GetAllPairs();
                if (pairs == null)
                    return NotFound();

                bool deleted = false;
                foreach (var pair in pairs.Result)
                {
                    if (pair.IncorrectDefinitionId == definitionId)
                    {
                       deleted = await _incorrectRepo.DeletePair(pair.DefinitionId, pair.IncorrectDefinitionId);
                    }
                }
                if (!deleted)
                    return NotFound();
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        }

        [HttpGet("Pairs")]
        public async Task<ActionResult<IEnumerable<DefIncPairDTO>>> GetPairs()
        {
            try
            {
                var incorrect = await _incorrectRepo.GetAllPairs();
                if (incorrect == null)
                {
                    return NotFound();
                }
                var response = new List<DefIncPairDTO>();
                foreach (var pair in incorrect)
                {
                    var buff= new DefIncPairDTO()
                    {
                        Id = pair.Id,
                        DefinitionID = pair.DefinitionId,
                        IncorrectID = pair.IncorrectDefinitionId,
                    };
                    response.Add(buff);
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();

            }
        }

        [HttpGet("PairsByDef/{definitionId}")]
        public async Task<ActionResult<IEnumerable<DefIncPairDTO>>> GetPairsByDef([FromRoute] int definitionId)
        {
            try
            {
                var incorrect = await _incorrectRepo.GetPairsByDefinition(definitionId);
                if (incorrect == null)
                {
                    return NotFound();
                }

                var response = new List<DefIncPairDTO>();
                foreach (var pair in incorrect)
                {
                    var buff = new DefIncPairDTO()
                    {
                        Id = pair.Id,
                        DefinitionID = pair.DefinitionId,
                        IncorrectID = pair.IncorrectDefinitionId,
                    };
                    response.Add(buff);
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest();
            }
        
        }
    }
}
