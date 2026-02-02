using DTO.DTOs;
using FrontEnd.Components.Services;
using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace FrontEnd.Components.Pages.Admin.Definitions
{
    public class DefinitionBase: ComponentBase
    {

        [Inject]
        IUnitService unitService { get; set; }
        [Inject]
        IDefinitionService definitionService { get; set; }

        [Inject]
        protected IIncorrectService IncorrectService { get; set; }

        public IEnumerable<UnitDTO> Units { get; set; } = Enumerable.Empty<UnitDTO>();
        public IEnumerable<DefinitionDTO> Definitions { get; set; } = Enumerable.Empty<DefinitionDTO>();

       // protected bool tableReady = false;

        //  protected override async Task OnInitializedAsync()
        protected override async Task OnInitializedAsync()
        {
            Units = await unitService.GetUnits();
            Definitions = await definitionService.GetDefinitions();
        }
        protected async Task AddDefinition(string name, string type, string p1, string p2,string unName)
        {
            var def = new DefinitionDTO() { name = name, type = type, part1 = p1, part2 = p2, UnitName=unName, ID=0};
            var response = await definitionService.AddDefinition(def);
           await RefreshDatabase();
        }

        protected async Task EditDefinition(int Id, string name,  string type, string p1, string p2, string unName)
        {
            var def = new DefinitionDTO() { name = name, type=type, part1 = p1, part2 = p2, UnitName = unName, ID = Id };
            var response = await definitionService.UpdateDefinition(def);
            await RefreshDatabase();
        }
        

        protected async Task DeleteDefinition(DefinitionDTO definition)
        {
           await definitionService.DeleteDefinition(definition);
           await RefreshDatabase();
        }
       protected async Task RefreshDatabase()
        {
            Units = await unitService.GetUnits();
            Definitions = await definitionService.GetDefinitions();
            //tableReady = false;
        }

        protected async Task<IEnumerable<IncorrectDTO>> GetIncorrect(int definitionID)
        {
            var inc = await IncorrectService.GetIncorrectByDefs(definitionID);
            return inc;
        }

        protected enum Types
        { 
            Normal,
            Img
        }

    }
}
