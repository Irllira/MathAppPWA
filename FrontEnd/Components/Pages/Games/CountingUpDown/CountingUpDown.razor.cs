using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Games.CountingUpDown
{
    public class CountingUpDownBase: ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        [Parameter]
        public string? By { get; set; }

        protected bool ready = false;
        protected int excerciseNumber;
        protected int symbol;
        protected int countBy;
        protected int correctNumber;
        protected int[] wrongNumbers = new int[4];

        protected string excercise = "";
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


            if (By == "1" || By == "one" || By == null) 
            { 
                countBy = 1;
            }
            else
            {
                int cBy = rnd.Next(0, 3);

                switch (cBy)
                {
                    case 0:         // 1
                        countBy = 2;
                        break;
                    case 1:
                        countBy = 5;
                        break;
                    case 2:
                        countBy = 10;
                        break;
                }
            }
            excerciseNumber = excerciseNumber * countBy + rnd.Next(0, countBy);



            switch (symbol)
            {
                case 0:                                                     //  +
                    correctNumber = excerciseNumber + countBy;
                    if(countBy == 1)
                    {
                        excercise = "Licz w górę: \n" + excerciseNumber;
                        break;
                    }
                    else
                        excercise = "Licz w górę co "+countBy +" : \n" + excerciseNumber;

                    break;

                case 1:                                                     //  -   
                    correctNumber = excerciseNumber - countBy;
                    if (countBy == 1)
                    {
                        excercise = "Licz w dół: \n" + excerciseNumber;
                        break;
                    }
                    excercise = "Licz w dół co " + countBy + " : \n" + excerciseNumber;

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
            int random = rnd.Next(excerciseNumber / countBy, excerciseNumber / countBy + 10)*countBy;

            for(int i=0;i<4;i++)
            {
                while(random==correctNumber)
                {
                    random= rnd.Next(excerciseNumber/countBy, excerciseNumber/countBy + 10 )* countBy;
                }
                wrongNumbers[i] = random;
                random = rnd.Next(excerciseNumber / countBy, excerciseNumber / countBy + 10 )* countBy;
            }
        }
        protected void AddNextNumber()
        {
            excerciseNumber = correctNumber;
            excercise = excercise+", "+correctNumber;

            switch (symbol)
            {
                case 0:                                                     //  +
                    correctNumber = excerciseNumber + countBy;

                    break;

                case 1:                                                     //  -   
                    correctNumber = excerciseNumber - countBy;

                    break;

                default:
                    excercise = "Error";
                    break;
            }
            FillTheWrongAnswers();
        }
    }
}
