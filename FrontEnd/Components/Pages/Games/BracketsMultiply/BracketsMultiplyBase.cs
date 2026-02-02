using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Games.BracketsMultiply
{
    public class BracketsMultiplyBase : ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        protected bool ready = false;
        protected List<int> excerciseNumbers = new List<int>();
        protected List<int> FinalNumbers = new List<int>();

        protected List<char> symbols = new List<char>();
        protected List<char> symbolsMid = new List<char>();
        protected List<char> symbolsFinal = new List<char>();


        protected string excercise = "";
        protected override void OnInitialized()
        {
            PrepareNewGame();
        }

        protected void PrepareNewGame()
        {
            excercise = "";
            Random rnd = new Random();
            symbolsMid = new List<char>();
            symbolsFinal = new List<char>();
            excerciseNumbers = new List<int>();
            symbols = new List<char>();

            for (int i = 0; i < 4; i++)
            {
                var a = rnd.Next(1, 11);
                excerciseNumbers.Add(a);
            }
            for (int i = 0; i < 2; i++)
            {
                var a = rnd.Next(0, 2);
                if (a == 0)
                {
                    symbols.Add('+');
                }
                else
                {
                    symbols.Add('-');
                }
            }
            excercise = "( " + excerciseNumbers[0] + "x " + symbols[0] + " " + excerciseNumbers[1] + " ) * ( "
                + excerciseNumbers[2] + "x " + symbols[1] + " " + excerciseNumbers[3] + " ) ";


            symbolsMid.Add(symbols[0]);
            symbolsMid.Add(symbols[1]);

            if (symbols[0] == symbols[1])
            {
                symbolsMid.Add('+');
            }
            else
            {
                symbolsMid.Add('-');
            }

            FinalNumbers.Add(excerciseNumbers[0] * excerciseNumbers[2]);

            int prt1;
            int prt2;

            if (symbolsMid[0] == '+')
            {
                prt1 = (excerciseNumbers[1] * excerciseNumbers[2]);
            }
            else
            {
                prt1 = 0 - (excerciseNumbers[1] * excerciseNumbers[2]);
            }
            if (symbolsMid[1] == '+')
            {
                prt2 = (excerciseNumbers[0] * excerciseNumbers[3]);
            }
            else
            {
                prt2 = 0 - (excerciseNumbers[0] * excerciseNumbers[3]);
            }

            FinalNumbers.Add(prt1 + prt2);

            FinalNumbers.Add(excerciseNumbers[1] * excerciseNumbers[3]);

            if (FinalNumbers[1]>0)
            {
                symbolsFinal.Add('+');
            }else
            {
                symbolsFinal.Add('-');
            }
           
            symbolsFinal.Add(symbolsMid[2]);
            ready = true;
        }
    }
}
