using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Runtime;
using System.Security.Claims;
using waterfood.Core.Objects.Accounts;
using waterfood.Core.Objects.Externals;
using waterfood.Core.Services.Interfaces;

namespace waterfood.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ReCaptchaSettings _settings;
        private readonly IAccountService _accountService;

        public AccountController(ReCaptchaSettings settings, IAccountService accountService)
        {
            _settings = settings;
            _accountService = accountService;
        }

        [Route("/login")]
        public IActionResult Login()
        {
            return User.Identity.IsAuthenticated ? Redirect("/") : View();
        }

        [Route("/loginAjax/{mail}")]
        public async Task<JsonResult> LoginAjax(string mail, string password, bool rememberMe, string google, string returnPage = null)
        {
            var login = new Login()
            {
                Password = password,
                UserName = mail,
                RememberMe = rememberMe
            };


            if (string.IsNullOrEmpty(google))
            {
                return Json("Please complete captcha!");
            }

            var httpResponse = await new HttpClient()
            .GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={_settings.SecretKey}&response={google}");

            var jsonRes = await httpResponse.Content.ReadAsStringAsync();
            var googleResponse = JsonConvert.DeserializeObject<GoogleResponse>(jsonRes);

            if (googleResponse != null)
            {
                if (!googleResponse.Success)
                {
                    return Json("Please complete captcha!");
                }
                else
                {

                    var result = _accountService.Login(login);
                    if (result.Success)
                    {
                        CreateIdentityObject(result.User, login.RememberMe);

                        return Json("correct");
                    }
                    else
                    {
                        return Json("wrong");
                    }
                }
            }
            else
            {
                return Json("Please complete captcha!");
            }
        }

        private void CreateIdentityObject(AuthenticateUser user, bool rememberMe)
        {
            var websiteClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.UserName),
                new Claim("FullName", user.FirstName + " " + user.LastName)
            };

            var siteIdentity = new ClaimsIdentity(websiteClaims, "Website Identity");
            //var licenseIdentity = new ClaimsIdentity(licenseClaims, "Government");

            var userPrincipal = new ClaimsPrincipal(new[] { siteIdentity });
            //-----------------------------------------------------------
            HttpContext.SignInAsync(userPrincipal);
        }

        [Route("/Logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return Redirect("/");
        }
    }
}
