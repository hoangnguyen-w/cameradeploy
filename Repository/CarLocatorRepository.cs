#nullable disable
using CameraBase.DTO;
using CameraBase.Entity;
using CameraBase.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CameraBase.Repository
{
    public class CarLocatorRepository : ICarLocatorRepository
    {
        private readonly CameraBasedContext _context;
        public static Carlocator location = new Carlocator();

        public CarLocatorRepository(CameraBasedContext context)
        {
            _context = context;
        }

        public async Task CreateAccount(CarLocatorDTO carLocatorDTO)
        {
            location = new Carlocator();
            location.location = carLocatorDTO.location;
            location.CarManagementID = carLocatorDTO.CarManagementID;

            _context.Carlocators.Add(location);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccount(int id)
        {
            var location = await _context.Carlocators.FirstOrDefaultAsync(locator => locator.CarLocatorID == id);
            _context.Carlocators.Remove(location);
            await _context.SaveChangesAsync();
        }

        public async Task EditAccount(CarLocatorDTO carLocatorDTO, int id)
        {
            var location = await _context.Carlocators.FirstOrDefaultAsync(locator => locator.CarLocatorID == id);
            location.location = carLocatorDTO.location;

            _context.Carlocators.Update(location);
            await _context.SaveChangesAsync();
        }

        public async Task<Carlocator> FindByID(int id)
        {
            var location = await _context.Carlocators.FirstOrDefaultAsync(locator => locator.CarLocatorID == id);
            return location;
        }

        public async Task<List<Carlocator>> GetAll()
        {
            var location = await _context.Carlocators.Include(car => car.CarManagement).ToListAsync();
            return location;
        }
    }
}
