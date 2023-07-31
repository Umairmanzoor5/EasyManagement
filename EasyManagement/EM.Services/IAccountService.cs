using EM.DataRepository.Accounts;

namespace EM.Services;

public interface IAccountService
{
    Task<List<ListAccounts>> ListAccountTask();
    Task<InfoAccount> InfoAccountTask(string email);
    Task<string> LoginAccountTask(LoginAccount account);
    Task RegisterAccountTask(RegisterAccount account);
    Task EditAccountTask(EditAccount account);
    //Task DisableAccountTask(string email);
}