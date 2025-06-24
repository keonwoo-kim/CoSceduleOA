using Microsoft.EntityFrameworkCore;

namespace CoScheduleOA.Domain
{
    public class CoSceduleOAContext : DbContext
    {
        public CoSceduleOAContext(DbContextOptions<CoSceduleOAContext> options) : base(options) { }

        public virtual DbSet<Rating> Ratings => Set<Rating>();
        public virtual DbSet<Comment> Comments => Set<Comment>();
        public virtual DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<Rating>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Value).IsRequired().HasConversion<short>();
                e.HasIndex(x => new { x.ItemId, x.UserId }).IsUnique();
                e.Property(x => x.CreatedUtc).HasDefaultValueSql("timezone('utc', now())");
                e.Property(x => x.UpdatedUtc).HasDefaultValueSql("timezone('utc', now())");
            });

            b.Entity<Comment>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasIndex(x => x.ItemId);
                e.Property(x => x.Content).IsRequired();
                e.Property(x => x.CreatedUtc).HasDefaultValueSql("timezone('utc', now())");
                e.Property(x => x.UpdatedUtc).HasDefaultValueSql("timezone('utc', now())");
            });

            b.Entity<User>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasIndex(x => x.UserId).IsUnique();
                e.Property(x => x.CreatedUtc)
                 .HasDefaultValueSql("timezone('utc', now())");
                e.Property(x => x.UpdatedUtc)
                 .HasDefaultValueSql("timezone('utc', now())");
            });
        }
    }
}
