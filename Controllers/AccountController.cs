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
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("Get")]
        public async Task<ActionResult<List<Account>>> GetAll()
        {
            try
            {
                var list = await _accountRepository.GetAll();
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetByName/{name}")]
        public async Task<ActionResult<List<Account>>> GetName(string name)
        {
            try
            {
                var list = await _accountRepository.GetByName(name);
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("GetByRole/{roleId}")]
        public async Task<ActionResult<List<Account>>> GetByRole(int roleId)
        {
            try
            {
                var list = await _accountRepository.GetByRole(roleId);
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<List<Account>>> GetId(int id)
        {
            try
            {
                var account = await _accountRepository.FindByID(id);  
                return Ok(account);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        public async Task<ActionResult<Account>> CreateAccount(CreateAccountDTO res)
        {
            try
            {
                await _accountRepository.CreateAccount(res);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        //[Authorize(Roles = "Admin")]
        [HttpPut("Update/{id}")]
        public async Task<ActionResult> EditAccount(int id, AccountDTO account)
        {
            try
            {
                await _accountRepository.EditAccount(account, id);
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
                await _accountRepository.DeleteAccount(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
