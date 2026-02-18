using DTO.DTOs;
using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.ChooseGame
{
    public partial class edLevelBase: ComponentBase
    {
        [Inject]
        IEdLevelService edLevelService { get; set; }

        [Inject]
        IUnitService unitService { get; set; }

        public IEnumerable<EducationLevelDTO> edLevels { get; set; } = Enumerable.Empty<EducationLevelDTO>();
        public IEnumerable<UnitDTO> units { get; set; } = Enumerable.Empty<UnitDTO>();

        protected override async Task OnInitializedAsync()
        {
            edLevels = await edLevelService.GetEducationLevels();
            units = await unitService.GetUnits();
        }
    }
}
