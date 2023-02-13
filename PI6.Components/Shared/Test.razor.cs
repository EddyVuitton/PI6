using Microsoft.AspNetCore.Components;
using MudBlazor;
using PI6.Shared.Entities;
using PI6.WebApi.Services;

namespace PI6.Components.Shared;

public partial class Test
{
    [Inject] public IApplicationService ApplicationService { get; set; }
    public IEnumerable<formularz_typ> FormularzeTyp = new List<formularz_typ>();

    private MudTable<formularz_typ> _table;
    private readonly bool _loading;
    private string searchString = "";

    protected override async Task OnInitializedAsync()
    {
        FormularzeTyp = await ApplicationService.GetAllFormularzTyp();
    }

    //private bool FilterFuncGeneral(CustomerDto customer) => FilterFuncSpecific(customer, searchString);

    //private static bool FilterFuncSpecific(CustomerDto customer, string searchString)
    //{
    //    if (string.IsNullOrWhiteSpace(searchString))
    //        return true;
    //    if (customer.City.Contains(searchString, StringComparison.OrdinalIgnoreCase))
    //        return true;
    //    if (customer.StateProvince.Contains(searchString, StringComparison.OrdinalIgnoreCase))
    //        return true;
    //    if (customer.CountryRegion.Contains(searchString, StringComparison.OrdinalIgnoreCase))
    //        return true;
    //    if (customer.PostalCode.Contains(searchString, StringComparison.OrdinalIgnoreCase))
    //        return true;
    //    if ($"{customer.City} {customer.StateProvince} {customer.CountryRegion} {customer.PostalCode}".Contains(searchString))
    //        return true;
    //    return false;
    //}
}