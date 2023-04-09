using CameraBase.DTO;
using CameraBase.Entity;
using CameraBase.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CameraBase.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private static Account user = new Account();
        private readonly IAuthRepository _authRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly CameraBasedContext _context;

        public AuthController(IAuthRepository authRepository, IAccountRepository accountRepository, CameraBasedContext context)
        {
            _accountRepository = accountRepository;
            _authRepository = authRepository;
            _context = context;
        }


        [HttpPost("login")]
        public async Task<ActionResult<Account>> Login(UserDTO req)
        {
            try
            {
                var acc = await _authRepository.CheckLogin(req);
                string token = _authRepository.CreateToken(acc);

                var refreshToken = _authRepository.GenerateRefreshToken();
                var setToken = _authRepository.SetRefreshToken(refreshToken, Response);

                user = acc;
                user.RefreshToken = setToken.RefreshToken;
                user.TokenExpires = setToken.TokenExpires;

                TokenDTO dto = new TokenDTO();

                dto.Token = token;
                dto.AccounName = acc.AccounName;
                dto.Image = acc.Image;
                dto.Role = acc.Role.RoleName;

                return Ok(dto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("logout"), Authorize(Roles = "Admin, Owner, Customer")]
        public ActionResult Logout(string token)
        {
            try
            {
                token = "";
                return Ok(token);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
