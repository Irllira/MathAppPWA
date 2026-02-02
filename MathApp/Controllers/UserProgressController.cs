using API.Enteties;
using API.Interfaces;
using DTO.DTOs;
using MathApp.Migrations;
using MathEducationWebApp.Components.Interfaces;
using MathEducationWebApp.Components.Nowy_folder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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
            var userProg = await _userProgressRepo.GetUserProgress();

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


        [HttpGet("GetProgressByUser/{userName}")]
        public async Task<ActionResult<IEnumerable<UserProgressDTO>>> GetUserProgressByUser([FromRoute] string userName)
        {
            var acc = await _accountRepo.GetAccountByName(userName);

            if(acc==null)
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

        [HttpGet("GetProgressByUserUnitType/{userName}/{unitName}/{type}")]
        public async Task<ActionResult<IEnumerable<UserProgressDTO>>> GetUserProgressByAll([FromRoute] string userName, [FromRoute] string unitName, [FromRoute] string type)
        {
            var acc = await _accountRepo.GetAccountByName(userName);
            var unit = await _unitRepo.GetUnitByName(unitName);

            if(acc==null || unit == null )
            {
                return NotFound();
            }

            var userProg = await _userProgressRepo.GetUserProgressByUserIdUnitIdType(acc.Id,unit.Id, type);

            if (userProg == null)
            {
                return NotFound();
            }

            var result = new UserProgressDTO() { Id = userProg.Id, type = userProg.type, AccountId = userProg.AccountId, all = userProg.all, good = userProg.good, unitName = unitName };

            return Ok(result);
        }

        [HttpPost("NewProgress")]
        public async Task<ActionResult<UserProgressDTO>> AddProgress([FromBody] UserProgressDTO userProgress)
        {
            var unit = await _unitRepo.GetUnitByName(userProgress.unitName);
            if (unit == null)
            {
                return NotFound();

            }
            var up = new Enteties.UserProgress() { type = userProgress.type, AccountId = userProgress.AccountId, UnitId = unit.Id, all = userProgress.all, good = userProgress.good, Id = userProgress.Id };

            await _userProgressRepo.AddProgress(up);
            return CreatedAtAction(nameof(GetAllUserProgress), new { id = up.Id }, up);
        }


        [HttpPost("NewProgressByName/{username}/{unitName}/{good}/{all}/{type}")]
        public async Task<ActionResult<UserProgressDTO>> AddProgressNames([FromRoute] string username, [FromRoute] string unitName, [FromRoute] int good, [FromRoute] int all, [FromRoute] string type , [FromBody] string s)
        {
            var acc =  await _accountRepo.GetAccountByName(username);
            var unit = await _unitRepo.GetUnitByName(unitName);
            if (acc == null || unit == null )
            {
                return NotFound();
            }

            var up = new Enteties.UserProgress() { type = type, AccountId = acc.Id, UnitId = unit.Id, all = all, good = good, Id = 0 };

            await _userProgressRepo.AddProgress(up);
            return CreatedAtAction(nameof(GetAllUserProgress), new { id = up.Id }, up);
        }


        [HttpPost("UpdateProgress")]
        public async Task UpdateProgress([FromBody] UserProgressDTO userProgress)

        {
            await _userProgressRepo.UpdateProgress(userProgress.Id, userProgress.type, userProgress.all, userProgress.good);

            //return CreatedAtAction(nameof(GetAllUserProgress), new { id = userProgress.Id }, userProgress);

        }

    }
}
