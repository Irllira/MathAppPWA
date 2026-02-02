using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Games.BiggerSmallerGame
{
    public class CountingUpDownBase: ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        [Parameter]
        public required string type { get; set; }

        protected bool ready = false;
        protected double excerciseNumber;
        protected int symbol;
        protected double correctNumber;
        protected double[] wrongNumbers = new double[4];

        protected int excerciseNumberDen;
        protected int correctNumberDen;
        protected int[] wrongNumbersDen = new int[4];

        protected string excercise = "";
        int max = 0;
        int min = 0;
        protected override void OnInitialized()
        {
            PrepareNewGame();
            ready = true;
        }

        protected void PrepareNewGame()
        {
            Random rnd = new Random();
            switch(type)
            {
                case "easy":
                    excerciseNumber = rnd.Next(4, 10);
                    symbol = rnd.Next(0, 3);
                    max = 10;
                    min = 0;
                    break;
                case "easyDoubDig":
                    excerciseNumber = rnd.Next(4, 100);
                    symbol = rnd.Next(0, 3);
                    min = 0;
                    max = 100;
                    break;
                case "natural":
                    excerciseNumber = rnd.Next(4, 1000);
                    symbol = rnd.Next(0, 5);
                    min = 0;
                    max = 1000;
                    break;
                case "minus":
                    excerciseNumber = rnd.Next(-500, 500);
                    symbol = rnd.Next(0, 5);
                    min = -500;
                    max = 500;
                    break;
                case "fractions":
                    FractionsExcercise();
                    return;
                    
                case "fractionsDec":
                    excerciseNumber = rnd.Next(1, 10) + rnd.NextDouble();
                    break;
            }                     

            switch (symbol)
            {
                case 0:                                                     //  =
                    correctNumber = excerciseNumber;
                    excercise = excerciseNumber + " = ";
                    
                    break;
                case 1:                                                     //  <
                    var add = rnd.Next(1, (int)Math.Abs(excerciseNumber) - 1);
                    correctNumber = excerciseNumber + add;
                    excercise = excerciseNumber + " < ";

                    break;
                case 2:                                                     //  >
                    var sub = rnd.Next(1, (int)Math.Abs(excerciseNumber )- 1);

                    correctNumber = excerciseNumber - sub;
                    excercise = excerciseNumber + " > ";

                    break;
                case 3:                                                     //  <=
                    var eqadd = rnd.Next(0, (int)Math.Abs(excerciseNumber )- 1);

                    correctNumber = excerciseNumber + eqadd;
                    excercise = excerciseNumber + " ≤ ";

                    break;
                case 4:                                                     //  >=
                    var eqsub = rnd.Next(0, (int)Math.Abs(excerciseNumber )+ 1);

                    correctNumber = excerciseNumber - eqsub;
                    excercise = excerciseNumber + " ≥ ";

                    break;
                default:
                    excercise = "Error";
                    break;
            }
            FillTheWrongAnswers(symbol);
        }

        protected void FillTheWrongAnswers(int symbol)
        {
            Random rnd = new Random();
            int a = 0;

            switch (symbol)
            {
                case 0:                                         //  =
                    for (int i = 0; i < 4; i++)
                    {
                        do
                        {
                            a = rnd.Next(min, max);
                        } while (a == correctNumber);

                        wrongNumbers[i]=a;
                    }
                    break;
                case 1:                                         //  <
                    for (int i = 0; i < 4; i++)
                    {
                        a = rnd.Next(min, (int)excerciseNumber +1);
                        wrongNumbers[i] = a;
                    }                    
                    break;
                   
                case 2:                                         //  >                   
                    for (int i = 0; i < 4; i++)
                    {
                        a = rnd.Next((int)excerciseNumber, max+20);
                        wrongNumbers[i] = a;
                    }
                    break;

                case 3:                                                     //  <=
                    for (int i = 0; i < 4; i++)
                    {
                        a = rnd.Next(min, (int)excerciseNumber);
                        wrongNumbers[i] = a;
                    }
                    break;

                case 4:                                                     //  >=
                    for (int i = 0; i < 4; i++)
                    {
                        a = rnd.Next((int)excerciseNumber +1, max + 20);
                        wrongNumbers[i] = a;
                    }
                    break;

                default:
                    excercise = "Error";
                    break;
            }
        }


        protected void FractionsExcercise()
        {
            Random rnd = new Random();

            excerciseNumberDen = rnd.GetItems(denominators, 1)[0];
            excerciseNumber = rnd.Next(1, excerciseNumberDen);
            int den=0, nww=0, multi = 0;

            if (symbol != 0)
            {
                den = rnd.GetItems(denominators, 1)[0];
                nww = NWW(den, excerciseNumberDen);

                correctNumberDen = nww;
                multi = nww/excerciseNumberDen;
            }

            switch (symbol)
            {
                case 0:                                                     //  =
                    var mlti = rnd.Next(1, 6);
                    correctNumber = excerciseNumber * mlti;
                    correctNumberDen = excerciseNumberDen * mlti;
                    excercise = " = ";
                    break;

                case 1:                                                     //  <
                    var add = rnd.Next(1,(int)(nww-excerciseNumber*multi));
                    correctNumber = excerciseNumber * multi+add;
                    excercise = " < ";
                    break;

                case 2:                                                     //  >
                    var sub = rnd.Next(1, (int)(excerciseNumber * multi));
                    correctNumber = excerciseNumber * multi - sub;
                    excercise = " > ";
                    break;

                case 3:                                                     //  <=
                    var eqadd = rnd.Next(0, (int)(nww - excerciseNumber * multi));
                    correctNumber = excerciseNumber*multi + eqadd;
                    excercise = " ≤ ";
                    break;

                case 4:                                                     //  >=
                    var eqsub = rnd.Next(0, (int)(excerciseNumber * multi)-1);
                    correctNumber = excerciseNumber*multi - eqsub;
                    excercise = " ≥ ";
                    break;

                default:
                    excercise = "Error";
                    break;
            }

            FillTheWrongAnswersFractions();
        }


        protected void FillTheWrongAnswersFractions()
        {
            Random rnd = new Random();
            int den = 0, nww = 0, multi = 0;

            for (int i = 0; i < 4; i++)
            {
                den = rnd.GetItems(denominators, 1)[0];
                nww = NWW(den, excerciseNumberDen);

                wrongNumbersDen[i] = nww;
                multi =  nww/ excerciseNumberDen;

                if (symbol == 0)
                {
                    var r = rnd.Next(3, 5);
                    symbol = r;
                }

                
                switch (symbol)
                {
                    case 1:                                                     //  <
                        var add = rnd.Next(0, (int)(excerciseNumber * multi));
                        wrongNumbers[i] = excerciseNumber * multi - add;
                       
                        break;

                    case 2:                                                     //  >
                        var sub = rnd.Next(0, (int)(nww-excerciseNumber * multi));
                        wrongNumbers[i] = excerciseNumber * multi + sub;
                        
                        break;

                    case 3:                                                     //  <=
                        var eqadd = rnd.Next(1, (int)(excerciseNumber * multi));
                        wrongNumbers[i] = excerciseNumber*multi - eqadd;
                        
                        break;

                    case 4:                                                     //  >=
                        var eqsub = rnd.Next(1, (int)(nww- excerciseNumber * multi));
                        wrongNumbers[i] = excerciseNumber*multi + eqsub;
                       
                        break;

                    default:
                        excercise = "Error";
                        break;
                }
            }
        }

        protected readonly int[] denominators = [4, 5, 6, 8, 10, 11, 12, 15 ];
        protected int NWW(int a, int b)
        {
            var nwd = Eukl(a, b);
            return (a * b) / nwd;
        }
        protected int Eukl(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                {
                    var buff = a % b;
                    a = buff;
                }
                else
                {
                    var buff = b % a;
                    b = buff;
                }
            }

            if (a == 0)
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
