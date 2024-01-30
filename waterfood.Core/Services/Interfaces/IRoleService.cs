using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using waterfood.Data.Entities.Users;

namespace waterfood.Core.Services.Interfaces
{
    public interface IRoleService
    {
        List<Role> GetRoles();
    }
}
