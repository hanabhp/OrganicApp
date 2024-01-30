using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using waterfood.Core.Objects.Accounts;

namespace waterfood.Api.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        [HttpGet]
        public IActionResult Profile()
        {
            //var profile = _accountService.GetProfile(CurrentUser);
            return Ok();
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
