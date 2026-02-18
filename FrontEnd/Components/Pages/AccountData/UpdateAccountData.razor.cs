using DTO.DTOs;
using FrontEnd.Components.Pages.Admin.Accounts;
using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace FrontEnd.Components.Pages.AccountData
{
    public partial class UpdateAccountDataBase: ComponentBase
    {
        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        IAccountService accountService { get; set; }

       

        protected string password = "", email = "", password2="";
        protected AccountsPasswordsDTO account{ get; set; }
        protected override async Task OnInitializedAsync()
        {
            var authenticationState = await authenticationStateTask;
            string username = authenticationState.User.Identity.Name;
            if (username != null)
            {
                account = await accountService.GetAccountPasswordsByName(username);

                //Accounts = await accountService.GetAccounts();
               // password = account.Password;
                email = account.Email;
               // password2 = password;
            }

        }
        protected async Task EditAccount(string email, string pass)
        {
            var acc = new AccountsPasswordsDTO() { Username = account.Username, Email = email, Password= pass , isActive = account.isActive, Role = account.Role, Salt=account.Salt};
            // var response = await definitionService.UpdateDefinition(def);
            await accountService.UpdateAccountUser(acc);
            
        }
    }
}
