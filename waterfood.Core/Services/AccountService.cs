using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QRCoder;
using waterfood.Core.Objects.Accounts;
using waterfood.Core.Objects.Enums;
using waterfood.Core.Objects.Generals;
using waterfood.Core.Services.Interfaces;
using waterfood.Core.Utilities.Generators;
using waterfood.Core.Utilities.Security;
using waterfood.Core.Utilities.Validators;
using waterfood.Data.Context;
using waterfood.Data.Entities.Users;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace waterfood.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly IEmailService _email;
        private readonly WaterFoodContext _context;
        private readonly IConfiguration _configuration;

        public AccountService(IConfiguration configuration, WaterFoodContext context, IEmailService email)
        {
            _configuration = configuration;
            _context = context;
            _email = email;
        }

        public void Init()
        {

        }

        public Response ResetPassword(ResetPassword reset)
        {
            if (string.IsNullOrEmpty(reset.Email) ||
                string.IsNullOrEmpty(reset.NewPassword) ||
                string.IsNullOrEmpty(reset.RepeatPassword))
                return new Response { Success = false, Message = "Fill all information" };

            if (reset.NewPassword != reset.RepeatPassword)
                return new Response { Success = false, Message = "رمزهای عبور وارد شده یکسان نمی باشد" };

            if (!reset.NewPassword.IsValidPassword())
                return new Response { Success = false, Message = "رمز عبور باید شامل حداقل 8 کاراکتر، یک حرف بزرگ انگلیسی و یک عدد باشد" };

            if (!reset.RepeatPassword.IsValidPassword())
                return new Response { Success = false, Message = "رمز عبور باید شامل حداقل 8 کاراکتر، یک حرف بزرگ انگلیسی و یک عدد باشد" };

            var user = ByUserName(reset.Email);

            if (user == null) return new Response { Success = false, Message = "شماره همراه ارسالی یافت نشد" };

            user.Password = PasswordHelper.EncodePasswordMd5(reset.NewPassword);
            Update(user);

            return new Response { Success = true, Message = "با موفقیت بازیابی شد" };
        }

        public LoginResponse Login(Login login)
        {
            if (login == null)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Fill all information."
                };
            }

            if (string.IsNullOrEmpty(login.UserName) || string.IsNullOrEmpty(login.Password))
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Fill all information."
                };
            }

            if (!IsUserExists(login.UserName))
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "The user not exists."
                };
            }

            var user = ByUserName(login.UserName);

            if (user == null)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "The user not exists."
                };
            }

            if (!user.State)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "The user has been deactivated."
                };
            }

            var hashed = PasswordHelper.EncodePasswordMd5(login.Password);

            if (user.Password != hashed)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "The password is wrong."
                };
            }

            var token = GenerateToken(user, login.RememberMe);

            return new LoginResponse
            {
                ExpireDate = login.RememberMe ? DateTime.Now.AddDays(2).ToShortDateString() : DateTime.Now.ToShortDateString(),
                ExpireTime = login.RememberMe ? DateTime.Now.AddDays(2).Hour + ":" + DateTime.Now.AddDays(2).Minute : DateTime.Now.AddHours(2).Hour + ":" + DateTime.Now.AddHours(2).Minute,
                Message = "Successful.",
                Success = true,
                Token = token,
                ThemeMode = "",
                User = new AuthenticateUser
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    State = user.State,
                    UserId = user.UserId,
                    UserName = user.UserName,
                }
            };

        }

        private string GenerateToken(User user, bool remember)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.GivenName, user.FirstName + " " + user.LastName)
            };

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: remember ? DateTime.Now.AddDays(2) : DateTime.Now.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ForgotPasswordResponse ForgotPasswordRequest(string? email)
        {
            if (email == null)
                return new ForgotPasswordResponse { Success = false, Message = "Enter an Email" };

            var user = ByUserName(email);

            if (user == null)
                return new ForgotPasswordResponse
                { Success = false, Message = "no account found with that email" };


            Update(user);

            var mail = _email.SendForgotPasswordCodeEmail(email);
            user.ActiveCode = mail;
            return new ForgotPasswordResponse { Success = true, Code = mail, Message = "", Phone = email };

        }

        public RegisterRequestResponse RegisterRequest(string? email)
        {
            if (email == null) return new RegisterRequestResponse { Success = false, Message = "Enter an email" };

            var code = _email.SendRegisterCodeEmail(email);
            return new RegisterRequestResponse { Success = true, Code = code, Message = "", Email = email };

        }

        public RegisterResponse Register(Register register)
        {
          
            bool before = false;
            var user = new User();

            if (IsUserExists(register.Email))
            {
                user = ByUserName(register.Email);
                if (user != null)
                {
                    if (user.State)
                    {
                        return new RegisterResponse { Success = false, Message = "That email address is already in use" };
                    }
                    else
                    {
                        before = true;
                    }

                }
            }

            if (!before)
            {
                user = new User()
                {
                    
                    UserName = register.Email,
                    Password = PasswordHelper.EncodePasswordMd5(register.Password),
                    ActiveCode = CodeGenerator.GenerateUniqueCode(),
                    FirstName = register.FirstName,
                    LastName = register.LastName,
                    RegisterDate = DateTime.Now,
                    State = true,
                    QrToken = TokenGenerator.GenerateUniqCode(),
                    UserAvatar = "default.png",
                    RoleRef = (int)UserRoles.User,
                };

                Create(user);
                var token = GenerateToken(user, false);
                return new RegisterResponse
                {
                    Message = "done successfully",
                    Success = true,
                    Token = token,
                    ThemeMode = "Light",
                    ExpireDate = DateTime.Now.ToShortDateString(),
                    ExpireTime = DateTime.Now.AddHours(2).Hour + ":" + DateTime.Now.AddHours(2).Minute,

                };
            }
            else
            {
                if (user == null)
                    return new RegisterResponse
                    {
                        Message = "somthing went wrong please try again later",
                        Success = false
                    };
                user.State = true;
                Update(user);
                return new RegisterResponse
                {
                    Message = "done successfully",
                    Success = true
                };

            }
        }

        public User? ByUserName(string userName)
        {
            return _context.Users.FirstOrDefault(x => x.UserName == userName);
        }

        public bool IsUserExists(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName)) return false;

            userName = userName.ToLower();

            return _context.Users.Any(u => u.UserName == userName && u.State);
        }

        public T Create<T>(T entity)
        {
            if (entity == null) return entity;
            _context.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public T Update<T>(T entity)
        {
            if (entity == null) return entity;

            _context.Update(entity);
            _context.SaveChanges();

            return entity;
        }

        public UserLocationDTO GetUserLocationById(int userId)
        {
            var result = new UserLocationDTO();
            //{
            //    User = new UserDTO()
            //};
            //var x = _context.Users
            //    .Include(x => x.UserLocation)
            //    .FirstOrDefault(u => u.UserId == userId);
            //if (x == null)
            //{
            //  result.Success = false;
            //  result.Message = "location not found";
            //  return result;
            //}
            //result.User.Address = x.UserLocation.Address;
            //result.User.FirstName = x.FirstName;
            //result.User.LastName = x.LastName;
            //result.User.State = x.State;
            //result.User.Creator = x.Creator;
            //result.User.Latitude = x.UserLocation.Latitude;
            //result.User.Longitude = x.UserLocation.Longitude;
            //result.User.RoleRef = x.RoleRef;
            //result.User.UserAvatar = x.UserAvatar;
            //result.User.UserId = x.UserId;
            //result.User.LocationId = x.UserLocation.LocationId;
            //result.User.RegisterDate = x.RegisterDate;

            //result.Success = true;
            //result.Message = "";

            return result;
        }

        public byte[] GenerateQrCodeBitmap(string userName)
        {

            var qrToken = _context.Users.First(x => x.UserName == userName).QrToken;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(qrToken, QRCodeGenerator.ECCLevel.Q);
            var qRCode = new PngByteQRCode(qrCodeData);
            var qrCodeBytes = qRCode.GetGraphic(60);

            //todo: save in static files            

            return qrCodeBytes;
        }

        public UserInfo GetCurrentUser(string userName)
        {
          var user = _context.Users.Include(x=> x.Role)
                    .First(x=> x.UserName == userName);
            if (user != null)
            {
                return new UserInfo()
                {
                    UserId = user.UserId,
                    FullName = user.FirstName + " " + user.LastName,
                    Role = user.Role.Title,
                    UserAvatar = user.UserAvatar,
                    Message = "",
                    Success = true
                };
            }
            return new UserInfo()
            {
                
                Message = "Not Found",
                Success = false
            };


        }
    }
}
