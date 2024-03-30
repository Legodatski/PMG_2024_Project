using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization;

namespace TaxSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Desk> Desks { get; set; }

        public DbSet<Request> Requests { get; set; }

        public DbSet<Amenity> Services { get; set; }

        public DbSet<DeskAmenity> DeskService { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Request>()
                .HasOne(x => x.Desk)
                .WithMany(x => x.Requests)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<DeskAmenity>()
                .HasKey(x => new { x.ServiceId, x.DeskId });

            builder.Entity<DeskAmenity>()
                .HasOne(x => x.Desk)
                .WithMany(x => x.Amenities)
                .HasForeignKey(x => x.DeskId);

            AlterUser(builder);
        }

        private void AlterUser(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .Property(x => x.PhoneNumber)
                .IsRequired(true);
        }
    }
}
