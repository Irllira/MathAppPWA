using API.Enteties;
using API.Interfaces;
using DTO.DTOs;
using MathApp.Enteties;
using MathEducationWebApp.Components.Interfaces;
using MathEducationWebApp.Components.Nowy_folder;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
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

        // GET: api/<IncorrectController>
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
                        content = incor.content
                    };
                    incorrectDTO.Add(i);
                }

                return Ok(incorrectDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;

            }
        }

        // GET api/<IncorrectController>/5
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
                    content = incorrect.content
                };


                return Ok(incorrectDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;

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
                    content = incorrect.content
                };


                return Ok(incorrectDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;

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
                        content = incor.content.ToString()
                    };
                    incorrectDTO.Add(i);
                }

                return Ok(incorrectDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;

            }
        }


        // POST api/<IncorrectController>
        [HttpPost("NewIncorrect")]
        public async Task<ActionResult<IncorrectDTO>> AddIncorrect([FromBody] IncorrectDTO incorrect)
        {
            var inc = new IncorrectDefinition() { content = incorrect.content };
            await _incorrectRepo.AddIncorrect(inc);

            return CreatedAtAction(nameof(GetAllIncorrect), new { id = inc.Id }, inc);
        }

        [HttpPost("AddPair")]
        public async Task AddPair(DefIncPairDTO pair)
        {
            await _incorrectRepo.AddPair(pair.DefinitionID, pair.IncorrectID);
        }
        // PUT api/<IncorrectController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<IncorrectController>/5
        [HttpDelete("DeletePair/{definitionId}/{incorrectid}")]
        public void DeletePair([FromRoute] int definitionId, [FromRoute] int incorrectid)
        {
            _incorrectRepo.DeletePair(definitionId, incorrectid);
        }

        [HttpDelete("DeletePairByDefinition/{definitionId}")]
        public async Task DeletePairByDefinition([FromRoute] int definitionId)
        {
            // _incorrectRepo.DeletePairByDefinition(definitionId);
            var pairs = _incorrectRepo.GetAllPairs();
            if (pairs == null)
                return;

            foreach (var pair in pairs.Result)
            {
                if (pair.DefinitionId == definitionId)
                    await _incorrectRepo.DeletePair(pair.DefinitionId, pair.IncorrectDefinitionId);
            }

        }

        [HttpDelete("DeletePairByIncorrect/{incorrectid}")]
        public async Task DeletePairByDIncorrect([FromRoute] int definitionId)
        {
            // _incorrectRepo.DeletePairByDefinition(definitionId);
            var pairs = _incorrectRepo.GetAllPairs();
            if (pairs == null)
                return;

            foreach (var pair in pairs.Result)
            {
                if (pair.IncorrectDefinitionId == definitionId)
                    await _incorrectRepo.DeletePair(pair.DefinitionId, pair.IncorrectDefinitionId);
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
                throw;

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
                throw;

            }
        }
    }
}
