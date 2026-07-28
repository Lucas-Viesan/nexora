using Microsoft.EntityFrameworkCore;

namespace Nexora.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        {
            
        }


    }
}
