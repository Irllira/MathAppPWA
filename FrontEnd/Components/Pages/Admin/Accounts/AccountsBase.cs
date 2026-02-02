using DTO.DTOs;
using FrontEnd.Components.Services;
using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Admin.Accounts
{
    public class AccountsBase: ComponentBase
    {
        [Inject]
        IAccountService accountService { get; set; }

        public IEnumerable<AccountsDTO> Accounts { get; set; } = Enumerable.Empty<AccountsDTO>();


       
        protected override async Task OnInitializedAsync()
        {
           
            Accounts = await accountService.GetAccounts();
            
        }

        protected async Task EditAccount(string name, string email, string rl, bool act)
        {
            var acc = new AccountsDTO() {Username = name, Email = email, isActive= act, Role= rl};
            // var response = await definitionService.UpdateDefinition(def);
            await accountService.UpdateAccoutAdmin(acc);
            await RefreshDatabase();
        }

        protected async Task RefreshDatabase()
        {
            Accounts = await accountService.GetAccounts();

        }

    }
}
