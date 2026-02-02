using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Games.RoundingUpDown
{
    public class RoundUpDownBase : ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        protected bool ready = false;
        protected int excerciseNumber;
        protected int roundTo;
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

            excerciseNumber = rnd.Next(0, 10);
            var num = rnd.Next(0,100);
            num = num * 10;
            var num2 = rnd.Next(0,9);
            num = num + num2;
            excerciseNumber = num;

            var option = rnd.Next(0, 2);

            switch (option)
            {
                case 0:                                                     //  10                   
                    roundTo = 10;
                    if(excerciseNumber%10>=5)
                    {
                        correctNumber = excerciseNumber - (excerciseNumber % 10) + 10;
                    }else
                    {
                        correctNumber = excerciseNumber - (excerciseNumber % 10);
                    }
                        break;

                case 1:                                                     //  100   
                    roundTo = 100;
                    if (excerciseNumber % 100 >= 50)
                    {
                        correctNumber = excerciseNumber - (excerciseNumber % 100) + 100;
                    }
                    else
                    {
                        correctNumber = excerciseNumber - (excerciseNumber % 100);
                    }
                    break;

                case 2:                                                     //  1000
                    roundTo = 1000;
                    if (excerciseNumber % 1000 >= 500)
                    {
                        correctNumber = excerciseNumber - (excerciseNumber % 1000) + 1000;
                    }
                    else
                    {
                        correctNumber = excerciseNumber - (excerciseNumber % 1000);
                    }
                    break;
              
                default:
                    excercise = "Error";
                    break;
            }
            excercise = "Zaokrąglij " + excerciseNumber + " do " + roundTo;            
        }      
    }
}
