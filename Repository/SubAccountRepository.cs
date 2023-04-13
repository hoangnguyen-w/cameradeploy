using CameraBase.DTO;
using CameraBase.Entity;
using CameraBase.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CameraBase.Repository
{
    public class SubAccountRepository : ISubAccountRepository
    {
        private readonly CameraBasedContext _context;
        public static SubAccount sub = new SubAccount();

        public SubAccountRepository(CameraBasedContext context)
        {
            _context = context;
        }

        public async Task<List<SubAccount>> GetAll()
        {
            var subList = await _context.SubAccounts.Include(a => a.Account).ToListAsync();
            return subList;
        }

        public async Task<List<SubAccount>> GetByName(string name)
        {
            var nameList = await _context.SubAccounts.Where(a => a.SubAccountName.Trim().ToLower().Contains(name.Trim().ToLower())).Include(a => a.Account).ToListAsync();
            return nameList;
        }

        public async Task<SubAccount> FindByID(int id)
        {
            var account = await _context.SubAccounts.FirstOrDefaultAsync(a => a.AccountID == id);
            return account;
        }

        public async Task EditAccount(SubAccountDTO _account, int id)
        {
            var acc = await _context.SubAccounts.FirstOrDefaultAsync(a => a.SubAccountID == id);

            acc.SubAccountName = _account.SubAccountName;
            acc.SubAccountPhone = _account.SubAccountPhone;
            acc.AccountID = _account.AccountID;

            _context.SubAccounts.Update(acc);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccount(int id)
        {
            var acc = await _context.SubAccounts.FirstOrDefaultAsync(a => a.SubAccountID == id);
            _context.SubAccounts.Remove(acc);
            await _context.SaveChangesAsync();
        }


        public async Task CreateAccount(SubAccountDTO res)
        {
            sub = new SubAccount();
            sub.SubAccountName = res.SubAccountName;
            sub.SubAccountPhone= res.SubAccountPhone;   
            sub.AccountID = res.AccountID;


            _context.SubAccounts.Add(sub);
            await _context.SaveChangesAsync();
        }

    }
}
