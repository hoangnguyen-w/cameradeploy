#nullable disable
using CameraBase.DTO;
using CameraBase.Entity;
using CameraBase.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CameraBase.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly CameraBasedContext _context;
        public static Account acc = new Account();

        public AccountRepository(CameraBasedContext context)
        {
            _context = context;
        }

        public async Task<List<Account>> GetAll()
        {
            var accountList = await _context.Accounts.Include(a => a.Role).Include(a => a.NotifiHistories).ToListAsync();
            //.Include(b => b.Role)
            return accountList;
        }

        public async Task<List<Account>> GetByName(string name)
        {
            var nameList = await _context.Accounts.Where(a => a.FullName.Trim().ToLower().Contains(name.Trim().ToLower())).Include(a => a.NotifiHistories).ToListAsync();
            return nameList;
        }

        public async Task<Account> FindByID(int id)
        {
            var account = await _context.Accounts.Include(a => a.NotifiHistories).FirstOrDefaultAsync(a => a.AccountID == id);
            return account;
        }

        public async Task<List<Account>> GetByRole(int roleId)
        {
            var account = await _context.Accounts.Where(a => a.RoleID == roleId).Include(a => a.NotifiHistories).ToListAsync();
            return account;
        }

        public async Task EditAccount(AccountDTO _account, int id)
        {
            var acc = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountID == id);

            var check = await _context.Accounts.FirstOrDefaultAsync(a => a.Phone == _account.Phone);

            if (check == null)
            {
                var checkEmail = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountEmail == _account.AccountEmail);
                if (checkEmail == null)
                {
                    acc.FullName = _account.fullName;
                    acc.Phone = _account.Phone;
                    acc.AccountEmail = _account.AccountEmail;
                    acc.Image = _account.Image;
                    acc.password = _account.Password;
                    acc.RoleID = _account.RoleID;
                }
                else
                {
                    throw new BadHttpRequestException("Email is already it exist");
                }
                _context.Accounts.Update(acc);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new BadHttpRequestException("Phone is already it exist");
            }

        }

        public async Task DeleteAccount(int id)
        {
            var acc = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountID == id);
            _context.Accounts.Remove(acc);
            await _context.SaveChangesAsync();
        }


        public async Task CreateAccount(CreateAccountDTO res)
        {
            var check = await _context.Accounts.FirstOrDefaultAsync(a => a.AccounName == res.AccountName);

            if (check == null)
            {
                acc = new Account();
                acc.AccounName = res.AccountName;
                acc.password = res.Password;
                acc.RoleID = res.RoleID;

                _context.Accounts.Add(acc);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new BadHttpRequestException("Account Name is already it exist");
            }


        }
    }
}

