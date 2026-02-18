using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Games.Percent
{
    public class PrecentBase : ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        [Parameter]
        public required string type { get; set; }

        protected int excerciseNumber1 = 0;
        protected double procent = 0;
        protected int sym;
        protected double excerciseNumber2;
        protected bool ready=false;
        protected override void OnInitialized()
        {
            PrepareNewGame();
        }
        protected void PrepareNewGame()
        {
            Random rnd = new Random();
            if (type=="proc") {
                excerciseNumber1 = rnd.Next(1, 50);
                procent = rnd.Next(1, 91);
                sym = rnd.Next(0, 2);

                switch (sym)
                {
                    case 0:
                        procent = 100 + procent;
                        break;
                    case 1:
                        procent = 100 - procent;
                        break;
                }
                excerciseNumber2 = (double)procent * excerciseNumber1 / 100;
            }else
            {
                excerciseNumber1 = rnd.Next(1, 50);
                excerciseNumber2 = rnd.Next(1, 50);

                procent = (excerciseNumber2 * 100) / excerciseNumber1;
            }

            ready = true;

        }
    }
}
