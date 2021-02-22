using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace userManager.Infra.Data.Context
{
    public class UserManagerContext : IdentityDbContext
    {
        public UserManagerContext(DbContextOptions<UserManagerContext> options) : base(options) { }

    }
}
