using Microsoft.AspNetCore.Components;
using MudBlazor;
using PI6.Shared.Data.Dtos;
using PI6.WebApi.Services;

namespace PI6.Components.Shared.Account;

public partial class Results
{
    [Parameter] public List<FormResultDto> FormResultsDto { get; set; }

    [Inject] public IApplicationService ApplicationService { get; set; }

    private TableGroupDefinition<FormResultDto> _formResultDefinition;

    protected override void OnInitialized()
    {
        _formResultDefinition = new()
        {
            GroupName = "Testy",
            Indentation = true,
            Expandable = true,
            Selector = (e) => e.FormName,
            IsInitiallyExpanded = false
        };
    }

    private static int Result(FormResultDto data)
    {
        return (int)Math.Round(data.NumberOfCorrectAnswers / (float)data.NumberOfAllCorrectAnswers * 100);
    }

    private static string AverageTime(TableGroupData<object, FormResultDto> data)
    {
        return $"{Math.Round(data.Items.Average(x => x.UsedTime)) / 60} min {Math.Round(data.Items.Average(x => x.UsedTime)) % 60} sek";
    }

    private static string AverageResult(TableGroupData<object, FormResultDto> data)
    {
        return $"{Math.Round(data.Items.Average(x => x.NumberOfCorrectAnswers / (float)x.NumberOfAllCorrectAnswers * 100))} %";
    }
}