using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Games.Sqr.Root
{
    public class RootCompBase : ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        protected int exerciseNumber;
        protected int power;
        protected int correctSmallerNumber;
        protected int correctBiggerNumber;


        protected bool ready = false;
        protected override void OnInitialized()
        {
            PrepareNewGame();
        }

        protected void PrepareNewGame()
        {
            Random rnd = new Random();
            do
            {
                exerciseNumber = rnd.Next(2, 50);
            }while (Math.Sqrt(exerciseNumber)%1==0);
            power = 2;

            correctBiggerNumber = (int)(Math.Sqrt(exerciseNumber) - Math.Sqrt(exerciseNumber) % 1) + 1;
            correctSmallerNumber = (int)(Math.Sqrt(exerciseNumber) - Math.Sqrt(exerciseNumber) % 1);
            ready = true;
        }
    }
}
