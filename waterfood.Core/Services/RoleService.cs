using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using waterfood.Core.Services.Interfaces;
using waterfood.Data.Context;
using waterfood.Data.Entities.Users;

namespace waterfood.Core.Services
{
    public class RoleService : IRoleService
    {
        private readonly WaterFoodContext _context;

        public RoleService(WaterFoodContext context)
        {
            _context = context;
        }

        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }
    }
}
