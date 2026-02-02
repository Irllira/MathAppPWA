using DTO.DTOs;
using MathApp.Enteties;
using MathEducationWebApp.Components.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers
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
                throw;
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
                throw;
            }



        }

        [HttpGet("AccountByName/{name}")]
        public async Task<ActionResult<AccountsPasswordsDTO>> GetAccountPasswordByName([FromRoute] string name)
        {
            try
            {
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
                throw;
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
                throw;
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
                throw;
            }

        }



        [HttpPost("AddAccount")]
        public async Task<ActionResult<AccountsPasswordsDTO>> AddAccount([FromBody] AccountsPasswordsDTO account)
        {
            var acc = new Account() { Email = account.Email, Password = account.Password, Username = account.Username, isActive = true, Salt = account.Salt, Role = account.Role };
            await _accountRepo.AddAccount(acc);
            return CreatedAtAction(nameof(GetAccounts), new { id = acc.Id }, acc);
        }

        [HttpPost("AddAccountByData")]
        public async Task AddAccountByData([FromForm] string un, [FromForm] string pass, [FromForm] string em, [FromForm] string salt)
        {
            var acc = new Account() { Email = em, Password = pass, Username = un, isActive = true, Salt = salt, Role = "User" };
            await _accountRepo.AddAccount(acc);
            //return CreatedAtAction(nameof(GetAccounts), new { id = acc.Id }, acc);
        }

        [HttpPost("UpdateAdmin")]
        public async Task UpdateAdmin([FromBody] AccountsDTO account)
        {
            await _accountRepo.UpdateAccountAdmin(account.Username, account.Role, account.isActive);
        }

        [HttpPost("UpdateUser")]
        public async Task UpdateUser([FromBody] AccountsPasswordsDTO account)
        {
            await _accountRepo.UpdateAccountUser(account.Username, account.Email, account.Password);
        }
    }
}
