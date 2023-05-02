using CameraBase.Entity;
using CameraBase.IRepository;

namespace CameraBase.Repository
{
    public class StatisticalRepository : IStatisticalRepository
    {
        private readonly CameraBasedContext _context;
        public StatisticalRepository(CameraBasedContext context)
        {
            _context = context;
        }

        public int totalNumberOfAccount()
        {
            int count = _context.Accounts.Count(a => a.RoleID == 2);
            return count;
        }

        public int totalNumberOfSubAccount()
        {
            int count = _context.SubAccounts.Count();
            return count;

        }

        public int totalNumberOfHistory()
        {
            int count = _context.NotifiHistories.Count();
            return count;
        }
        public int totalNumberOfCar()
        {
            int count = _context.carManagements.Count();
            return count;
        }

    }
}
