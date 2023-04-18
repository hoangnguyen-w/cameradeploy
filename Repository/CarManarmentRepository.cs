#nullable disable
using CameraBase.DTO;
using CameraBase.Entity;
using CameraBase.IRepository;
using Microsoft.EntityFrameworkCore;

namespace CameraBase.Repository
{
    public class CarManarmentRepository : ICarManarmentRepository
    {
        private readonly CameraBasedContext _context;

        public CarManarmentRepository(CameraBasedContext context)
        {
            _context = context;
        }

        public static CarManagement car = new CarManagement();

        public async Task<List<CarManagement>> GetAll()
        {
            var carList = await _context.carManagements.ToListAsync();
            //
            return carList;
        }

        public async Task<List<CarManagement>> GetByName(string name)
        {
            var nameList = await _context.carManagements.Where(car => car.CarName.Trim().ToLower().Contains(name.Trim().ToLower())).ToListAsync();
            return nameList;
        }
        public async Task<List<CarManagement>> GetByColor(string color)
        {
            var nameList = await _context.carManagements.Where(car => car.CarColor.Trim().ToLower().Contains(color.Trim().ToLower())).ToListAsync();
            return nameList;
        }
        public async Task<List<CarManagement>> GetByBrand(string brand)
        {
            var nameList = await _context.carManagements.Where(car => car.CarBrand.Trim().ToLower().Contains(brand.Trim().ToLower())).ToListAsync();
            return nameList;
        }
        public async Task<List<CarManagement>> GetByLicensePlates(string licensePlates)
        {
            var nameList = await _context.carManagements.Where(car => car.LicensePlates.Trim().ToLower().Contains(licensePlates.Trim().ToLower())).ToListAsync();
            return nameList;
        }

        public async Task<CarManagement> FindByID(int id)
        {
            var car = await _context.carManagements.FirstOrDefaultAsync(carID => carID.CarManagementID == id);
            return car;
        }

        public async Task EditAccount(CarManagementDTO carManagementDTO, int id)
        {
            var car = await _context.carManagements.FirstOrDefaultAsync(carID => carID.CarManagementID == id);

            car.CarName = carManagementDTO.carName;
            car.CarColor = carManagementDTO.carColor;
            car.CarBrand = carManagementDTO.carBrand;
            car.LicensePlates = carManagementDTO.licensePlates;
            car.Status = true;
            

            _context.carManagements.Update(car);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAccount(int id)
        {
            var car = await _context.carManagements.FirstOrDefaultAsync(carID => carID.CarManagementID == id);
            _context.carManagements.Remove(car);
            await _context.SaveChangesAsync();
        }

        public async Task ChangeStatus(int id)
        {
            var car = await _context.carManagements.FirstOrDefaultAsync(carID => carID.CarManagementID == id);
            if (car.Status)
            {
                car.Status = false;
            }
            else
            {
                car.Status = true;
            }
            
            _context.carManagements.Update(car);
            await _context.SaveChangesAsync();
        }


        public async Task CreateAccount(CarManagementDTO carManagementDTO)
        {
            car = new CarManagement();
            car.CarName = carManagementDTO.carName;
            car.CarColor = carManagementDTO.carColor;
            car.LicensePlates = carManagementDTO.licensePlates;
            car.CarBrand = carManagementDTO.carBrand;
            car.Status = true;

            _context.carManagements.Add(car);
            await _context.SaveChangesAsync();
        }

    }
}
