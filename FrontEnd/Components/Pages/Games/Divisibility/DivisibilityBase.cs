using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Games.Divisibility
{
    public class DivisibilityBase : ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        protected bool ready = false;
        protected int excerciseNumber;
        protected List<string> answer = new List<string>();
        protected List<string> chosenAnswers = new List<string>();

        protected List<string> allAnswers = new List<string>() { "2", "3", "4", "5", "9", "10", "Brak" };


        protected List<string> bgstyle = new List<string> { "gamebtns" };


        protected override void OnInitialized()
        {
            PrepareNewGame();
            ready = true;
        }

        protected void PrepareNewGame()
        {
            bgstyle = new List<string> { "gamebtns" };
            chosenAnswers = new List<string> { };
            answer = new List<string> { };

            allAnswers = new List<string> { "2", "3", "4", "5", "9", "10", "Brak" };
            Random rnd = new Random();
            excerciseNumber = rnd.Next(4, 70);

            for (int i = 0; i < allAnswers.Count-1; i++)
            {
                var a = 0;
                Int32.TryParse(allAnswers[i], out a);
                if (excerciseNumber % a == 0)
                {
                    answer.Add(a.ToString());
                }               
                bgstyle.Add("gamebtns");
            }
            if(answer.Count()==0)
            {
                answer.Add(allAnswers.Last());
            }

        }
    }
}
