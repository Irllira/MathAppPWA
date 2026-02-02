using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Games.Sqr.Root
{
    public class FromRootBase : ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        protected int exerciseNumber;
        protected int power =2;
        protected int correctRoot;
        protected int correctFullNmb;


        protected bool ready = false;
        protected override void OnInitialized()
        {
            PrepareNewGame();
        }

        protected void PrepareNewGame()
        {
            Random rnd = new Random();
            int rtable = rnd.GetItems(rootable, 1)[0];

            do
            {
                exerciseNumber = rnd.Next(2, 10);
            } while (rootable.Contains(exerciseNumber));

            correctRoot = exerciseNumber;
            exerciseNumber *= rtable;
            correctFullNmb = (int)Math.Sqrt(rtable);
            ready = true;
        }
        protected int[] rootable = [4, 9, 16, 25, 36];
    }
}
