using FrontEnd.Components.Classes;
using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.CountingUpAndDown.CountingUpDownBase
{
    public class CountingUpDownBase: ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        protected bool ready = false;
        protected int excerciseNumber;
        protected int symbol;
        protected int correctNumber;
        protected int[] wrongNumbers = new int[4];

        protected string excercise = "";

        protected GamesBase gameBase = new GamesBase("Podstawy Liczb", "Gry");
        protected override void OnInitialized()
        {
            PrepareNewGame();
            ready = true;
        }

        protected void PrepareNewGame()
        {

            Random rnd = new Random();
            excerciseNumber = rnd.Next(10, 20);
            symbol = rnd.Next(0, 2);

            switch (symbol)
            {
                case 0:                                                     //  +
                    correctNumber = excerciseNumber+1;
                    excercise = "Licz w górę: \n" + excerciseNumber + " ";

                    break;

                case 1:                                                     //  -   
                    correctNumber = excerciseNumber -1;
                    excercise = "Licz w dół: \n"+excerciseNumber + " ";

                    break;
                    
                default:
                    excercise = "Error";
                    break;
            }
            FillTheWrongAnswers();
        }

        protected void FillTheWrongAnswers()
        {
            Random rnd = new Random();
            int random = rnd.Next(excerciseNumber, excerciseNumber+ 10);

            for(int i=0;i<4;i++)
            {
                while(random==correctNumber)
                {
                    random= rnd.Next(excerciseNumber, excerciseNumber + 10);
                }
                wrongNumbers[i] = random;
                random = rnd.Next(excerciseNumber, excerciseNumber + 10);
            }
        }
        protected void AddNextNumber()
        {
            excerciseNumber = correctNumber;
            excercise = excercise+" "+correctNumber;

            switch (symbol)
            {
                case 0:                                                     //  +
                    correctNumber = excerciseNumber + 1;

                    break;

                case 1:                                                     //  -   
                    correctNumber = excerciseNumber - 1;

                    break;

                default:
                    excercise = "Error";
                    break;
            }
            FillTheWrongAnswers();
        }
    }
}
