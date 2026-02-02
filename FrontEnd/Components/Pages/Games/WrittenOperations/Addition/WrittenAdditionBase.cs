using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Games.WrittenOperations.Addition
{
    public class WrittenAdditionBase : ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        protected bool ready = false;
        protected int exNum1;
        protected int exNum2;
        protected string exerciseNumber1="";
        protected string exerciseNumber2="";

        protected char operation;
        protected int correctNumber;

        protected string excercise = "";
        protected override void OnInitialized()
        {
            PrepareNewGame();
            ready = true;
        }

        protected void PrepareNewGame()
        {
            Random rnd = new Random();

            exNum1 = rnd.Next(1, 90);
            exNum2 = rnd.Next(1, 90);

            var option = rnd.Next(0, 2);

            if (exNum1 < exNum2)
            {
                var buff = exNum2;
                exNum2 = exNum1;
                exNum1 = buff;
            }

            switch (option)
            {
                case 0:                                                     //  +                   
                    operation = '+';
                    correctNumber = exNum1 + exNum2;
                    excercise = " ";

                    break;

                case 1:                                                     //  -   
                    operation = '-';
                    correctNumber = exNum1 - exNum2;

                    break;

                default:
                    excercise = "Error";
                    break;
            }
            var a = ((exNum1 - exNum1 % 10) / 10).ToString();
            if (a == "0")
            {
                a = " ";
            }
            var b = (exNum1 % 10);
            exerciseNumber1 = a + "  " + b;


            a = ((exNum2 - exNum2 % 10) / 10).ToString();
            if (a == "0")
            {
                a = " ";
            }
            b = (exNum2 % 10);
            exerciseNumber2 = a + "  " + b;
        }
    }
}
