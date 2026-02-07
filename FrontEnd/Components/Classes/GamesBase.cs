using DTO.DTOs;
using FrontEnd.Components.Pages.Admin.Definitions;
using FrontEnd.Components.Services;
using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FrontEnd.Components.Classes
{
    public class GamesBase
    {
        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        [Inject]
        NavigationManager NavManager { get; set; }

        string unitName;
        string type;

        int good = 0;
        int all = 0;
        public string message = "";
        public GamesBase(string unName, string tp)
        {
            unitName = unName;
            type = tp;
        }      

        public async Task GoodNumber()
        {
            good++;
            all++;
            message = "Dobrze!";


           // razor.StateHasChanged();
            if (all >= 10 )//|| usedDefinitions.Count == Definitions.Count)
            {
                //ready = false;
                await Task.Delay(500);
                await UpdateUserScore();
                message = "";
              //  this.StateHasChanged();
                return;
            }

            await Task.Delay(500);
            //await PrepareNew();
            message = "";

           // this.StateHasChanged();

        }
        public async Task WrongNumber()
        {
            all++;
            message = "Zła Odpowiedź! Spróbuj ponownie";
            //this.StateHasChanged();

            await Task.Delay(1500);
            //   PrepareNewGame();
            message = "";
            //  this.StateHasChanged();
        }

        public async Task UpdateUserScore()
        {
            var g = good;
            var a = all;
            var authenticationState = await authenticationStateTask;
            string username = authenticationState.User.Identity.Name;
            if (username != null)
            {
                var progrss = await userProgressService.GetProgressByAll(username, unitName, type);
                if (progrss != null)
                {
                    g += progrss.good;
                    a += progrss.all;
                    var updated = new UserProgressDTO() { Id = progrss.Id, AccountId = progrss.AccountId, type =type, unitName = unitName, all = a, good = g };
                    await userProgressService.UpdateProgress(updated);

                }
                else
                {
                    await userProgressService.AddProgressByName(username, unitName, type, good, all);
                }

            }
        }

        private void GoBack()
        {
            NavManager.NavigateTo("ChooseGame/uname/" + unitName);
        }
    }
}
