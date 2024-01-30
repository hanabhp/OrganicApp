using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using waterfood.Core.Objects.Accounts;
using waterfood.Core.Services.Interfaces;

namespace waterfood.Api.Controllers
{
    [Route("v1/api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost]
        public IActionResult Login([FromBody] Login login)
        {
            //_accountService.Init();
            return Ok(_accountService.Login(login));
        }

        [HttpGet("{email}")] //learnofen.ir/api/account/registerRequest?phone=0936
        public IActionResult RegisterRequest([FromRoute] string email)
        {
            return Ok(_accountService.RegisterRequest(email));
        }

        [HttpPost]
        public IActionResult Register([FromBody] Register register)
        {
            return Ok(_accountService.Register(register));
        }

        [HttpGet("{email}")]
        public IActionResult ForgotPasswordRequest([FromRoute] string email)
        {
            return Ok(_accountService.ForgotPasswordRequest(email));
        }

        [HttpPost]
        public IActionResult Reset([FromBody] ResetPassword reset)
        {
            return Ok(_accountService.ResetPassword(reset));
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserLocationById([FromRoute] int userId)
        {
            return Ok(_accountService.GetUserLocationById(userId));
        }
        [Authorize]
        [HttpGet]
        public IActionResult GenerateQrCodeImage()
        {

            var user = CurrentUser().UserName;
            return Ok(_accountService.GenerateQrCodeBitmap(user));
        }
        [Authorize]
        [HttpGet]
        public IActionResult GetCurrentUser()
        {
            var user = CurrentUser().UserName;
            return Ok(_accountService.GetCurrentUser(user));
        }
        private CurrentUser? CurrentUser()
        {
            if (HttpContext.User.Identity is not ClaimsIdentity identity) return null;
            var userClaims = identity.Claims;
            var enumerable = userClaims as Claim[] ?? userClaims.ToArray();
            return new CurrentUser()
            {
                UserName = enumerable.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value,
                UserId = int.Parse(enumerable.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value),
                FullName = enumerable.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value,
            };

        }
    }
}
