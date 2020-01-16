using Microsoft.EntityFrameworkCore;

namespace Auth
{
    
        public class DbContext : Microsoft.EntityFrameworkCore.DbContext
        {
            public DbContext(DbContextOptions<DbContext> options) : base(options)
            { }
            public DbSet<RefreshToken> Tokens { get; set; }
            public DbSet<Users> Users { get; set; }
            public DbSet<AuthTokens> AuthTokens { get; set; }

        }
    
}
