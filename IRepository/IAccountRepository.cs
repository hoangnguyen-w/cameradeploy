using CameraBase.DTO;
using CameraBase.Entity;

namespace CameraBase.IRepository
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAll();
        Task<List<Account>> GetByName(string name);
        Task<List<Account>> GetByRole(int roleId);
        Task<List<Account>> FindByID(int id);
        Task EditAccount(AccountDTO _account, int id);
        Task DeleteAccount(int id);
        Task CreateAccount(CreateAccountDTO accountDTO);
    }
}
