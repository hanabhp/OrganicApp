using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using waterfood.Core.Objects.Accounts;
using waterfood.Core.Objects.Reserves;
using waterfood.Core.Services;
using waterfood.Core.Services.Interfaces;

namespace waterfood.Api.Controllers
{
    [Route("v1/api/[controller]/[action]")]
    [ApiController]
    public class ReserveController : ControllerBase
    {
        private readonly IReserveService _reserveService;

        public ReserveController(IReserveService reserveService)
        {
            _reserveService = reserveService;
        }
        [Authorize]
        [HttpPost]
        public IActionResult ReserveAnItem([FromBody]ReserveAnItemDTO reserve)
        {
             var user = CurrentUser();
             return Ok(_reserveService.ReserveAnItem(reserve.ItemId, reserve.CenterId, user));
         
          
        }
        [Authorize]
        [HttpPost]
        public IActionResult CheckReservedItemByQrToken([FromBody] QrTokenDTO qr)
        {
            var user = CurrentUser();
            return Ok(_reserveService.CheckUserReservedItemsByQrToken(qr.QeToken,user.UserId));
        }
        [HttpGet]
        public IActionResult GetAllItemStatuses()
        {
            return Ok(_reserveService.GetAllItemStatuses());
        }
        [Authorize]
        [HttpPost]
        public IActionResult SetItemStatusByCenterOwner(List<ReserveItemsCheckList> items)
        {
            var user = CurrentUser();
            return Ok(_reserveService.SetItemStatusByCenterOwner(items,user.UserId));
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
