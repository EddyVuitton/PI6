using Microsoft.AspNetCore.Components;
using PI6.Shared.Data.Entities;
using PI6.WebApi.Services;

namespace PI6.Components.Shared.Account;

public partial class AccountCreate
{
    [Inject] public IApplicationService ApplicationService { get; set; }

    private List<account_type> _accountTypes = new();
    private account _account = new();
    private string _choosedAccountType = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        _accountTypes = await ApplicationService.GetAccountTypes();
    }

    private async Task Save()
    {
        _account.us_activate = DateTime.Now;
        _account.us_ust_id = _accountTypes.FirstOrDefault(x => x.ust_name == _choosedAccountType).ust_id;

        try
        {
            var responseMessage = await ApplicationService.CreateAccount(_account);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}