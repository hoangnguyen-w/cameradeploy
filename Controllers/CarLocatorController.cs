using CameraBase.DTO;
using CameraBase.Entity;
using CameraBase.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CameraBase.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarLocatorController : ControllerBase
    {
        private readonly ICarLocatorRepository _carLocatorRepository;

        public CarLocatorController(ICarLocatorRepository locatorRepository)
        {
            _carLocatorRepository = locatorRepository;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<List<Carlocator>>> GetAll()
        {
            try
            {
                var list = await _carLocatorRepository.GetAll();
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<Carlocator>> GetId(int id)
        {
            try
            {
                var account = await _carLocatorRepository.FindByID(id);
                return Ok(account);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<ActionResult<Carlocator>> CreateAccount(CarLocatorDTO location)
        {
            try
            {
                await _carLocatorRepository.CreateAccount(location);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("Update/{id}")]
        public async Task<ActionResult> EditAccount(int id, CarLocatorDTO location)
        {
            try
            {
                await _carLocatorRepository.EditAccount(location, id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteAccount(int id)
        {
            try
            {
                await _carLocatorRepository.DeleteAccount(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
