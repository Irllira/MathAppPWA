using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.Games.Geometry
{
    public class PositionBase: ComponentBase
    {

        [Inject]
        protected IUserProgressService userProgressService { get; set; }

        protected bool ready = false;
        protected string type="";

        protected string figure1 = "";
        protected string figure2 = "";

        protected string excercise1 = "";
        protected string excercise2 = "";

        protected string img1Name = "";
        protected string img2Name = "";


        protected override void OnInitialized()
        {
            //base.OnInitialized();
            PrepareNewGame();
        }

        protected void PrepareNewGame()
        {
            Random rnd = new Random();
            figure1 = rnd.GetItems(figures, 1)[0];
            do
            {
                figure2 = rnd.GetItems(figures, 1)[0];

            } while (figure2 == figure1);
            type = rnd.GetItems(types, 1)[0];

            img1Name = GetImgName(figure1);
            img2Name = GetImgName(figure2);

            excercise1 = figure1 + " jest ";
            Grammar();
            ready = true;
        }

        protected void Grammar()
        {
            if (type == "nad" || type == "pod")
            {
                switch (figure2)
                {
                    case "Koło":
                        excercise2 = " kołem";
                        break;
                    case "Prostokąt":
                        excercise2 = " prostokątem";
                        break;
                    case "Kwadrat":
                        excercise2 = " kwadratem";
                        break;
                    case "Trójkąt":
                        excercise2 = "trójkątem";
                        break;
                }
                return;
            }


            switch (figure2)
            {
                case "Koło":
                    excercise2 = " koła";
                    break;
                case "Prostokąt":
                    excercise2 = " prostokąta";
                    break;
                case "Kwadrat":
                    excercise2 = " kwadratu";
                    break;
                case "Trójkąt":
                    excercise2 = "trójkąta";
                    break;
            }         
        }


        protected string GetImgName(string figure)
        {
            switch (figure)
            {
                case "Koło":
                    return "BasicKolo.png";
                case "Prostokąt":
                    return "BasicProstokat.png";
                case "Kwadrat":
                    return "BasicKwadrat.png";
                case "Trójkąt":
                    return "BasicTrojkat.png";
            }
            return "";
        }


        protected readonly string[] figures = ["Koło", "Prostokąt", "Kwadrat", "Trójkąt"];
    
        protected readonly string[] types = ["nad","pod","na lewo od", "na prawo od", "na skos od"];

    }
}
