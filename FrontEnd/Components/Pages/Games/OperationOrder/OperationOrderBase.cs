using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Games.OperationOrder
{
    public class OperationOrderBase : ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        protected bool ready = false;

        protected string[] operations = { "", "", "" };
        public int[] numbers = {0,0,0,0 };
        public int value;

        public string excercise = "";
        public string[] anwsers = new string[3];
        protected int[] anwsersType = new int[3];

        protected override void OnInitialized()
        {
            PrepareNewGame();
        }

        protected void PrepareNewGame()
        {
            excercise = "";
            numbers = [0,0,0,0];

            Random rnd = new Random();
            for (int i = 0; i < operations.Length; i++)
            {
                var x = rnd.Next(0, 4);
                switch (x)
                {
                    case 0:             // +
                        operations[i] = "+";
                        anwsersType[i] = 0;
                        break;
                    case 1:             // -
                        operations[i] = "-";
                        anwsersType[i] = 0;
                        break;
                    case 2:             // *
                        operations[i] = "*";
                        anwsersType[i] = 2;
                        if (i != 0 && anwsersType[i - 1] == 2)
                        {
                            anwsersType[i] = 1;
                        }
                        else
                        {
                            anwsersType[i] = 2;
                        }

                        break;
                    case 3:             // :
                        operations[i] = ":";
                        if (i != 0 && anwsersType[i - 1] == 2)
                        {
                            anwsersType[i] = 1;
                        }
                        else
                        {
                            anwsersType[i] = 2;
                        }

                        break;
                }
            }
            var bracket = rnd.Next(0, 8);
            if (bracket < 3)
            {
                anwsersType[bracket] = 5;
            }

            value = rnd.Next(50, 300);
            var v = value;
            //GetNumbers();

            for(int i=0;i<numbers.Length;i++)
            {
                numbers[i] = rnd.Next(1,120);
            }

            for (int i = 0; i < operations.Count(); i++)
            {
                var s = "";
                if (bracket == i)
                {
                    s = " ( " + numbers[i] + " " + operations[i] + " " + numbers[i + 1] + " ) ";
                    excercise += "( " + numbers[i] + " " + operations[i] + " ";

                }
                else
                {
                    s = numbers[i] + " " + operations[i] + " " + numbers[i + 1];
                    if (bracket == i - 1)
                    {
                        excercise += numbers[i] + " ) " + operations[i] + " ";
                    }
                    else
                    {
                        excercise += numbers[i] + " " + operations[i] + " ";

                    }

                }
                anwsers[i] = s;
            }

            excercise += numbers[3];

            if (bracket == 2)
                excercise += " )";
            ready = true;
        }
       
        private void GetNumbers()
        {
            Random rnd = new Random();

            string[] ops = ["", "", ""];
            int help1 = 0;
            int help1Inx = -2;

            int help2 = 0;
            int help2Inx = -2;

            for (int i = 0; i < operations.Length; i++)
            {
                if (anwsersType[i] == 5)
                {
                    if (operations[i] == ":")
                    {
                        var x = rnd.Next(1, 10);
                        var y = rnd.Next(1, 10);
                        numbers[i] = x * y;
                        if (x < y)
                        {
                            numbers[i + 1] = x;
                        }
                        else
                        {
                            numbers[i + 1] = y;
                        }
                        help1 = numbers[i] / numbers[i + 1];
                        help1Inx = i;
                    }

                    if (operations[i] == "-")
                    {
                        var x = rnd.Next(1, 50);
                        var y = rnd.Next(1, 50);
                        if (x < y)
                        {
                            numbers[i] = y;
                            numbers[i + 1] = x;
                        }
                        else
                        {
                            numbers[i] = x;
                            numbers[i + 1] = y;
                        }
                        help1 = numbers[i] - numbers[i + 1];
                        help1Inx = i;

                    }
                    if (operations[i] == "+")
                    {
                        var x = rnd.Next(1, 50);
                        var y = rnd.Next(1, 50);
                        numbers[i] = x;
                        numbers[i + 1] = y;

                        help1 = numbers[i] + numbers[i + 1];
                        help1Inx = i;

                    }
                    if (operations[i] == "*")
                    {
                        var x = rnd.Next(1, 50);
                        var y = rnd.Next(1, 10);
                        numbers[i] = x;
                        numbers[i + 1] = y;

                        help1 = numbers[i] * numbers[i + 1];
                        help1Inx = i;

                    }
                }
                else
                {
                    ops[i] = operations[i];
                }
            }

            for (int i = 0; i < operations.Length; i++)
            {
                if (anwsersType[i] == 2)
                {
                    if (operations[i] == ":")
                    {
                        if (help1 == 0 || (help1Inx != i - 1) && (help1Inx != i + 1))
                        {
                            var x = rnd.Next(1, 10);
                            var y = rnd.Next(1, 10);
                            numbers[i] = x * y;
                            if (x < y)
                            {
                                numbers[i + 1] = x;
                            }
                            else
                            {
                                numbers[i + 1] = y;
                            }
                        }
                        else
                        {
                            if (help1Inx == i - 1)
                            {
                                var x = rnd.Next(1, 10);

                                while (help1 % x != 0)
                                {
                                    x = rnd.Next(1, 10);
                                }
                                numbers[i] = x;
                            }
                            if (help1Inx == i + 1)
                            {
                                var x = rnd.Next(1, 10);
                                numbers[i] = help1 * x;
                            }
                        }
                    }
                    if (operations[i] == "*")
                    {
                        var y = rnd.Next(1, 10);
                        numbers[i] = y;
                    }
                }
            }

            for (int i = 0; i < operations.Length; i++)
            {
                if (anwsersType[i] == 1)
                {
                    if (operations[i] == ":")
                    {
                        if (help1 == 0 || (help1Inx != i - 1) && (help1Inx != i + 1) || (help2Inx != i - 1) && (help2Inx != i + 1))
                        {
                            var x = rnd.Next(1, 10);
                            var y = rnd.Next(1, 10);
                            numbers[i] = x * y;
                            if (x < y)
                            {
                                numbers[i + 1] = x;
                            }
                            else
                            {
                                numbers[i + 1] = y;
                            }
                        }
                        else
                        {
                            if (help1Inx == i - 1)
                            {
                                var x = rnd.Next(1, 10);

                                while (help1 % x != 0)
                                {
                                    x = rnd.Next(1, 10);
                                }
                                numbers[i] = x;
                            }
                            if (help1Inx == i + 1)
                            {
                                var x = rnd.Next(1, 10);
                                numbers[i] = help1 * x;
                            }
                            /////////

                            if (help2Inx == i - 1)
                            {
                                var x = rnd.Next(1, 10);

                                while (help2 % x != 0)
                                {
                                    x = rnd.Next(1, 10);
                                }
                                numbers[i] = x;
                            }
                            if (help2Inx == i + 1)
                            {
                                var x = rnd.Next(1, 10);
                                numbers[i] = help2 * x;
                            }
                        }


                        help2 = numbers[i] / numbers[i + 1];
                        help2Inx = i;
                    }
                    if (operations[i] == "*")
                    {
                        var y = rnd.Next(1, 10);
                        numbers[i] = y;


                        help2 = numbers[i] * numbers[i + 1];
                        help2Inx = i;
                    }
                }
            }

        }

    }  
}
