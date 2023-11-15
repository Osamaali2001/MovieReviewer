using Microsoft.EntityFrameworkCore;
using MovieReviewer.Models;

namespace MovieReviewer.Data
{
    public class ApplicationDbContext : DbContext 
    {
        public DbSet<User> User { get; set; }
        public DbSet<Movie> Movie { get; set; }
        public DbSet<Actor> Actor { get; set; }
        public DbSet<Director> Director { get; set; }
        public ApplicationDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder model)
        {
            model.ApplyConfiguration(new UserConfiguration());
        }
    }
}
