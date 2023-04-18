using CameraBase.DTO;
using CameraBase.Entity;
using CameraBase.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CameraBase.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class CarManagementController : ControllerBase
    {
        private readonly ICarManarmentRepository _carManagementRepository;
        public CarManagementController(ICarManarmentRepository carRepository)
        {
            _carManagementRepository = carRepository;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Get")]
        public async Task<ActionResult<List<CarManagement>>> GetAll()
        {
            try
            {
                var list = await _carManagementRepository.GetAll();
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<List<CarManagement>>> GetName(string name)
        {
            try
            {
                var list = await _carManagementRepository.GetByName(name);
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //[Authorize(Roles = "Admin, Customer, Owner")]
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<CarManagement>> GetId(int id)
        {
            try
            {
                var account = await _carManagementRepository.FindByID(id);
                return Ok(account);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("ChangeStatus/{id}")]
        public async Task<ActionResult> ChangeStatusCar(int id)
        {
            try
            {
                await _carManagementRepository.ChangeStatus(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<CarManagement>> CreateCar(CarManagementDTO car)
        {
            try
            {
                await _carManagementRepository.CreateAccount(car);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("Update/{id}")]
        public async Task<ActionResult> EditCar(int id, CarManagementDTO car)
        {
            try
            {
                await _carManagementRepository.EditAccount(car, id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteCar(int id)
        {
            try
            {
                await _carManagementRepository.DeleteAccount(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
