using ERAMS.API.Model;
using Microsoft.AspNetCore.Mvc;

namespace ERAMS.API.Service
{
    public interface IRoleService
    {
        public Task<List<Role>> GetAllRoles();

        public Task<IActionResult> CreateRole(Role role);
    }
}
