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
    public class SubAccountController : ControllerBase
    {
        private readonly ISubAccountRepository _subRepository;
        private readonly IAccountRepository _accountRepository;
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
        [HttpGet("GetByIdAcc/{id}")]
        public async Task<ActionResult<Account>> GetId(int id)
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
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<SubAccount>> GetIdAcc(int id)
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

        [HttpGet("GetByPhone/{id}")]
        public async Task<ActionResult<string>> GetPhone(int id)
        {
            try
            {
                var account = await _subRepository.FindbyIDReturnPhone(id);
                return Ok(account);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("SortByPhone/{id}")]
        public async Task<ActionResult<List<SortDTO>>> SortPhone(int id, List<SortDTO> subAccounts)
        {
            try
            {
                var account = await _subRepository.SortSubAccount(id, subAccounts);
                return Ok(account);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpPost("Create")]
        public async Task<ActionResult<SubAccount>> CreateSubAccount(SubAccountDTO sub)
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
        [Authorize(Roles = "Admin, Customer")]
        [HttpPut("Update/{id}")]
        public async Task<ActionResult> EditSubAccount(int id, SubAccountDTO account)
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
        //[Authorize(Roles = "Admin, Customer")]
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteSubAccount(int id)
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

