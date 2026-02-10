using FrontEnd.Components.Classes;
using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Games.Abs
{
    public class AbsBase : ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        protected bool ready = false;
        protected int exampleNumber;
        protected int correctNumber;
        // protected List<int> wrongNumbers = new List<int>();
        protected GamesBase gameBase = new GamesBase("Liczby Całkowite", "Gry");

        protected override void OnInitialized()
        {
            PrepareNewGame();
        }

        protected void PrepareNewGame()
        {
            Random rnd = new Random();

            exampleNumber = rnd.Next(-50, 50);
            correctNumber = Math.Abs(exampleNumber);

            ready = true;
        }
    }
}
