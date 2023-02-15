using MudBlazor;

namespace PI6.Components.Pages;

public partial class TworzenieQuizu
{
    public string TextValue1 { get; set; }
    public string TextValue2 { get; set; }

    private int _questionCount = 1;

    private void AddNew()
    {
        _questionCount++;
        StateHasChanged();
    }
}
