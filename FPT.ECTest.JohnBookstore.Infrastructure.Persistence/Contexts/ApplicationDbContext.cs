using FPT.ECTest.JohnBookstore.Application.Interfaces;
using FPT.ECTest.JohnBookstore.Domain.Common;
using FPT.ECTest.JohnBookstore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FPT.ECTest.JohnBookstore.Infrastructure.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Order> Orders { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //All Decimals will have 18,6 Range
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,6)");
            }

            builder.Entity<Shop>()
                .HasMany(s => s.ShopBooks)
                .WithOne(b => b.Shop)
                .HasForeignKey(s => s.ShopId);

            builder.Entity<Book>()
                .HasMany(s => s.ShopBooks)
                .WithOne(b => b.Book)
                .HasForeignKey(s => s.BookId);

            builder.Entity<Order>()
                .HasOne(a => a.Shop)
                .WithMany(a => a.Orders)
                .HasForeignKey(c => c.ShopId);

            builder.Entity<Order>()
                .HasOne(a => a.Book)
                .WithMany(a => a.Orders)
                .HasForeignKey(c => c.BookId);

            base.OnModelCreating(builder);

        }
    }
}
