using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Games.WrittenOperations.Multiplication
{
    public class WrittenMultiplicationBase : ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        protected bool ready = false;
        protected int exNum1;
        protected int exNum2;
        protected string exerciseNumber1 = "";
        protected string exerciseNumber2 = "";

        protected int correctNumber;

                
        protected override void OnInitialized()
        {
            PrepareNewGame();
            ready = true;
        }

        protected void PrepareNewGame()
        {
            Random rnd = new Random();

            exNum1 = rnd.Next(1, 100);
            exNum2 = rnd.Next(0, 10);

            correctNumber = exNum1 * exNum2;

            var a = ((exNum1 - exNum1 % 10) / 10).ToString();
            if (a == "0")
            {
                a = " ";
            }
            var b = (exNum1 % 10);
            exerciseNumber1 = a + "  " + b;

            
            exerciseNumber2 = exNum2+"";
        }
    }
}
