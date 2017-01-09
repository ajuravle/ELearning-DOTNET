using ELearning.Model;
using Microsoft.EntityFrameworkCore;

namespace ELearning.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
        {
        }

        public DbSet<UniversityUser> UniversityUsers { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<FastQuestion> FastQuestion { get; set; }
        public DbSet<FastAnswer> FastAnswer { get; set; }
    }
}
