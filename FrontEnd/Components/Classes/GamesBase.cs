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
        string unitName;
        string type;

        public int good = 0;
        public int all = 0;
        public string message = "";
        public GamesBase(string unName, string tp)
        {
            unitName = unName;
            type = tp;
        }      

        public async Task GoodAnswer()
        {
            good++;
            all++;
            message = "Dobrze!";

            await Task.Delay(500);
            message = "";
        }
        public async Task WrongAnswer()
        {
            all++;
            message = "Zła Odpowiedź! Spróbuj ponownie";
            await Task.Delay(1500);
            message = "";
        }

    
        public async Task UpdateUserScore(Task<AuthenticationState> authenticationStateTask, IUserProgressService userProgressService)
        {
            if (all == 0)
                return;

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
    }
}
