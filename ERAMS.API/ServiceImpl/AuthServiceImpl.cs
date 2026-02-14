using ERAMS.API.Data;
using ERAMS.API.Dto;
using ERAMS.API.Helper;
using ERAMS.API.Model;
using ERAMS.API.Service;
using Microsoft.AspNetCore.Http.HttpResults;
//using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;

namespace ERAMS.API.ServiceImpl
{
    public class AuthServiceImpl : IAuthService
    {
        private readonly ContextClass _context;
        private readonly JwtTokenGenerate _jwt;

        public AuthServiceImpl(ContextClass context, JwtTokenGenerate jwt)
        {
            _context = context;
            _jwt = jwt;

        }
        public async Task Register( RegisterRequest register)
        {
            var user = new User
            {
                Username = register.Username,
                Email = register.Email,
                Password = PasswordHasher.HashPassword(register.Password),
                RoleId = register.RoleId
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

        }

        public async Task<string> LogIn(LoginRequest request)
        {
            var hash = PasswordHasher.HashPassword(request.Password);
            

            var user = await _context.Users.Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == request.Email && x.Password==hash);
            if (user== null)
            {
                throw new Exception("Please enter valid Crendistials");
            }
            return _jwt.GenerateToken(user);
        }



    }
}
