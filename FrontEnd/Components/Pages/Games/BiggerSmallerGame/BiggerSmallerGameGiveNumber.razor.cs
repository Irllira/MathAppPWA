using FrontEnd.Components.Classes;
using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace MathApp.FrontEnd.Components.Pages.Games.BiggerSmallerGame
{
    public class BiggerSmallerGiveNumberBase : ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        [Parameter]
        public required string type { get; set; }

        protected bool ready = false;
        protected int excerciseNumber;
        protected int symbol;

        protected GamesBase gameBase = new GamesBase("Podstawy Liczb", "Gry");
        protected string excercise = "";
        protected override void OnInitialized()
        {
            string unitName= "";
            switch (type)
            {
                case "natural":
                    unitName = "Liczby Naturalne";
                    break;

                case "minus":
                    unitName = "Liczby Całkowite";
                    break;

                default:
                    unitName = "Podstawy Liczb";
                    break;
            }
            gameBase = new GamesBase(unitName, "Gry");
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
