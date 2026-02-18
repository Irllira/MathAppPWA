using DTO.DTOs;
using MathApp.Backend.API.Interfaces;
using MathApp.Backend.Data.Enteties;
using MathApp.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MathApp.Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserProgressController : ControllerBase
    {
        private readonly IUserProgressRepo _userProgressRepo;
        private readonly IAccountRepo _accountRepo;
        private readonly IUnitRepo _unitRepo;

        public UserProgressController(IUserProgressRepo userProgressRepo, IAccountRepo accountRepo, IUnitRepo unitRepo)
        {
            _userProgressRepo = userProgressRepo;
            _accountRepo = accountRepo;
            _unitRepo = unitRepo;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProgressDTO>>> GetAllUserProgress()
        {
            try
            {
                var userProg = await _userProgressRepo.GetUserProgress();

                if (userProg == null)
                {
                    return NotFound();
                }
                List<UserProgressDTO> result = new List<UserProgressDTO>();
                foreach (var up in userProg)
                {
                    var unit = await _unitRepo.GetUnitByID(up.UnitId);
                    var acc = await _accountRepo.GetAccountById(up.AccountId);

                    if (unit != null && acc != null)
                    {
                        result.Add(new UserProgressDTO() { Id = up.Id, type = up.type, AccountId = up.AccountId, all = up.all, good = up.good, unitName = unit.name });
                    }
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("GetProgressByUser/{userName}")]
        public async Task<ActionResult<IEnumerable<UserProgressDTO>>> GetUserProgressByUser([FromRoute] string userName)
        {
            try
            {
                var acc = await _accountRepo.GetAccountByName(userName);

                if (acc == null)
                {
                    return NotFound();
                }

                var userProg = await _userProgressRepo.GetUserProgressByUserId(acc.Id);

                if (userProg == null)
                {
                    return NotFound();
                }
                List<UserProgressDTO> result = new List<UserProgressDTO>();
                foreach (var up in userProg)
                {
                    var unit = await _unitRepo.GetUnitByID(up.UnitId);
                    if (unit != null)
                    {
                        result.Add(new UserProgressDTO() { Id = up.Id, type = up.type, AccountId = up.AccountId, all = up.all, good = up.good, unitName = unit.name });
                    }
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetProgressByUserUnitType/{userName}/{unitName}/{type}")]
        public async Task<ActionResult<IEnumerable<UserProgressDTO>>> GetUserProgressByAll([FromRoute] string userName, [FromRoute] string unitName, [FromRoute] string type)
        {
            try
            {
                var acc = await _accountRepo.GetAccountByName(userName);
                var unit = await _unitRepo.GetUnitByName(unitName);

                if (acc == null || unit == null)
                {
                    return NotFound();
                }

                var userProg = await _userProgressRepo.GetUserProgressByUserIdUnitIdType(acc.Id, unit.Id, type);

                if (userProg == null)
                {
                    return NotFound();
                }

                var result = new UserProgressDTO() { Id = userProg.Id, type = userProg.type, AccountId = userProg.AccountId, all = userProg.all, good = userProg.good, unitName = unit.name };

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("NewProgress")]
        public async Task<ActionResult<UserProgressDTO>> AddProgress([FromBody] UserProgressDTO userProgress)
        {
            try
            {
                var unit = await _unitRepo.GetUnitByName(userProgress.unitName);
                var account = await _accountRepo.GetAccountById(userProgress.AccountId);

                if (unit == null || account==null)
                {
                    return NotFound();

                }
                var up = new Data.Enteties.UserProgress() { type = userProgress.type, AccountId = userProgress.AccountId, UnitId = unit.Id, all = userProgress.all, good = userProgress.good, Id = userProgress.Id };

                var res = await _userProgressRepo.AddProgress(up);
                if(res==null)
                    return NotFound();

                var resDTO = new UserProgressDTO() { type = res.type, unitName = unit.name, AccountId = account.Id, Id = res.Id, all = res.all, good = res.good };
                return CreatedAtAction(nameof(GetAllUserProgress), new { id = resDTO.Id }, resDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("NewProgressByName/{username}/{unitName}/{good}/{all}/{type}")]
        public async Task<ActionResult<UserProgressDTO>> AddProgressNames([FromRoute] string username, [FromRoute] string unitName, [FromRoute] int good, [FromRoute] int all, [FromRoute] string type)
        {
            try
            {
                var acc = await _accountRepo.GetAccountByName(username);
                var unit = await _unitRepo.GetUnitByName(unitName);
                if (acc == null || unit == null)
                {
                    return NotFound();
                }

                var up = new Data.Enteties.UserProgress() { type = type, AccountId = acc.Id, UnitId = unit.Id, all = all, good = good, Id = 0 };

                var res = await _userProgressRepo.AddProgress(up);

                if (res == null)
                    return NotFound();

                var resDTO = new UserProgressDTO() { type = res.type, unitName = unit.name, AccountId = acc.Id, Id = res.Id, all = res.all, good = res.good };
                return CreatedAtAction(nameof(GetAllUserProgress), new { id = resDTO.Id }, resDTO);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("UpdateProgress")]
        public async Task<ActionResult<UserProgressDTO>> UpdateProgress([FromBody] UserProgressDTO userProgress)
        {
            try
            {
                var res = await _userProgressRepo.UpdateProgress(userProgress.Id, userProgress.type, userProgress.all, userProgress.good);

                if (res == null)
                    return NotFound();

                var resDTO = new UserProgressDTO() { type = res.type, unitName = userProgress.unitName, AccountId = userProgress.AccountId, Id = res.Id, all = res.all, good = res.good };
                return CreatedAtAction(nameof(GetAllUserProgress), new { id = resDTO.Id }, resDTO);

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
