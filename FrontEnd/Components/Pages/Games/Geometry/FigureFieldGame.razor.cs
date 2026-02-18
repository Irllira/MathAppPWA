using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Games.Geometry
{
    public class FigureFieldBase : ComponentBase
    {

        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        protected bool ready =false;
        protected string phase = "";

        protected string imgPath = "";
        protected string figure ="";

        protected int[] numbers = new int[3];
        protected double finalAnwser;
        protected override void OnInitialized()
        {
            PrepareNewGame();
        }

        protected void PrepareNewGame()
        {
            Random rnd = new Random();

            for (int i = 0; i < 3; i++)
            {
                var r = rnd.Next(1, 14);
                numbers[i] = r;
            }

            var fig = rnd.Next(0, 7);
            imgPath = "/Assets/";           

            switch (fig)
            {
                case 0:
                    figure = "TriangleReg";
                    imgPath = imgPath + "TrojkatPole.png";
                    finalAnwser = (double)numbers[0] * numbers[1] / 2;
                    break;

                case 1:
                    figure = "Triangle90";
                    imgPath = imgPath + "TrojkatProstPole.png";
                    finalAnwser = (double)numbers[0] * numbers[1] / 2;
                    break;

                case 2:
                    figure = "Rownoleglobok";
                    imgPath = imgPath + "RownolPole.png";
                    finalAnwser = (double)numbers[0] * numbers[1];
                    break;

                case 3:
                    figure = "Kwadrat";
                    imgPath = imgPath + "BasicKwadrat.png";
                    finalAnwser = (double)numbers[0] * numbers[0];
                    break;

                case 4:
                    figure = "Prostokat";
                    imgPath = imgPath + "BasicProstokat.png";
                    finalAnwser = (double)numbers[0] * numbers[1];
                    break;
                case 5:
                    figure = "Rab";
                    imgPath = imgPath + "RabPole.png";
                    finalAnwser = (double)numbers[0] * numbers[1]/2;
                    break;
                case 6:
                    figure = "Trapez";
                    imgPath = imgPath + "TrapezPole.png";
                    finalAnwser = ((double)numbers[0] + numbers[1]) *numbers[2] / 2;
                    break;
            }

            phase = "ChooseFormula";
            ready = true;
        }

        protected readonly Formula[] formulas = [new Formula("Rownoleglobok", "a*h", ""),
        new Formula("Triangle", "a*h", "2"), new Formula("Kwadrat", "a*a", ""),new Formula("Prostokat", "a*b", ""),
        new Formula("Rab", "e*f", "2"),new Formula("Trapez", "(a+b)*h", "2")];
    }


    public class Formula
    {
        public string name;
        public string numerator;
        public string denominator;
        public int symbolsNmbr;
        public char[] symbols = new char[3];

        public Formula()
        {
            name = "";
            numerator = "";
            denominator = "";
        }


        public Formula(string nm, string num, string denom)
        {
            name = nm;
            numerator = num;
            denominator = denom;

            symbols = new char[3];
            symbolsNmbr =0;

            foreach(char c in num)
            {
                if(c!='*'&& c != '+' && c != '(' && c != ')')
                {
                    symbols[symbolsNmbr] = c;
                    symbolsNmbr++;
                }
            }
        }
    }
}
