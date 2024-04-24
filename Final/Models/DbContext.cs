using Microsoft.EntityFrameworkCore;

namespace Final.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<Hobby> Hobbies { get; set; }
        public DbSet<FavoriteBreakfastFood> FavoriteBreakfastFoods { get; set; }
        public DbSet<Book> Books { get; set; }

    }
}
