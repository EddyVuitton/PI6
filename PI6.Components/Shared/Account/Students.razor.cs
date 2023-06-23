using Microsoft.AspNetCore.Components;
using MudBlazor;
using PI6.Shared.Data.Dtos;
using PI6.WebApi.Services;

namespace PI6.Components.Shared.Account;

public partial class Students
{
    [Parameter] public List<StudentGroupMapDto> StudentGroupMap { get; set; }

    [Inject] public IApplicationService ApplicationService { get; set; }

    private TableGroupDefinition<StudentGroupMapDto> _groupDefinition;

    protected override void OnInitialized()
    {
        _groupDefinition = new()
        {
            GroupName = "Grupa",
            Indentation = false,
            Expandable = true,
            Selector = (e) => e.SgrName,
            IsInitiallyExpanded = false
        };
    }
}