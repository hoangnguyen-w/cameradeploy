#nullable disable
using CameraBase.DTO;
using CameraBase.Entity;
using CameraBase.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CameraBase.Repository
{
    public class NotifiHistoryRepository : INotifiHistoryRepository
    {
        private readonly CameraBasedContext _context;
        public static NotifiHistory notify = new NotifiHistory();

        public NotifiHistoryRepository(CameraBasedContext context)
        {
            _context = context;
        }

        public async Task<List<NotifiHistory>> GetAll()
        {
            var notiList = await _context.NotifiHistories.Include(a => a.Account).Include(car => car.CarManagement).ToListAsync();
            //.Include(a => a.Account).Include(car => car.CarManagement)
            return notiList;
        }

        public async Task<List<NotifiHistory>> FindByID(int id)
        {
            var notifyID = await _context.NotifiHistories.Where(noti => noti.AccountID == id).ToListAsync();
            return notifyID;
        }

        public async Task<List<NotifiHistory>> FindDay(DateTime start, DateTime end)
        {
            var notiday = await _context.NotifiHistories.Where(a => a.HistoryDate >= start && a.HistoryDate <= end).ToListAsync();
                //Where(s => s.HistoryDate >= notifiHistoryDayDTO.dateStart && s.HistoryDate <= notifiHistoryDayDTO.dateEnd).ToListAsync();
            return notiday;
        }

        public async Task EditAccount(NotifiHistoriesDTO notifiHistoriesDTO, int id)
        {
            var noti = await _context.NotifiHistories.FirstOrDefaultAsync(noti => noti.HistoryID == id);

            noti.HistoryName = notifiHistoriesDTO.HistoryName;
            noti.HistoryDate = notifiHistoriesDTO.HistoryDate;
            noti.HistoryStatus = notifiHistoriesDTO.HistoryStatus   ;
            noti.AccountID = notifiHistoriesDTO.AccountID;
            noti.CarManagementID = notifiHistoriesDTO.CarManagementID;


            _context.NotifiHistories.Update(noti);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccount(int id)
        {
            var noti = await _context.NotifiHistories.FirstOrDefaultAsync(noti => noti.HistoryID == id);
            _context.NotifiHistories.Remove(noti);
            await _context.SaveChangesAsync();
        }


        public async Task CreateAccount(NotifiHistoriesDTO notifiHistoriesDTO)
        {
            notify = new NotifiHistory();
            notify.HistoryName = notifiHistoriesDTO.HistoryName;
            notify.HistoryDate = notifiHistoriesDTO.HistoryDate;
            notify.HistoryStatus = notifiHistoriesDTO.HistoryStatus;
            notify.AccountID = notifiHistoriesDTO.AccountID;
            notify.CarManagementID = notifiHistoriesDTO.CarManagementID;

            _context.NotifiHistories.Add(notify);
            await _context.SaveChangesAsync();
        }

    }
}
