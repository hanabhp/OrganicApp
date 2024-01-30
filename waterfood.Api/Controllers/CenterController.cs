using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using waterfood.Core.Objects.Accounts;
using waterfood.Core.Objects.Centers;
using waterfood.Core.Services.Interfaces;

namespace waterfood.Api.Controllers
{
    [Route("v1/api/[controller]/[action]")]
    [ApiController]
    public class CenterController : ControllerBase
    {
        private readonly ICenterService _centerService;
        public CenterController(ICenterService centerService)
        {
            _centerService = centerService;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Centers()
        {
            var user = CurrentUser();
            return Ok(_centerService.GetCenters(user.UserId));
        }
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Center(int id)
        {
            var user = CurrentUser();
            return Ok(_centerService.GetCenterById(id,user.UserId));
        }
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult CenterItems(int id)
        {
            return Ok(_centerService.GetCenterItemsById(id));
        }
        [Authorize]
        [HttpGet]
        public IActionResult FavoriteCenters()
        {
            var user = CurrentUser();
            return Ok(_centerService.GetFavoriteCenters(user.UserId));
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddToFavorite([FromBody] FavoriteCentersList center)
        {
            var user = CurrentUser();
            return Ok(_centerService.SetCenterAsFavoriteById(center.CenterId, user.UserId));
              
          

        }
        [Authorize]
        [HttpPost]
        public IActionResult RemoveFromFavorite([FromBody] FavoriteCentersList center)
        {
            var user = CurrentUser();
            if (user != null)
            {
                return Ok(_centerService.RemoveCenterAsFavoriteById(center.CenterId, user.UserId));
            }
            else
            {
                return Ok("login pleaase");
            }

        }
        [Authorize]
        [HttpGet]
        public IActionResult GetCentersLocation()
        {
            var user = CurrentUser();
            if (user != null)
            {
                return Ok(_centerService.GetAllCenterLocations(user.UserId));
            }
            else
            {
                return Ok("login pleaase");
            }

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
