using CameraBase.DTO;
using CameraBase.Entity;
using CameraBase.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace CameraBase.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class SubAccountController : ControllerBase
    {
        private readonly ISubAccountRepository _subRepository;

        public SubAccountController(ISubAccountRepository subAccountRepository)
        {
            _subRepository = subAccountRepository;
        }

        [HttpGet("Get")]
        public async Task<ActionResult<List<SubAccount>>> GetAll()
        {
            try
            {
                var list = await _subRepository.GetAll();
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<List<SubAccount>>> GetName(string name)
        {
            try
            {
                var list = await _subRepository.GetByName(name);
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<SubAccount>> GetId(int id)
        {
            try
            {
                var account = await _subRepository.FindByID(id);
                return Ok(account);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<SubAccount>> CreateAccount(SubAccountDTO sub)
        {
            try
            {
                await _subRepository.CreateAccount(sub);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult> EditAccount(int id, SubAccountDTO account)
        {
            try
            {
                await _subRepository.EditAccount(account, id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteAccount(int id)
        {
            try
            {
                await _subRepository.DeleteAccount(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}

