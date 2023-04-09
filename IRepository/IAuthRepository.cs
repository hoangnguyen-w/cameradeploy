using CameraBase.DTO;
using CameraBase.Entity;

namespace CameraBase.IRepository
{
    public interface IAuthRepository
    {
        Task<Account> CheckLogin(UserDTO account);

        string CreateToken(Account account);

        RefreshToken GenerateRefreshToken();
        Account SetRefreshToken(RefreshToken newRefreshToken, HttpResponse response);
    }
}
