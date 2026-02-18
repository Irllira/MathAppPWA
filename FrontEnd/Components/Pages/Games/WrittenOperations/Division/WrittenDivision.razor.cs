using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Games.WrittenOperations.Division
{
    public class WrittenDivisionBase : ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        protected bool ready = false;
        protected int exNum1;
        protected int exNum2;
        protected string exerciseNumber1 = "";
        protected string exerciseNumber2 = "";

        protected int howLong;

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

            correctNumber = rnd.Next(10, 100);
            exNum2 = rnd.Next(1, 10);

            exNum1 = correctNumber * exNum2;


            exerciseNumber1 = "";
            string x = exNum1 + "";
            for (int i = 0; i < x.Length; i++)
            {
                exerciseNumber1= exerciseNumber1 + x[i]+" ";
            }
            exerciseNumber1.Remove(exerciseNumber1.Length-1);

            exerciseNumber2 = "";
            x = exNum2 + "";
            for (int i = 0; i < x.Length; i++)
            {
                exerciseNumber2 = exerciseNumber2 + x[i] + " ";
            }
            exerciseNumber2.Remove(exerciseNumber2.Length - 1);


            if (exNum1 > 99)
            {
                howLong = 3;
            }
            else
            {
                if (exNum1 > 9)
                {
                    howLong = 2;
                }
                else
                {
                    howLong = 1;
                }
            }
        }
    }
}
