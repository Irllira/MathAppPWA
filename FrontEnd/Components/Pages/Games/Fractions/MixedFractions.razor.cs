using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Games.Fractions
{
    public class MixedFractionsBase : ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        public string type = "";

        public bool ready = false;

        public int ExcerciseNumerator;
        public int ExcerciseDenominator;
        public int ExcerciseFullNumber;


        public int AnwserNumerator;
        public int AnwserDenominator;
        public int AnwserFullNumber;

        protected override void OnInitialized()
        {
            PrepareNewGame();
        }

        protected void PrepareNewGame()
        {
            Random rnd = new Random();
           
            ExcerciseDenominator = rnd.Next(2,20);
            ExcerciseNumerator = rnd.Next(1,ExcerciseDenominator);

            ExcerciseFullNumber = rnd.Next(1, 5);

            var t = rnd.Next(0, 2);
            if (t == 0)
            {
                type = "from";
                AnwserFullNumber = 0;
                AnwserNumerator = ExcerciseNumerator + (ExcerciseDenominator * ExcerciseFullNumber);
                AnwserDenominator = ExcerciseDenominator;
            }
            else
            {
                type = "to";
                AnwserNumerator = ExcerciseNumerator;
                ExcerciseNumerator += (ExcerciseDenominator* ExcerciseFullNumber);

                AnwserFullNumber = ExcerciseFullNumber;
                ExcerciseFullNumber = 0;
                AnwserDenominator = ExcerciseDenominator;
            }

            ready = true;
        }
    }
}
