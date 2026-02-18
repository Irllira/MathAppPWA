using FrontEnd.Components.Classes;
using FrontEnd.Components.Services.Contracts;
using MathApp.FrontEnd.Components.Classes;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Games.CommonDiv
{
    public partial class CommonDivBase :ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        protected bool ready = false;

        protected int excerciseNumber1;
        protected int excerciseNumber2;

        protected int anwserNWD;
        protected int anwserNWW;

        protected int numberNumbers1 = 0;
        protected List<int> divNumbers1 = new List<int>();
        protected List<int> userNumbers1 = new List<int>();

        protected int numberNumbers2 = 0;
        protected List<int> divNumbers2 = new List<int>();
        protected List<int> userNumbers2 = new List<int>();

        protected Euklides euklides = new Euklides();
        protected GamesBase gameBase = new GamesBase("Ułamki", "Gry");
        protected override void OnInitialized()
        {
            PrepareNewGame();
        }

        protected void PrepareNewGame()
        {
            divNumbers1 = new List<int>();
            divNumbers2 = new List<int>();
            userNumbers1 = new List<int>();
            userNumbers2 = new List<int>();

            Random rnd = new Random();
            do {
                excerciseNumber1 = rnd.Next(20, 120);
                excerciseNumber2 = rnd.Next(20, 120);
                anwserNWD = euklides.Eukl(excerciseNumber1, excerciseNumber2);
            } while (anwserNWD == 1 || excerciseNumber1==excerciseNumber2);

            divNumbers1.Add(excerciseNumber1);
            divNumbers2.Add(excerciseNumber2);

            userNumbers1.Add(0);
            numberNumbers1 = 0;

            userNumbers2.Add(0);
            numberNumbers2 = 0;
            anwserNWW = excerciseNumber2 * excerciseNumber1 / anwserNWD;

            ready = true;
        }
    }
}
