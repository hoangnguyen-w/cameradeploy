using CameraBase.DTO;
using CameraBase.Entity;

namespace CameraBase.IRepository
{
    public interface ISubAccountRepository
    {
        Task<List<SubAccount>> GetAll();
        Task<List<SubAccount>> GetByName(string name);
        Task<List<SubAccount>> FindByID(int id);
        Task<List<string>> FindbyIDReturnPhone(int id);

        Task<List<SortDTO>> SortSubAccount(int idAccount, List<SortDTO> subAccounts);

        Task EditAccount(SubAccountDTO _account, int id);
        Task DeleteAccount(int id);
        Task CreateAccount(SubAccountDTO accountDTO);
    }
}
