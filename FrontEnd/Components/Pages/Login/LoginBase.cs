using DTO.DTOs;
using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Login
{
    public class LoginBase : ComponentBase
    {
        [Inject]
        protected IAccountService accountService { get; set; }


        public IEnumerable<AccountsPasswordsDTO> accounts { get; set; } = Enumerable.Empty<AccountsPasswordsDTO>();
       

        //  protected override async Task OnInitializedAsync()
     
        protected async Task AddAccount(string un, string pass, string em, string salt)
        {
            var acc = new AccountsPasswordsDTO() { Username = un, Password = pass, Email = em, Role = "User", isActive = true, Salt =salt };
            var response = await accountService.AddAccount(acc);
            await RefreshDatabase();

        }
        protected async Task RefreshDatabase()
        {
            accounts = await accountService.GetAccountsPasswords();

        }
    }
}
