#nullable disable
using CameraBase.DTO;
using CameraBase.Entity;
using CameraBase.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CameraBase.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly CameraBasedContext _context;
        private readonly IConfiguration _configuration;
        public static Account acc = new Account();

        public AuthRepository(IConfiguration configuration, CameraBasedContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<Account> CheckLogin(UserDTO account)
        {
            var check = await _context.Accounts.Include(a => a.Role).FirstOrDefaultAsync(a => a.AccounName == account.AccounName);
            if (check != null)
            {
                if (account.password != check.password)
                {
                    throw new BadHttpRequestException("Wrong password!");
                }
            }
            else
            {
                throw new BadHttpRequestException("No!");
            }
            return check;
        }

        public string CreateToken(Account account)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, account.AccounName),
                new Claim(ClaimTypes.PostalCode, account.AccountID + ""),
                new Claim(ClaimTypes.Role, account.Role.RoleName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:TokenSecret").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: cred
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddHours(7),
                Created = DateTime.Now
            };
            return refreshToken;
        }

        public Account SetRefreshToken(RefreshToken newRefreshToken, HttpResponse response)
        {
            acc = new Account();
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            acc.RefreshToken = newRefreshToken.Token;
            acc.TokenCreated = newRefreshToken.Created;
            acc.TokenExpires = newRefreshToken.Expires;

            return acc;
        }
    }
}
