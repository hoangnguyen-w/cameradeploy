using CameraBase.DTO;
using CameraBase.Entity;

namespace CameraBase.IRepository
{
    public interface ICarLocatorRepository
    {
        Task<List<Carlocator>> GetAll();
        Task<Carlocator> FindByID(int id);
        Task EditAccount(CarLocatorDTO carLocatorDTO, int id);
        Task DeleteAccount(int id);
        Task CreateAccount(CarLocatorDTO carLocatorDTO);
    }
}
