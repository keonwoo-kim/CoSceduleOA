using CoScheduleOA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;

namespace CoScheduleOA.Domain.Context
{
    public class CoScheduleOAContext : DbContext
    {
        public CoScheduleOAContext(DbContextOptions<CoScheduleOAContext> options) : base(options) { }

        public virtual DbSet<Rating> Ratings => Set<Rating>();
        public virtual DbSet<Comment> Comments => Set<Comment>();
        public virtual DbSet<User> Users => Set<User>();
        public virtual DbSet<Item> Items => Set<Item>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            b.HasDefaultSchema("public");

            b.Entity<Comment>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Content).IsRequired();
                e.Property(x => x.CreatedUtc)
                 .HasDefaultValueSql("timezone('utc', now())");
                e.Property(x => x.UpdatedUtc)
                 .HasDefaultValueSql("timezone('utc', now())");
                e.Property(x => x.IsDeleted)
                 .HasDefaultValue(false);

                e.HasOne(c => c.User)
                 .WithMany(u => u.Comments)
                 .HasForeignKey(c => c.UserId);

                e.HasOne(c => c.Item)
                 .WithMany(i => i.Comments)
                 .HasForeignKey(c => c.ItemId);

                e.HasQueryFilter(c => !c.IsDeleted);
            });

            b.Entity<Rating>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Value).IsRequired();
                e.Property(x => x.CreatedUtc)
                 .HasDefaultValueSql("timezone('utc', now())");
                e.Property(x => x.UpdatedUtc)
                 .HasDefaultValueSql("timezone('utc', now())");
                e.Property(x => x.IsDeleted)
                 .HasDefaultValue(false);

                e.HasOne(r => r.User)
                 .WithMany(u => u.Ratings)
                 .HasForeignKey(r => r.UserId);

                e.HasOne(r => r.Item)
                 .WithMany(i => i.Ratings)
                 .HasForeignKey(r => r.ItemId);

                e.HasQueryFilter(r => !r.IsDeleted);
            });

            b.Entity<User>(e =>
            {
                e.HasKey(x => x.Id);
                e.HasIndex(x => x.UserId).IsUnique();
                e.Property(x => x.UserId).IsRequired();
                e.Property(x => x.UserName).IsRequired();
                e.Property(x => x.PasswordHash).IsRequired();
                e.Property(x => x.CreatedUtc)
                 .HasDefaultValueSql("timezone('utc', now())");
                e.Property(x => x.UpdatedUtc)
                 .HasDefaultValueSql("timezone('utc', now())");
                e.Property(x => x.IsDeleted)
                 .HasDefaultValue(false);

                e.HasQueryFilter(u => !u.IsDeleted);
            });

            b.Entity<Item>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Source).IsRequired();
                e.Property(x => x.ExternalId).IsRequired();
                e.Property(x => x.Title).IsRequired();
                e.Property(x => x.Url).IsRequired();
                e.Property(x => x.CreatedUtc)
                 .HasDefaultValueSql("timezone('utc', now())");
                e.Property<bool>("IsDeleted")
                 .HasDefaultValue(false);

                e.HasQueryFilter(i => EF.Property<bool>(i, "IsDeleted") == false);
            });
        }

        public override int SaveChanges()
        {
            ApplySoftDeleteRules();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplySoftDeleteRules();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplySoftDeleteRules()
        {
            var now = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries())
            {
                var prop = entry.Entity.GetType().GetProperty("IsDeleted",
                    BindingFlags.Public | BindingFlags.Instance);
                if (prop == null)
                    continue;

                switch (entry.State)
                {
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        prop.SetValue(entry.Entity, true);
                        entry.CurrentValues["UpdatedUtc"] = now;
                        break;

                    case EntityState.Added:
                        entry.CurrentValues["CreatedUtc"] = now;
                        entry.CurrentValues["UpdatedUtc"] = now;
                        prop.SetValue(entry.Entity, false);
                        break;

                    case EntityState.Modified:
                        entry.CurrentValues["UpdatedUtc"] = now;
                        break;
                }
            }
        }
    }
}