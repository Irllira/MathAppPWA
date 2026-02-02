using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Games.Brackets
{
    public class BracketBase :ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        [Parameter]
        public required string type { get; set; }

        protected string part="";
        protected bool ready=false;
        protected int excerciseNumber1;
        protected int excerciseNumber2;
        protected int symbol;
        protected char sym;

        protected int modifier;

        protected List<string> wrongAnwsers = new List<string>();

        protected string excercise="";
        protected override void OnInitialized()
        {
            PrepareNewGame();
        }

        protected void PrepareNewGame()
        {
            wrongAnwsers = new List<string>();
            part = "first";
            Random rnd= new Random();
            symbol = rnd.Next(0, 2);

           

            // modifier = rnd.Next(2, 10);


            if (type == "from")
            {
                excerciseNumber1 = rnd.Next(2, 20);
                excerciseNumber2 = rnd.Next(2, 20);
                modifier = rnd.Next(2, 10);
                excerciseNumber1 *= modifier;
                excerciseNumber2 *= modifier;
                modifier = Eukl(excerciseNumber1, excerciseNumber2);

                switch (symbol)
                {
                    case 0:                     // +
                        excercise = excerciseNumber1 + "x + " + excerciseNumber2;
                        sym = '+';
                        break;

                    case 1:                     // -
                        excercise = excerciseNumber1 + "x - " + excerciseNumber2;
                        sym = '-';
                        break;

                        // case 2:                     // *
                        //   excercise = excerciseNumber1 + "x * " + excerciseNumber2;
                        // break;

                        //case 3:                     // :
                        //  excercise = excerciseNumber1 + "x : " + excerciseNumber2;

                        //break;
                }
                if (excerciseNumber1 != modifier && excerciseNumber2 % excerciseNumber1 != 0)
                {
                    wrongAnwsers.Add(excerciseNumber1 + "");
                }
                if (excerciseNumber2 != modifier && excerciseNumber1 % excerciseNumber2 != 0)
                {
                    wrongAnwsers.Add(excerciseNumber2 + "");
                }
                for (int i = 0; i < 2; i++)
                {
                    var r = rnd.Next(2, 50);
                    while (r == modifier || (excerciseNumber1 % r == 0 && excerciseNumber2 % r == 0))
                    {
                        r = rnd.Next(2, 50);
                    }
                    wrongAnwsers.Add(r + "");
                }
                rnd.Shuffle<string>(wrongAnwsers.ToArray());

                // ready = true;
            }
            else
            {
                excerciseNumber1 = rnd.Next(2, 11);
                excerciseNumber2 = rnd.Next(2, 11);
                do
                {
                    modifier = rnd.Next(-10, 10);
                } while (modifier == 0 || modifier == 1);
                switch (symbol)
                {
                    case 0:                     // +
                        excercise = modifier + "(" + excerciseNumber1 + "x + " + excerciseNumber2 + ") =";
                        sym = '+';
                        break;

                    case 1:                     // -
                        excercise = modifier + "(" + excerciseNumber1 + "x - " + excerciseNumber2 + ") =";
                        sym = '-';
                        break;
                }
            }
            ready = true;
        }

        protected int Eukl(int a, int b)
        {
            while(a!=0 && b!=0)
            {
                if(a>b)
                {
                    var buff = a % b;
                    a = buff;
                }else
                {
                    var buff = b % a;
                    b = buff;
                }
            }

            if(a==0)
            {
                return b;
            }
            else
            {
                return a;
            }
        }
    }
}
