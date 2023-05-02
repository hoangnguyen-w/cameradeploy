using CameraBase.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CameraBase.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class StatisticalController : ControllerBase
    {
        private readonly IStatisticalRepository _statisticalRepository;
        public StatisticalController(IStatisticalRepository statisticalRepository)
        {
            _statisticalRepository = statisticalRepository;
        }

        [HttpGet("TotalOfAccount")]
        public ActionResult NumberOfAccount()
        {
            try
            {
                int count = _statisticalRepository.totalNumberOfAccount();
                return Ok(count);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("TotalOfSubAccount")]
        public ActionResult NumberOfSubAccount()
        {
            try
            {
                int count = _statisticalRepository.totalNumberOfSubAccount();
                return Ok(count);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("TotalOfHistory")]
        public ActionResult NumberOfHistory()
        {
            try
            {
                int count = _statisticalRepository.totalNumberOfHistory();
                return Ok(count);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("TotalOfCar")]
        public ActionResult NumberOfCar()
        {
            try
            {
                int count = _statisticalRepository.totalNumberOfCar();
                return Ok(count);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
