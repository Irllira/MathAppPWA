using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Games.Fractions.Operations
{
    public class FractionsOperationsBase : ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        [Parameter]
        public string operationType { get; set; }

        public bool ready=false;
        public bool diffden = false;

        public string type = "";
        public char sym;
        public int[] denominator = new int[2];
        public int[] numerator = new int[2];

        public int correctDen;
        public int correctNum;

        public string excercise = "";

        protected override void OnInitialized()
        {
            PrepareNewGame();
            ready = true;
        }

        protected void PrepareNewGame()
        {
            Random rnd = new Random();
            diffden = false;

            if (operationType == "mixed")
            {
                var r = rnd.Next(0, 3);
                switch(r)
                {
                    case 0:
                        type = "add";
                        break;
                    case 1:
                        type = "minus";
                        break;
                    case 2:
                        type = "multi";
                        break;
                    case 3:
                        type = "div";
                        break;
                }
            } else
            {
                type = operationType;
            }

            denominator = rnd.GetItems(denominators.ToArray(), 2);

            for (int i = 0; i < 2; i++)
            {
                numerator[i] = rnd.Next(1, denominator[i]);
                if (numerator[i] == 0)
                    numerator[i] = 1;
                checkSmallerFraction(i);
            }

            double check1;
            double check2;

            switch (type)
            {
                case "add":
                    sym = '+';
                    if (denominator[0] == denominator[1])
                    {
                        correctDen = denominator[0];
                        correctNum = numerator[0] + numerator[1];
                    }
                    else
                    {
                        diffden = true;
                        correctDen = NWW(denominator[0], denominator[1]);

                        var multi0 = correctDen / denominator[0];
                        var multi1 = correctDen / denominator[1];

                        correctNum = (numerator[0] * multi0) + (numerator[1] * multi1);
                    }
                    break;
                case "minus":
                    sym = '-';
                    check1 = (double)numerator[0] / denominator[0];
                    check2 = (double)numerator[1] / denominator[1];

                    if (check1 < check2)
                    {
                        var buff = numerator[0];
                        numerator[0] = numerator[1];
                        numerator[1] = buff;

                        buff = denominator[0];
                        denominator[0] = denominator[1];
                        denominator[1] = buff;
                    }


                    if (denominator[0] == denominator[1])
                    {
                        correctDen = denominator[0];
                        correctNum = numerator[0] - numerator[1];
                    }
                    else
                    {
                        diffden = true;
                        correctDen = NWW(denominator[0], denominator[1]);

                        var multi0 = correctDen / denominator[0];
                        var multi1 = correctDen / denominator[1];

                        correctNum = (numerator[0] * multi0) - (numerator[1] * multi1);
                    }

                    break;
                case "multi":
                    sym = '*';

                    correctDen = denominator[0] * denominator[1];
                    correctNum = numerator[0] * numerator[1];

                    break;
                case "div":
                    diffden = true;
                    sym = ':';
                    check1 = (double)numerator[0] / denominator[0];
                    check2 = (double)numerator[1] / denominator[1];

                    if (check1 < check2)
                    {
                        var buff = numerator[0];
                        numerator[0] = numerator[1];
                        numerator[1] = buff;

                        buff = denominator[0];
                        denominator[0] = denominator[1];
                        denominator[1] = buff;
                    }

                    correctDen = denominator[0] * numerator[1];
                    correctNum = numerator[0] * denominator[1];
                    break;
            }
        }

        protected void checkSmallerFraction(int i)
        {
            var nwd = Eukl(numerator[i], denominator[i]);
            if (nwd != 1)
            {
                //var b = ExcerciseDenominator[i] / ExcerciseNumerator[i];
                denominator[i] /= nwd;
                numerator[i] /= nwd;
                //checkSmallerFraction(i);
            }
            else
            {
                return;
            }
        }

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


        protected List<int> denominators = new List<int>() { 2, 3, 4, 5, 6, 8, 10 };

    }
}
