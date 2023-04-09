using CameraBase.DTO;
using CameraBase.Entity;
using CameraBase.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CameraBase.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class NotifiHistoryController : ControllerBase
    {
        private readonly INotifiHistoryRepository _notifiHistoryRepository;
        public NotifiHistoryController(INotifiHistoryRepository notiRepository)
        {
            _notifiHistoryRepository = notiRepository;
        }
        [HttpGet("Get")]
        public async Task<ActionResult<List<NotifiHistory>>> GetAll()
        {
            try
            {
                var list = await _notifiHistoryRepository.GetAll();
                return Ok(list);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<NotifiHistory>> GetId(int id)
        {
            try
            {
                var noti = await _notifiHistoryRepository.FindByID(id);
                return Ok(noti);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetByDay")]
        public async Task<ActionResult<List<NotifiHistory>>> GetDAY(DateTime start, DateTime end)//NotifiHistoryDayDTO notifiHistoryDayDTO
        {
            try
            {
                var noti = await _notifiHistoryRepository.FindDay(start, end);   
                return Ok(noti);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult<NotifiHistory>> CreateNoti(NotifiHistoriesDTO noti)
        {
            try
            {
                await _notifiHistoryRepository.CreateAccount(noti);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult> EditNoti(int id, NotifiHistoriesDTO noti)
        {
            try
            {
                await _notifiHistoryRepository.EditAccount(noti, id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult> DeleteNoti(int id)
        {
            try
            {
                await _notifiHistoryRepository.DeleteAccount(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



    }
}
