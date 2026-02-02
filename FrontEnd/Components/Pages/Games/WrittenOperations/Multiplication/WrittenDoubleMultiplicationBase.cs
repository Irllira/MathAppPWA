using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Games.WrittenOperations.Multiplication
{
    public class WrittenDoubleMultiplicationBase : ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        protected bool ready = false;
        protected int exNum1;
        protected int exNum2;
        protected string exerciseNumber1 = "";
        protected string exerciseNumber2 = "";

        protected int corrNum1;
        protected int corrNum2;

        protected int correctNumber;

                
        protected override void OnInitialized()
        {
            PrepareNewGame();
            ready = true;
        }

        protected void PrepareNewGame()
        {
            Random rnd = new Random();

            exNum1 = rnd.Next(10, 100);
            exNum2 = rnd.Next(10, 90);

            correctNumber = exNum1 * exNum2;

            if (exNum2 > exNum1)
            {
                var buff = exNum1;
                exNum1 = exNum2;
                exNum2 = buff;
            }

            corrNum1= exNum1* (exNum2%10);
            corrNum2 = exNum1*(exNum2-exNum2%10)/10;

            exerciseNumber1 = (exNum1-exNum1%10)/10+" "+exNum1%10;
            exerciseNumber2 = (exNum2-exNum2%10)/10+" "+exNum2%10;
            
        }
    }
}
