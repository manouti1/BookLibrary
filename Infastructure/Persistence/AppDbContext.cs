using BookLibrary.Domain;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Infastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.Id);
                entity.Property(u => u.Id)
                    .ValueGeneratedOnAdd();
            });
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.HasIndex(u => u.Username).IsUnique();

                entity.Property(u => u.Id)
                      .ValueGeneratedOnAdd();
            });
        }
    }
}
