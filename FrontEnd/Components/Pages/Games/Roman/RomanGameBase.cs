using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Games.Roman
{
    public class RomanGameBase:ComponentBase
    {
        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        [Parameter]        
        public required string type { set; get; }

        protected bool ready = false;
        protected string exerciseNumber = "";
        
        protected string correctNumber="";
        protected string[] wrongAnwsers = { "","","" };

        protected override void OnInitialized()
        {
            PrepareNewGame();
        }
        protected void PrepareNewGame()
        {
            Random rnd = new Random();
            var ex = rnd.Next(1, 3000);
            if (type == "To")
            {
                exerciseNumber = ex.ToString();
                correctNumber = ToRoman(ex);

                for (int i = 0; i < wrongAnwsers.Length; i++)
                {
                    do
                    {
                        ex = rnd.Next(1, 3000);
                    } while (ex.ToString() == exerciseNumber);
                    wrongAnwsers[i] = ToRoman(ex);
                }

            }
            else
            {
                exerciseNumber = ToRoman(ex);
                correctNumber = ex.ToString();

                for (int i = 0; i < wrongAnwsers.Length; i++)
                {
                    do
                    {
                        ex = rnd.Next(1, 3000);
                    } while (ex.ToString() == exerciseNumber);
                    wrongAnwsers[i] = ex.ToString();
                }
            }  
            ready = true;
        }


        protected string ToRoman(int num)
        {
            string s = "";
            var th = (num - (num % 1000)) / 1000;
            if (th != 0)
            {
                s = s + romThousnds[th - 1];
                num = num - th * 1000;
            }
            var hn = (num - (num % 100)) / 100;
            if (hn != 0)
            {
                s = s + romHundreds[hn - 1];
                num = num - hn * 100;
            }
            var ten = (num - (num % 10)) / 10;
            if (ten != 0)
            {
                s = s + romTens[ten - 1];
                num = num - ten * 10;
            }
            var one = num;
            if (one != 0)
            {
                s = s + romOnes[one - 1];
            }
            return s;
        }

        protected string[] romThousnds = {"M","MM","MMM" };
        protected string[] romHundreds = { "C", "CC", "CCC" , "CD", "D","DC", "DCC","DCCC","CM"};
        protected string[] romTens = { "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
        protected string[] romOnes = { "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };




    }
}
