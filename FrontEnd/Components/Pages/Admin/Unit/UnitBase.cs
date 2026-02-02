using DTO.DTOs;
using FrontEnd.Components.Services;
using FrontEnd.Components.Services.Contracts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace FrontEnd.Components.Pages.Admin.Unit
{
    public class UnitBase: ComponentBase
    {
        [Inject]
        IEdLevelService _edLevelService { get; set; }
   
        [Inject]
        IUnitService _unitService { get; set; }

        [Inject]
        IPagesService _pagesService { get; set; }

        public IEnumerable<EducationLevelDTO> edLevels { get; set; } = Enumerable.Empty<EducationLevelDTO>();
        public IEnumerable<UnitDTO> units { get;set; } = Enumerable.Empty<UnitDTO>();


        //  protected override async Task OnInitializedAsync()
        protected override async Task OnInitializedAsync()
        {
            edLevels = await _edLevelService.GetEducationLevels();
            units = await _unitService.GetUnits();
        }

        protected async Task AddUnit(string name, string desc, string edLvlName)
        {
            var unit = new UnitDTO() {name = name, description= desc, educationLevel= edLvlName, ID=0 };
            var response = await _unitService.AddUnit(unit);
            await RefreshDatabase();
        }
        protected async Task EditUnit(int Id, string name, string desc, string edLvlName)
        {
            var unit = new UnitDTO() { name = name, description = desc, educationLevel = edLvlName, ID = Id };
            var response = await _unitService.UpdateUnit(unit);
            await RefreshDatabase();
        }

        protected async Task DeleteUnit(UnitDTO unit)
        {
            await _unitService.DeleteUnit(unit);
            await RefreshDatabase();
        }
        protected async Task RefreshDatabase()
        {
            edLevels = await _edLevelService.GetEducationLevels();
            units = await _unitService.GetUnits();
        }

        protected async Task<IEnumerable<PagesDTO>> GetPages(int unitId)
        {
            var pg = await _pagesService.GetPagesByUnitID(unitId);
            return pg;
        }

        protected async Task DeletePage( int pageId)
        {
            await _pagesService.DeletePage(pageId);
        }

        protected async Task UpdatePage(PagesDTO page)
        {
            await _pagesService.UpdatePage(page);
        }

        protected async Task CreatePage(PagesDTO page)
        {
            await _pagesService.AddPage(page);
        }
    }
}
