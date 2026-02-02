using DTO.DTOs;
using FrontEnd.Components.Services;
using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace FrontEnd.Components.Pages.ChooseGame
{
    public class PickGameBase :ComponentBase
    {
        [Inject]
        IPagesService pagesService { get; set; }
        [Inject]
        IUnitService unitService { get; set; }


        [Parameter]
        public string unitName { get; set; }

        protected IEnumerable<PagesDTO> pages { get; set; } = Enumerable.Empty<PagesDTO>();

        protected override async Task OnInitializedAsync()
        {
            var unitID = await unitService.GetUnitByName(unitName);
            var id = unitID.ID;

            pages = await pagesService.GetPagesByUnitID(id);
        }
    }
}
