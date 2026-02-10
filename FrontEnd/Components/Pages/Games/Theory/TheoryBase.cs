using DTO.DTOs;
using FrontEnd.Components.Classes;
using FrontEnd.Components.Services;
using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace FrontEnd.Components.Pages.Games.Theory
{
    public class TheoryBase : ComponentBase
    {
        [Inject]
        IDefinitionService definitionService { get; set; }

        [Inject]
        IIncorrectService incorrectService { get; set; }

        [Inject]
        protected IUserProgressService userProgressService { get; set; }

         

        [Parameter]
        public string Unit { get; set; }

        protected GamesBase gamesBase;

        public List<DefinitionDTO> Definitions { get; set; } = new List<DefinitionDTO>();

        public List<IncorrectDTO> incorrects = new List<IncorrectDTO>();

        public List<int> usedDefinitions = new List<int>();

        public DefinitionDTO? currentDef = null;
        public bool ready;
        public bool error = false;

        public string imgName = "";
        protected override async Task OnInitializedAsync()
        {
            gamesBase = new GamesBase(Unit, "Teoria");

            var defs = await definitionService.GetDefinitionsByUnit(Unit);
            foreach (var def in defs)
            {
                var incs = await incorrectService.GetPairsByDef(def.ID);
                if (incs.ToArray().Length != 0)
                {
                    Definitions.Add(def);
                }
            }

            if (Definitions.Count > 0)
            {
                await PrepareNew();
            }
            else
            {
                ready = false;
                error = true;
            }
        }
        protected async Task PrepareNew()
        {
            imgName = "";
            Random rnd = new Random();
            int a;
            do
            {
                a = rnd.Next(0, Definitions.Count());
            } while (usedDefinitions.Contains(Definitions[a].ID) == true);

            currentDef = Definitions[a];

            if (currentDef.type == "Img" && currentDef.part1.Contains(";"))
            {
                var prts = currentDef.part1.Split(";");
                imgName = prts[1];
                currentDef.part1 = prts[0];
            }
         
            await FillIncorrects(currentDef.ID);
            ready = true;
        }
        protected async Task FillIncorrects(int defID)
        {
            var inc = await incorrectService.GetIncorrectByDefs(defID);
            incorrects = inc.ToList();
        }
    }
}
