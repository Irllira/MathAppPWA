using DTO.DTOs;
using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.ChooseGame
{
    public class ChooseUnitBase : ComponentBase
    {
        [Inject]
        IUnitService unitService { get; set; }

        [Parameter]
        public required string educationLevelName { get; set; }
        public IEnumerable<UnitDTO> units { get; set; } = Enumerable.Empty<UnitDTO>();

        protected override async Task OnInitializedAsync()
        {
            units = await unitService.GetUnitsByEdLvlName(educationLevelName);
            //string s = educationLevelName;
        }
    }
}
