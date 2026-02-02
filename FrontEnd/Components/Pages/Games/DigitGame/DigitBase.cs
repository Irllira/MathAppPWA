using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System;

namespace FrontEnd.Components.Pages.Games.DigitGame
{
    public class DigitBase : ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        protected bool ready = false;
        protected int excerciseNumber;
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
            int[] l = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9];
            int[] w = new int[4];

            w = rnd.GetItems(l, 4);


            excerciseNumber = w[0] * 1000 + w[1] * 100 + w[2] * 10 + w[3];
            symbol = rnd.Next(0, 4);

            switch (symbol)
            {
                case 0:                                                     //  jedności
                    correctNumber = excerciseNumber % 10;
                    excercise = "Podaj cyfrę jedności liczby " + excerciseNumber;

                    break;

                case 1:                                                     //  dziesiątek   
                    correctNumber = ((excerciseNumber % 100)- (excerciseNumber % 10))/10;
                    excercise = "Podaj cyfrę dziesiątek liczby " + excerciseNumber;

                    break;


                case 2:                                                     //  setek   
                    correctNumber = ((excerciseNumber%1000)- (excerciseNumber%100))/100;
                    excercise = "Podaj cyfrę setek liczby " + excerciseNumber;

                    break;

                case 3:                                                     //  tysięcy   
                    correctNumber = (excerciseNumber - (excerciseNumber%1000))/1000;
                    excercise = "Podaj cyfrę tysięcy liczby " + excerciseNumber;

                    break;
                default:
                    excercise = "Error";
                    break;
            }
           
            for (int i = 0; i < 4; i++)
            {
                if (w[i] == correctNumber)
                {
                    var rand = rnd.Next(1, 5);
                    if(correctNumber>=5)
                    {
                        wrongNumbers[i] = w[i]-rand;
                    }else
                    {
                        wrongNumbers[i] = w[i] + rand;
                    }
                   
                }
                else
                {
                    wrongNumbers[i] = w[i];
                    //random = rnd.Next(excerciseNumber1, excerciseNumber1 + 10);
                }
            }
            rnd.Shuffle(wrongNumbers);
        }

     
    }
}
