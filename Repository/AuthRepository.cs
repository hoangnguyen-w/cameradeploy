#nullable disable
using CameraBase.DTO;
using CameraBase.Entity;
using CameraBase.IRepository;
using Firebase.Auth;
using FirebaseAdmin.Auth;
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
        private readonly IAccountRepository _accountRepository;
        public static Account acc = new Account();

        public AuthRepository(IConfiguration configuration, CameraBasedContext context, IAccountRepository accountRepository)
        {
            _configuration = configuration;
            _context = context;
            _accountRepository = accountRepository;
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

        public async Task<jwtDTO> AuthenFirebase(string idToken)
        {
            string key = "AIzaSyAwB1GD5SLBrIMuOjp6DrOUhNGjkUKPUz0";
            string jwt = "";
            jwtDTO jwtDto = null;
            FirebaseToken decodedToken = await FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance.VerifyIdTokenAsync(idToken);
            string uid = decodedToken.Uid;
            var authenUser = new FirebaseAuthProvider(new FirebaseConfig(key));
            var authen = authenUser.GetUserAsync(idToken);
            User user = authen.Result;
            var tagetAccount = await _context.Accounts.Include(a => a.Role).Where(a => a.AccounName == user.Email).FirstOrDefaultAsync();
            if (tagetAccount == null)
            {
                CreateAccountDTO createAccountDTO = new CreateAccountDTO()
                {
                    AccountName = user.Email,
                    Password = "",
                    RoleID = 2
                };
                await _accountRepository.CreateAccount(createAccountDTO);
                var _tagetAccount = await _context.Accounts.Include(a => a.Role).Where(a => a.AccounName == user.Email).FirstOrDefaultAsync();
                jwt = ReCreateFirebaseToken(_tagetAccount, uid);
                jwtDto = new jwtDTO(_tagetAccount.AccountID, _tagetAccount.AccountEmail, _tagetAccount.Image, jwt, _tagetAccount.Role.RoleName);
                    return (jwtDto);
                /*if (_tagetAccount != null)
                {
                    string url = "http://localhost:3000/";
                    jwt = ReCreateFirebaseToken(_tagetAccount, uid);
                    jwtDto = new JWTDto(_tagetAccount.AccountID, _tagetAccount.AccountEmail, true, _tagetAccount.Owner, user.PhotoUrl, jwt, _tagetAccount.Role.RoleName);
                    EmailDto emailDto = new EmailDto()
                    {
                        To = user.Email,
                        Subject = "Chào mừng đến với Bookly!",
                        Body = "<i>Nhấn vào đường link này để trở về trang chủ!!!</i> " + " <a href=" + url + ">link</a>",
                    };
                    SendConfirmGoogleSignInEmail(emailDto);
                    return (jwtDto);
                }*/
            }
            jwt = ReCreateFirebaseToken(tagetAccount, uid);
            jwtDto = new jwtDTO(tagetAccount.AccountID, tagetAccount.AccountEmail, tagetAccount.Image, jwt, tagetAccount.Role.RoleName);
            return (jwtDto);
        }

        public string ReCreateFirebaseToken(Account account, string uid)
        {
            if (account.AccounName != null)
            {
                List<Claim> claims = new List<Claim>{
            //new Claim(ClaimTypes.Name, account.Owner),
            new Claim(ClaimTypes.Email, account.AccounName),
            //new Claim(ClaimTypes.Uri, account.Image),
            new Claim(ClaimTypes.PostalCode, account.AccountID + ""),
            new Claim(ClaimTypes.Role, account.Role.RoleName),
            new Claim(ClaimTypes.GivenName, uid)
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
            else
            {
                throw new BadHttpRequestException("Fill all personal information");
            }
        }
    }
}
