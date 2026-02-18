using MathApp.Enteties;
//using MathEducationWebApp.Migrations;

namespace MathEducationWebApp.Enteties
{
    public class DataSeeder
    {
      //  private readonly DataBase _db;

       // public DataSeeder(DataBase db)
       // {
       //     _db = db;
       // }

        public static async Task Seed(DataBase context)
        {
            await Task.Delay(10);
            if (context.Database.CanConnect())
            {
                if(!context.educationLevels.Any())
                {
                    var educationLevel = EdLevels();
                    context.educationLevels.AddRange(educationLevel);
                    //context.educationLevels.in
                    context.SaveChanges();
                }
            }
            return;
        }
        
        private static IEnumerable<EducationLevel> EdLevels()
        {
            var levels = new List<EducationLevel>() {
            new EducationLevel()
            {
                //Id = 1,
                name = "Klasy Jeden-Trzy",
                Units = new List<Unit>()
                {
                    new Unit()
                    {
                     //   Id =1,
                        name= "Podstawy Liczb",
                        description= "Podstawowy Liczb, Liczenie w przód i tył, Większe i mniejsze",
                        educationLevelId = 1
                    },

   
                    new Unit()
                    {
                       // Id = 2,
                        name = "Podstawowe działania matematyczne",
                        description= "Dodawanie, Odejmowanie, Mnożenie, Dzielenie",
                        educationLevelId = 1
                    },
                   
                    new Unit()
                    {
                       // Id = 3,
                        name = "Podstawy figur geometrycznych",
                        description ="Rozpoznawanie figur geometrycznych, liczenie obwodu",
                        educationLevelId = 1
                    },
                }
            },
            new EducationLevel()
            {
                //Id = 2,
                name = "Klasy Cztery-Sześć",
                Units = new List<Unit>()
                {
                    new Unit()
                    {
                     //   Id =1,
                        name = "Liczby Naturalne",
                        educationLevelId = 2
                    },

                    new Unit()
                    {
                     //  Id = 2,
                        name = "Zaokrąglanie Liczb Naturalnych",
                        educationLevelId = 2
                    },
                    new Unit()
                    {
                       // Id = 3,
                        name = "Liczby Rzymskie",
                        educationLevelId = 2
                    },
                    new Unit()
                    {
                       // Id = 4,
                        name = "Dodawanie i Odejmowanie Pisemne",
                        educationLevelId = 2
                    },
                }
            },
            new EducationLevel()
            {
               // Id = 3,
                name = "Klasy Siedem-Osiem",
                Units = new List<Unit>()
                {
                    new Unit()
                    {
                       // Id = 5,
                        name = "Potęgi i Pierwiastki",
                        educationLevelId = 3
                    },
                    new Unit()
                    {
                       // Id = 7,
                        name = "Wyrażenia algebraiczne",
                        educationLevelId = 3
                    },
                    new Unit()
                    {
                       // Id = 8,
                        name = "Działania na wyrażeniach algebraicznych",
                        educationLevelId = 3
                    },
                    new Unit()
                    {
                      //  Id = 9,
                        name = "Procenty",
                        educationLevelId = 3
                    },
                    new Unit()
                    {
                      //  Id = 10,
                        name = "Równania z niewiadomą",
                        educationLevelId = 3
                    },
                    new Unit()
                    {
                      //  Id = 11,
                        name = "Proporcjonalność prosta",
                        educationLevelId = 3
                    },
                }
            }
            };
            return levels;
        }
    }

    
}
