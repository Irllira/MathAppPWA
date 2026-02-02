using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Diagnostics.Metrics;

namespace FrontEnd.Components.Pages.Games.OperationsBase
{
    public class OperationsBase: ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        [Parameter]
        public required string operationType { get; set; }
        [Parameter]
        public required string numType { get; set; }


        protected int min;
        protected int max;

        protected bool ready = false;
        protected int excerciseNumber1;
        protected int excerciseNumber2;
        protected int symbol;
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

            if (operationType == "" || operationType == null || operationType == "4" || operationType == "mixed")
            {
                symbol = rnd.Next(0, 4);
            }
            else
            {
                symbol = Int32.Parse(operationType);
            }



            switch (numType)
            {
                case "easy":
                    excerciseNumber1 = rnd.Next(0, 10);
                    excerciseNumber2 = rnd.Next(0, 10);
                    min = 0;
                    max = 10;
                    break;
                case "natural":
                    if (symbol == 2 || symbol == 3)
                    {
                        excerciseNumber1 = rnd.Next(10, 20);
                        excerciseNumber2 = rnd.Next(1, 10);
                        min = 1;
                        max = 20;
                    }
                    else
                    {
                        excerciseNumber1 = rnd.Next(10, 100);
                        excerciseNumber2 = rnd.Next(1, 100);
                        min = 1;
                        max = 100;
                    }
                        break;
                case "minus":
                   
                    if (symbol == 2 || symbol == 3)
                    {
                        excerciseNumber1 = rnd.Next(-20, 20);
                        excerciseNumber2 = rnd.Next(-10, 10);
                        if (excerciseNumber1 > 0 && excerciseNumber2 > 0)
                        {
                            var min = rnd.Next(0, 3);
                            switch (min)
                            {
                                case 0:
                                    excerciseNumber1 *= -1;
                                    break;
                                case 1:
                                    excerciseNumber2 *= -1;
                                    break;
                                default:
                                    break;
                            }
                        }
                        min = -10;
                        max = 20;
                    }
                    else
                    {

                        excerciseNumber1 = rnd.Next(-50, 50);
                        excerciseNumber2 = rnd.Next(-50, 50);
                        min = -50;
                        max = 50;
                    }
                    break;

            }

            string ex1 = "";
            string ex2 = "";
            switch (symbol)
            {
                case 0:                                                     //  +
                    correctNumber = excerciseNumber1 + excerciseNumber2;
                    if (excerciseNumber1 < 0)
                    {
                        ex1 = " ( " + excerciseNumber1 + " ) ";
                    }
                    else
                    {
                        ex1 = excerciseNumber1 + "";
                    }
                    if (excerciseNumber2 < 0)
                    {
                        ex2 = " ( "+excerciseNumber2 + " ) ";
                    }else
                    {
                        ex2 = excerciseNumber2 + "";
                    }
                    excercise = "Podaj wynik działania " + ex1 + " + " + ex2 + " = ";

                    break;

                case 1:                                                     //  -                       
                    if (excerciseNumber1 < excerciseNumber2)
                    {
                        var ex = excerciseNumber2;
                        excerciseNumber2 = excerciseNumber1;
                        excerciseNumber1 = ex;
                     
                    }
                
                    correctNumber = excerciseNumber1 - excerciseNumber2;
                    if (excerciseNumber1 < 0)
                    {
                        ex1 = " ( " + excerciseNumber1 + " ) ";
                    }
                    else
                    {
                        ex1 = excerciseNumber1 + "";
                    }

                    if (excerciseNumber2 < 0)
                    {
                        ex2 = " ( " + excerciseNumber2 + " ) ";
                    }
                    else
                    {
                        ex2 = excerciseNumber2 + "";
                    }

                    excercise = "Podaj wynik działania " + ex1 + " - " + ex2 + " = ";  
                    break;


                case 2:                                                     //  *
                    correctNumber = excerciseNumber1 * excerciseNumber2;

                    if (excerciseNumber1 < 0)
                    {
                        ex1 = " ( " + excerciseNumber1 + " ) ";
                    }
                    else
                    {
                        ex1 = excerciseNumber1 + "";
                    }

                    if (excerciseNumber2 < 0)
                    {
                        ex2 = " ( " + excerciseNumber2 + " ) ";
                    }
                    else
                    {
                        ex2 = excerciseNumber2 + "";
                    }

                    excercise = "Podaj wynik działania " + ex1 + " * " + ex2 + " = ";

                    break;

                case 3:                                                     //  /   
                    if (excerciseNumber2 == 0)
                        excerciseNumber2 = 1;
                    var buff = excerciseNumber1 * excerciseNumber2;
                    correctNumber = excerciseNumber1;
                    excerciseNumber1 = buff;

                    if (excerciseNumber1 < 0)
                    {
                        ex1 = " ( " + excerciseNumber1 + " ) ";
                    }
                    else
                    {
                        ex1 = excerciseNumber1 + "";
                    }

                    if (excerciseNumber2 < 0)
                    {
                        ex2 = " ( " + excerciseNumber2 + " ) ";
                    }
                    else
                    {
                        ex2 = excerciseNumber2 + "";
                    }


                    excercise = "Podaj wynik działania " + ex1 + " : " + ex2 + " = ";

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
            int random = rnd.Next(min,max);
            int wrongNumber = 4;
            int wrongFirst = 0;


            if (numType == "minus" && correctNumber != 0)
            {
                wrongNumbers[0] = correctNumber * (-1);
               
                wrongFirst++;
                for (int i = wrongFirst; i < wrongNumber; i++)
                {
                    while (random == correctNumber)
                    {
                        random = rnd.Next(min, max);
                    }
                    wrongNumbers[i] = random;
                    if(random!=0)
                    {
                        i++;
                        wrongNumbers[i] = -1 * random;
                    }
                    random = rnd.Next(min, max);
                }
            }
            else
            {
                for (int i = wrongFirst; i < wrongNumber; i++)
                {
                    while (random == correctNumber)
                    {
                        random = rnd.Next(min, max);
                    }
                    wrongNumbers[i] = random;
                    random = rnd.Next(min, max);
                }
            }
        }

    }
}
