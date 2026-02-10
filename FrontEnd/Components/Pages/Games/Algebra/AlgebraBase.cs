using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Games.Algebra
{
    public class AlgebraBase : ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        protected bool ready = false;
        protected List<int> excerciseNumbers = new List<int>();
        protected int x;
        protected int FinalNumber;

        protected char symbol;
        //protected List<char> symbolsMid = new List<char>();
        //protected List<char> symbolsFinal = new List<char>();


        protected string excercise = "";
        protected override void OnInitialized()
        {
            PrepareNewGame();
        }

        
        protected void PrepareNewGame()
        {
            int a;
            excercise = "";
            Random rnd = new Random();
            
            excerciseNumbers = new List<int>(); 

            x = rnd.Next(2, 20);

            for (int i = 0; i < 2; i++)
            {
                a = rnd.Next(1, 11);
                excerciseNumbers.Add(a);
            }
            
            a = rnd.Next(0, 2);
            if (a == 0)
            {
                symbol = '+';
                FinalNumber = excerciseNumbers[0] * x + excerciseNumbers[1];
            }
            else
            {
                symbol = '-';
                FinalNumber = excerciseNumbers[0] * x - excerciseNumbers[1];
            }

            excercise = excerciseNumbers[0] + "x " + symbol + " " + excerciseNumbers[1];
           
            ready = true;
        }
    }
}
