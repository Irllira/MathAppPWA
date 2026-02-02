using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Games.CommonDiv
{
    public class CommonDivBase :ComponentBase
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
                anwserNWD = Eukl(excerciseNumber1, excerciseNumber2);
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

    }
}
