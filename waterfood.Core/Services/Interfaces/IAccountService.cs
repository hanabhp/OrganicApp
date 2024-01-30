
using QRCoder;
using waterfood.Core.Objects.Accounts;
using waterfood.Core.Objects.Generals;
using waterfood.Data.Entities.Users;

namespace waterfood.Core.Services.Interfaces
{
    public interface IAccountService
    {
        RegisterRequestResponse RegisterRequest(string? phone);
        bool IsUserExists(string userName);
        RegisterResponse Register(Register register);
        User? ByUserName(string userName);
        T Create<T>(T entity);
        T Update<T>(T entity);
        LoginResponse Login(Login login);
        void Init();
        ForgotPasswordResponse ForgotPasswordRequest(string? phone);
        Response ResetPassword(ResetPassword reset);
        UserLocationDTO GetUserLocationById(int userId);
        byte[] GenerateQrCodeBitmap(string userName);
        UserInfo GetCurrentUser(string userName);
    }
}
