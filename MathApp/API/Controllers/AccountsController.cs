using DTO.DTOs;
using MathApp.Backend.API.Interfaces;
using MathApp.Backend.Data.Enteties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MathApp.Backend.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepo _accountRepo;

        public AccountsController(IAccountRepo accountRepo)
        {
            _accountRepo = accountRepo;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountsDTO>>> GetAccounts()
        {
            try
            {
                var accounts = await _accountRepo.GetAllAccounts();

                if (accounts == null)
                {
                    return NotFound();
                }

                var accountsDTO = new List<AccountsDTO>();

                foreach (var account in accounts)
                {
                    var acc = new AccountsDTO() { Username = account.Username, Email = account.Email, isActive = account.isActive, Role = account.Role };
                    accountsDTO.Add(acc);
                }
                return Ok(accountsDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }


        [HttpGet("Login")]
        public async Task<ActionResult<IEnumerable<AccountsPasswordsDTO>>> GetAccountsPasswords()
        {
            try
            {
                var accounts = await _accountRepo.GetAllAccounts();

                if (accounts == null)
                {
                    return NotFound();
                }

                var accountsDTO = new List<AccountsPasswordsDTO>();

                foreach (var account in accounts)
                {
                    var acc = new AccountsPasswordsDTO() { Username = account.Username, Password = account.Password, Email = account.Email, Salt = account.Salt, isActive = account.isActive, Role = account.Role };
                    accountsDTO.Add(acc);
                }
                return Ok(accountsDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        [HttpGet("AccountByName/{name}")]
        public async Task<ActionResult<AccountsPasswordsDTO>> GetAccountPasswordByName([FromRoute] string name)
        {
            try
            {
                if(name == null)
                {
                    throw new Exception("Name cannot be null");
                }

                var account = await _accountRepo.GetAccountByName(name);

                if (account == null)
                {
                    return NotFound();
                }

                AccountsPasswordsDTO accountDTO = new AccountsPasswordsDTO() { Username = account.Username, Password = account.Password, Email = account.Email, Salt = account.Salt, isActive = account.isActive, Role = account.Role };

                return Ok(accountDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e); 
                return BadRequest(e.Message);
            }
        }
        
        [HttpGet("AccountAdminByName/{name}")]
        public async Task<ActionResult<AccountsDTO>> GetAccountAdminByName([FromRoute] string name)
        {
            try
            {
                var account = await _accountRepo.GetAccountByName(name);

                if (account == null)
                {
                    return NotFound();
                }

                AccountsDTO accountDTO = new AccountsDTO() { Username = account.Username, Email = account.Email, isActive = account.isActive, Role = account.Role };

                return Ok(accountDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }


        [HttpGet("AccountAdminByEmail/{email}")]
        public async Task<ActionResult<AccountsDTO>> GetAccountAdminByEmail([FromRoute] string email)
        {
            try
            {
                var account = await _accountRepo.GetAccountByEmail(email);

                if (account == null)
                {
                    return NotFound();
                }

                AccountsDTO accountDTO = new AccountsDTO() { Username = account.Username, Email = account.Email, isActive = account.isActive, Role = account.Role };

                return Ok(accountDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }



        [HttpPost("AddAccount")]
        public async Task<ActionResult<AccountsPasswordsDTO>> AddAccount([FromBody] AccountsPasswordsDTO account)
        {
            try
            {
                var acc = new Account() { Email = account.Email, Password = account.Password, Username = account.Username, isActive = true, Salt = account.Salt, Role = account.Role };
                var added = await _accountRepo.AddAccount(acc);
                if (added == null)
                {
                    return NotFound();
                }
                var addedDTO = new AccountsPasswordsDTO() { Username = added.Username, Password = added.Password, Email = added.Email, Salt = added.Salt, isActive = added.isActive, Role = added.Role };

                return CreatedAtAction(nameof(GetAccounts), new { username = addedDTO.Username }, addedDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }
        [HttpPost("AddAccountByData")]
        public async Task<ActionResult<AccountsPasswordsDTO>> AddAccountByData([FromForm] string un, [FromForm] string pass, [FromForm] string em, [FromForm] string salt)
        {
            try
            {
                var acc = new Account() { Email = em, Password = pass, Username = un, isActive = true, Salt = salt, Role = "User" };
                var added = await _accountRepo.AddAccount(acc);
                if (added == null)
                {
                    return NotFound();
                }
                var addedDTO = new AccountsPasswordsDTO() { Username = added.Username, Password = added.Password, Email = added.Email, Salt = added.Salt, isActive = added.isActive, Role = added.Role };

                return CreatedAtAction(nameof(GetAccounts), new { username = addedDTO.Username }, addedDTO);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);

            }
        }

        [HttpPost("UpdateAdmin")]
        public async Task<ActionResult<AccountsPasswordsDTO>> UpdateAdmin([FromBody] AccountsDTO account)
        {
            try
            {
                var updated = await _accountRepo.UpdateAccountAdmin(account.Username, account.Role, account.isActive);
                if(updated == false)
                {
                    return NotFound();
                }
                return Ok(account);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("UpdateUser")]
        public async Task<ActionResult<AccountsPasswordsDTO>> UpdateUser([FromBody] AccountsPasswordsDTO account)
        {
            try
            {
                bool updated = await _accountRepo.UpdateAccountUser(account.Username, account.Email, account.Password);
                if(updated == false)
                {
                    return NotFound();
                }  
                return Ok(account);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }
    }
}
