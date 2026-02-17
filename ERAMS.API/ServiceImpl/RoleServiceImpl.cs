using ERAMS.API.Data;
using ERAMS.API.Model;
using ERAMS.API.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;

namespace ERAMS.API.ServiceImpl
{
    public class RoleServiceImpl : IRoleService
    {
        private readonly ContextClass _context;

        public RoleServiceImpl(ContextClass context)
        {
            _context = context;
        }
        public async Task<List<Role>> GetAllRoles()
        {
           return  await _context.Roles.ToListAsync();

        }

        public async Task<IActionResult> CreateRole(Role role)
        {
            var data = await _context.Roles.FirstOrDefaultAsync(x => x.Name == role.Name);
            if (data != null)
            {
                return new BadRequestObjectResult("Role already exist");
            }
            else
            {
                _context.Roles.Add(role);
                await _context.SaveChangesAsync();
                return new OkObjectResult("Role created successfully");
               // return Ok("Role created successfully",data);


            }
        }
          
    }
}
