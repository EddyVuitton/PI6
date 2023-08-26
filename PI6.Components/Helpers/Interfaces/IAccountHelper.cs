using PI6.Shared.Data.Dtos;

namespace PI6.Components.Helpers.Interfaces;

public interface IAccountHelper
{
    Task<AccountDto> LoadAccount();
}