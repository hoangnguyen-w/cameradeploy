using CameraBase.DTO;
using CameraBase.Entity;

namespace CameraBase.IRepository
{
    public interface INotifiHistoryRepository
    {
        Task<List<NotifiHistory>> GetAll();
        Task<NotifiHistory> FindByID(int id);
        //Task<List<NotifiHistory>> FindDay(NotifiHistoryDayDTO notifiHistoryDayDTO);
        Task<List<NotifiHistory>> FindDay(DateTime start, DateTime end);
        
        Task EditAccount(NotifiHistoriesDTO carManagnotifiHistoriesDTOementDTO, int id);
        Task DeleteAccount(int id);
        Task CreateAccount(NotifiHistoriesDTO notifiHistoriesDTO);
    }
}
