﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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

        public DbSet<Service> Services { get; set; }

        protected override async void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Request>()
                .HasOne(x => x.Desk)
                .WithMany(x => x.Requests)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
