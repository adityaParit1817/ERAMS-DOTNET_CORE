using ERAMS.API.Data;
using ERAMS.API.Helper;
using ERAMS.API.Model;
using ERAMS.API.Service;
using Microsoft.AspNetCore.Http.HttpResults;
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
        public async Task Register(string username , string password , string email , int roled)
        {
            var user = new User
            {
                Username = username,
                Email = email,
                Password = PasswordHasher.HashPassword(password),
                RoleId = roled,
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

        }

        public async Task<string> LogIn(string email , string password)
        {
            var hash = PasswordHasher.HashPassword(password);
            var user = await _context.Users.Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == email && x.Password == hash);

            if(user== null)
            {
                throw new Exception("Please enter valid Crendistials");
            }
            return _jwt.GenerateToken(user);
        }



    }
}
