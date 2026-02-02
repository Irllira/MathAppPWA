using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Games.Fractions
{
    public class FractionsDecBase: ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        [Parameter]
        public required string type { get; set; }

        public bool ready = false;

        public double numerator;
        public double denominator;
        public string excercise ="";
        public double excerciseNumber;

        public double correctNumber;
        public List<double> wrongAnwsers = new List<double>();
        public string correctAnwser="";
        public string correctNumAnwser = "";
        public string correctDenAnwser = "";

        public List<string> wrongNumAnwsers = new List<string>();
        public List<string> wrongDenAnwsers = new List<string>();


        protected override void OnInitialized()
        {
            PrepareNewGame();
        }

        protected void PrepareNewGame()
        {
            wrongNumAnwsers = new List<string>();
            wrongDenAnwsers = new List<string>();

            wrongAnwsers = new List<double>();
            Random rnd = new Random();
            if (type == "toDec")
            {
                var buff = rnd.GetItems(denominators.ToArray(), 1);
                denominator = buff[0];
                numerator = rnd.Next(1, buff[0]);

                correctNumber =numerator / denominator;

                for (int i = 0; i < 4; i++)
                {
                    double check;
                    do
                    {
                        var dec = rnd.GetItems(denominators.ToArray(), 1);
                        double num = rnd.Next(1, dec[0]);
                        check = (double) num / dec[0];
                    } while (check == correctNumber);
                   
                    wrongAnwsers.Add(check);
                }
                ready= true;
            }else
            {
                var buff = rnd.GetItems(denominators.ToArray(), 1);
                denominator = buff[0];
                numerator = rnd.Next(1, buff[0]);

                excerciseNumber = numerator / denominator;
                correctNumAnwser = numerator + "";
                correctDenAnwser = denominator+"";
                for (int i = 0; i < 4; i++)
                {
                    string check="";
                    int[] dec;// = rnd.GetItems(denominators.ToArray(), 1);
                    double num;// = rnd.Next(1, dec[0]);
                    do
                    {
                        dec = rnd.GetItems(denominators.ToArray(), 1);
                        num = rnd.Next(1, dec[0]);
                        check = num+" / "+dec[0];
                    } while ((double)num / dec[0] == excerciseNumber);

                    wrongNumAnwsers.Add(num+"");
                    wrongDenAnwsers.Add(dec[0] +"");

                }
                ready = true;
            }
        }

        protected readonly List<int> denominators = new List<int>() {2,4,5,8,10,20,25,50,100};
    }
}
