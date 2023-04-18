using CameraBase.DTO;
using CameraBase.Entity;

namespace CameraBase.IRepository
{
    public interface ICarManarmentRepository
    {
        Task<List<CarManagement>> GetAll();
        Task<List<CarManagement>> GetByName(string name);

        Task<List<CarManagement>> GetByColor(string color);

        Task<List<CarManagement>> GetByBrand(string brand);

        Task<List<CarManagement>> GetByLicensePlates(string licensePlates);
        Task<CarManagement> FindByID(int id);
        Task EditAccount(CarManagementDTO carManagementDTO, int id);
        Task DeleteAccount(int id);
        Task ChangeStatus(int id);
        Task CreateAccount(CarManagementDTO carManagementDTO);
    }
}
