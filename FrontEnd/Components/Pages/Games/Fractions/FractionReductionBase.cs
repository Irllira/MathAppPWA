using FrontEnd.Components.Classes;
using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Games.Fractions
{
    public class FractionReductionBase : ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        public bool ready = false;
        public string part = "multi";

        public int numerator;
        public int denominator;

        public int multiply;
        public string correctNumber="";
        public List<int> wrongMultiply = new List<int>();

        protected Euklides euklides = new Euklides();
        protected override void OnInitialized()
        {
            PrepareNewGame();
        }

        protected void PrepareNewGame()
        {
            wrongMultiply = new List<int>();
            Random rnd = new Random();

            var buff = rnd.GetItems(denominators.ToArray(), 1);
            denominator = buff[0];
            numerator = rnd.Next(1, buff[0]);
            if (numerator == 0)
                numerator = 1;
            checkSmallerFraction();

            
            multiply = rnd.Next(2, 11);
            denominator*= multiply;
            numerator *= multiply;
            multiply = euklides.Eukl(numerator, denominator);

            correctNumber = numerator/multiply + " / " + denominator/multiply;

            for (int i = 0; i < 3; i++)
            {
                int mult;
                do {
                    mult = rnd.Next(2, 11);
                } while (mult==multiply || (denominator%mult==0 && numerator%mult==0));
                wrongMultiply.Add(mult);
            }
            ready = true;
        }

        /*protected void checkBiggerMulti()
        {
            for(int i = 10;i>multiply;i--)
            {
                if(ExcerciseNumerator%i==0 && ExcerciseDenominator%i==0)
                {
                    multiply = i;
                    return;
                }
            }
        }*/

        protected void checkSmallerFraction()
        {
            if(denominator%numerator==0 && numerator!=1)
            {
                var b = denominator / numerator;
                denominator /= b;
                numerator /= b;
                checkSmallerFraction();
            }
            else
            {
                return;
            }
        }

        protected readonly List<int> denominators = new List<int>() { 2, 3, 4, 5, 6, 8, 10};
        
    
    }
}
