using Microsoft.EntityFrameworkCore;

namespace Final.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TeamMember> TeamMembers { get; set; }
        // Add other DbSets here
    }
}
