using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;


namespace FrontEnd.Components.Pages.Games.BiggerSmallerGameChooseSign
{
    public class BiggerSmallerGameLvl2Base : ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        [Parameter]
        public required string type { get; set; }

        protected bool ready = false;
        protected int excerciseNumber1;
        protected int excerciseNumber2;
        protected int symbol;
        protected string anwser = "";
        protected List<string> wrongAnwser = new List<string>() { ">","<","=" };


        protected int excerciseNumberDen1;
        protected int excerciseNumberDen2;
        protected override void OnInitialized()
        {
            PrepareNewGame();
            ready = true;
        }

        protected void PrepareNewGame()
        {
            Random rnd = new Random();
            wrongAnwser = new List<string> { ">", "<", "=" };

            switch (type)
            {
                case "easy":
                    excerciseNumber1 = rnd.Next(4, 10);
                    symbol = rnd.Next(0, 3);
                    break;

                case "easyDoubDig":
                    excerciseNumber1 = rnd.Next(10, 100);
                    symbol = rnd.Next(0, 3);
                    break;

                case "natural":
                    excerciseNumber1 = rnd.Next(4, 1000);
                    symbol = rnd.Next(0, 3);
                   
                    break;

                case "minus":
                    excerciseNumber1 = rnd.Next(-500, 500);
                    symbol = rnd.Next(0, 3);
               
                    break;
                case "fraction":
                    excerciseNumberDen1=rnd.Next(2,11);
                    excerciseNumberDen2 = rnd.Next(2, 11);

                    excerciseNumber1 = rnd.Next(1, excerciseNumberDen1);
                    excerciseNumber2 = rnd.Next(1, excerciseNumberDen2);

                    if ((double)excerciseNumber2 / excerciseNumberDen2 == (double)excerciseNumber1 / excerciseNumberDen1)
                    {
                        anwser = "=";

                    }
                    else
                    {
                        if ((double)excerciseNumber1 / excerciseNumberDen1 > (double)excerciseNumber2 / excerciseNumberDen2)
                        {
                            anwser = ">";
                        }
                        else
                        {
                            anwser = "<";
                        }
                    }
                    wrongAnwser.Remove(anwser);
                    return;
            }
                        

            switch (symbol)
            {
                case 0:                                                     //  =
                    excerciseNumber2 = excerciseNumber1;
                    anwser = "=";
                    break;

                case 1:                                                     //  <
                    var add = rnd.Next(1, Math.Abs(excerciseNumber1));
                    excerciseNumber2 = excerciseNumber1 + add;
                    anwser = "<";
                    break;

                case 2:                                                     //  >
                    var sub = rnd.Next(1, Math.Abs(excerciseNumber1) - 1);
                    excerciseNumber2 = excerciseNumber1 - sub;
                    anwser = ">";
                    break;
                   
            }
            wrongAnwser.Remove(anwser);
        }
    }
}
