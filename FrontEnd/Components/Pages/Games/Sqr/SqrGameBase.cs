using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading;

namespace FrontEnd.Components.Pages.Games.Sqr
{
    public class SqrGameBase: ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        [Parameter]
        public required string numbersType { get; set; }

        [Parameter]
        public required string operationType { get; set; }

        protected string roundType="";

        protected bool ready = false;
        protected int exerciseNumber;
        protected int power;
        protected int correctNumber;

        protected int exerciseDenumerator;
        protected int correctDenumerator;


        protected List<int> wrongAnwsers = new List<int>();
        protected List<int> wrongAnwsersDen = new List<int>();


        protected override void OnInitialized()
        {
            PrepareNewGame();
        }

        protected void PrepareNewGame()
        {
            Random rnd = new Random();
            if(operationType == "mix")
            {
                var n = rnd.Next(0, 2);
                if(n==0)
                {
                    roundType = "sqr";
                }else
                {
                    roundType = "root";
                }
            }else
            {
                roundType = operationType;
            }

            wrongAnwsersDen = new List<int>();
            wrongAnwsers = new List<int>();

            correctNumber = 1;
            exerciseNumber = 1;

            power = rnd.Next(2, 4);

            if (numbersType == "fraction")
            {
                exerciseDenumerator = 1;
                correctDenumerator = 1;

                if (roundType == "sqr")
                {
                    exerciseDenumerator = rnd.Next(2, 10);
                    exerciseNumber = rnd.Next(1, exerciseDenumerator);
                    for (int i = 0; i < power; i++)
                    {
                        correctNumber *= exerciseNumber;
                        correctDenumerator *= exerciseDenumerator;
                    }
                    GetIncorrectsSqrFraction();
                }else
                {
                    correctDenumerator = rnd.Next(2, 10);
                    correctNumber = rnd.Next(1, correctDenumerator);
                    for (int i = 0; i < power; i++)
                    {
                        exerciseNumber *= correctNumber;
                        exerciseDenumerator *= correctDenumerator;
                    }
                    GetIncorrectsRootFraction();
                }
            }
            else
            {
                if (roundType == "sqr")
                {
                    exerciseNumber = rnd.Next(1, 10);
                    for (int i = 0; i < power; i++)
                        correctNumber *= exerciseNumber;

                    GetIncorrectsSqr();
                }
                else
                {
                    correctNumber = rnd.Next(1, 10);
                    for (int i = 0; i < power; i++)
                        exerciseNumber *= correctNumber;

                    GetIncorrectsRoot();
                }

            }
            ready = true;
        }

        protected void GetIncorrectsSqr()
        {
            Random rnd = new Random();

            AddWrongAnwser(exerciseNumber * power);

            if (power == 2)
            {
                AddWrongAnwser(exerciseNumber * exerciseNumber * exerciseNumber);
            }
            else
            {
                AddWrongAnwser(exerciseNumber * exerciseNumber);
            }

            AddWrongAnwser(exerciseNumber);

            var num = rnd.Next(4, 500);
            while (num == correctNumber)
            {
                num = rnd.Next(4, 500);
            }
            wrongAnwsers.Add(num);

            wrongAnwsers.OrderBy(_ => rnd.Next()).ToList();
        }


        protected void GetIncorrectsSqrFraction()
        {
            Random rnd = new Random();

            if (correctNumber != exerciseNumber)
            {
                wrongAnwsers.Add(exerciseNumber);
            }else
            {
                wrongAnwsers.Add(exerciseNumber+power);

            }
            wrongAnwsersDen.Add(correctDenumerator);

            if(correctNumber==exerciseNumber)
            wrongAnwsers.Add(correctNumber);
            wrongAnwsersDen.Add(exerciseDenumerator);

            AddWrongAnwser(exerciseNumber * power);
            wrongAnwsersDen.Add(exerciseDenumerator * power);

            AddWrongAnwser(exerciseNumber*exerciseDenumerator);
            wrongAnwsersDen.Add(exerciseNumber * exerciseDenumerator);

            wrongAnwsers.OrderBy(_ => rnd.Next()).ToList();
        }

        protected void GetIncorrectsRoot()
        {
            Random rnd = new Random();

            AddWrongAnwser(exerciseNumber);

            AddWrongAnwser(exerciseNumber/power);

            var a = rnd.Next(4, 10);
            for (int i = 0; i < 2; i++)
            {
                while (a == correctNumber)
                {
                    a = rnd.Next(4, 10);
                }
                wrongAnwsers.Add(a);
            }

            wrongAnwsers.OrderBy(_ => rnd.Next()).ToList();
        }

        protected void GetIncorrectsRootFraction()
        {
            Random rnd = new Random();
            if (correctNumber != exerciseNumber)
            {
                wrongAnwsers.Add(exerciseNumber);
            }else
            {
                wrongAnwsers.Add(exerciseNumber * power);
            }
            wrongAnwsersDen.Add(correctDenumerator);


            wrongAnwsers.Add(correctNumber);
            wrongAnwsersDen.Add(exerciseDenumerator);

            AddWrongAnwser(exerciseNumber / power);
            wrongAnwsersDen.Add(exerciseDenumerator / power);

            AddWrongAnwser(exerciseNumber * power);
            wrongAnwsersDen.Add(exerciseNumber * power);

            wrongAnwsers.OrderBy(_ => rnd.Next()).ToList();
        }


        protected void AddWrongAnwser(int num)
        {
            Random rnd = new Random();

            if (num != correctNumber)
            {
                wrongAnwsers.Add(num);
            }
            else
            {
                var x = rnd.Next(1, 10);
                wrongAnwsers.Add(num + x);
            }
        }

      
    }
}
