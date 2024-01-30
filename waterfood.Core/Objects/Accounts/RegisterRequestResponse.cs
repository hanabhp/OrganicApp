

using waterfood.Core.Objects.Generals;

namespace waterfood.Core.Objects.Accounts
{
    public class RegisterRequestResponse : Response
    {
        public string Email { get; set; } = null!;
        public string Code { get; set; } = null!;
    }

    public class Register
    {
        public string Email { get; set; } = null!;
        public string Code { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }

    public class RegisterResponse : Response
    {
        public string Token { get; set; } = null!;
        public string ExpireDate { get; set; } = null!;
        public string ExpireTime { get; set; } = null!;
        public string ThemeMode { get; set; } = null!;
    }

    public class Login
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool RememberMe { get; set; }
    }

    public class LoginResponse : Response
    {
        public string Token { get; set; } = null!;
        public string ExpireDate { get; set; } = null!;
        public string ExpireTime { get; set; } = null!;
        public string ThemeMode { get; set; } = null!;
        public AuthenticateUser User { get; set; } = null!;
    }

    public class AuthenticateUser
    {
        public long UserId { get; set; }
        public string UserName { get; set; } = null!;
        public bool State { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

    public class ForgotPasswordResponse : Response
    {
        public string Phone { get; set; } = null!;
        public string Code { get; set; } = null!;
    }

    public class ResetPassword
    {
        public string Email { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string RepeatPassword { get; set; } = null!;
    }
}
