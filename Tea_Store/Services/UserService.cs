using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Tea_Store.Data;
using Tea_Store.Models;

namespace Tea_Store.Services
{
    public interface IUserService
    {
        public Task<User> GetUserIdByToken(ClaimsPrincipal User);
    }

    public class UserService : IUserService
    {
        private readonly TeaDBContext _context;

        public UserService(TeaDBContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserIdByToken(ClaimsPrincipal User)
        {
            var email = User.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;
            if (email != null)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
                return user;
            }
            return null;
        }
    }
}
