using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Games.BiggerSmallerGameGiveNumber
{
    public class BiggerSmallerLvl3Base : ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        [Parameter]
        public required string type { get; set; }

        protected bool ready = false;
        protected int excerciseNumber;
        protected int symbol;

        protected string excercise = "";
        protected override void OnInitialized()
        {
            PrepareNewGame();
            ready = true;
        }
        protected void PrepareNewGame()
        {
            Random rnd = new Random();
            switch(type)
            {
                case "easy":
                    excerciseNumber = rnd.Next(3, 10);
                    symbol = rnd.Next(0, 3);
                    break;
                case "easyDoubDig":
                    excerciseNumber = rnd.Next(10, 100);
                    symbol = rnd.Next(0, 3);
                    break;
                case "natural":
                    excerciseNumber = rnd.Next(1, 1000);
                    symbol = rnd.Next(0, 5);
                    break;
                case "minus":
                    excerciseNumber = rnd.Next(-500,500);
                    symbol = rnd.Next(0, 5);
                    break;
                case "fraction":
                    break;
            }
        }
    }
}
