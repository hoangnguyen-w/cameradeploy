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

        public async Task<List<SubAccount>> FindByID(int id)
        {
            //var account = await _context.SubAccounts.FirstOrDefaultAsync(a => a.AccountID == id);
            var account = await _context.SubAccounts.Where(a => a.AccountID == id).ToListAsync();
            if (account == null)
            {
                throw new BadHttpRequestException("No affiliate account yet");
            }
            return account;
        }

        public async Task<List<string>> FindbyIDReturnPhone(int id)
        {
            var phone = await _context.SubAccounts.Where(a => a.AccountID == id)
                                                  .Select(a => a.SubAccountPhone)
                                                  .ToListAsync();
            if (phone == null)
            {
                throw new BadHttpRequestException("No phone here!!!");
            }
            return phone;
        }

        public async Task EditAccount(SubAccountDTO _account, int id)
        {
            var acc = await _context.SubAccounts.FirstOrDefaultAsync(a => a.SubAccountID == id);

            var check = await _context.SubAccounts.FirstOrDefaultAsync(a => a.SubAccountPhone == _account.SubAccountPhone);

            if (check == null)
            {
                var check1 = await _context.Accounts.FirstOrDefaultAsync(a => a.Phone == _account.SubAccountPhone);

                if (check1 == null)
                {
                    acc.SubAccountName = _account.SubAccountName;
                    acc.SubAccountPhone = _account.SubAccountPhone;
                    acc.AccountID = _account.AccountID;

                    _context.SubAccounts.Update(acc);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new BadHttpRequestException("Sub Phone is already it exist");
                }

            }
            else
            {
                throw new BadHttpRequestException("Sub Phone is already it exist");
            }



        }

        public async Task<List<SortDTO>> SortSubAccount(int idAccount, List<SortDTO> listSub)
        {
            
            var getAll = await _context.SubAccounts.ToListAsync();
            int list = getAll.RemoveAll(a => a.AccountID != idAccount);

            for (int i = 0; i < getAll.Count(); i++)
            {
                _context.SubAccounts.Remove(getAll[i]);

            }

            for(int j = 0; j < listSub.Count(); j++)
            {
                var sub = new SubAccount()
                {
                    AccountID = idAccount,
                    SubAccountName = listSub[j].SubAccountName, 
                    SubAccountPhone = listSub[j].SubAccountPhone,
                };
                _context.SubAccounts.Add(sub);
            }

            await _context.SaveChangesAsync();

            return listSub;
        }




        public async Task DeleteAccount(int id)
        {
            var acc = await _context.SubAccounts.FirstOrDefaultAsync(a => a.SubAccountID == id);
            _context.SubAccounts.Remove(acc);
            await _context.SaveChangesAsync();
        }


        public async Task CreateAccount(SubAccountDTO res)
        {
            var check = await _context.SubAccounts.FirstOrDefaultAsync(a => a.SubAccountPhone == res.SubAccountPhone);

            if (check == null)
            {
                var check1 = await _context.Accounts.FirstOrDefaultAsync(a => a.Phone == res.SubAccountPhone);

                if (check1 == null)
                {
                    sub = new SubAccount();
                    sub.SubAccountName = res.SubAccountName;
                    sub.SubAccountPhone = res.SubAccountPhone;
                    sub.AccountID = res.AccountID;

                    _context.SubAccounts.Add(sub);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new BadHttpRequestException("Sub Phone is already it exist");
                }

            }
            else
            {
                throw new BadHttpRequestException("Sub Phone is already it exist");
            }

        }

    }
}
