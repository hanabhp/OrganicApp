using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using waterfood.Core.Services.Interfaces;
using waterfood.Data.Context;

namespace waterfood.Api.Controllers
{
    [Route("v1/api/[controller]/[action]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public IActionResult Roles()
        {
            return Ok(_roleService.GetRoles());
        }

        //lambda
    }
}
