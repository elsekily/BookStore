using BookStoreLibrary.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreLibrary.Persistence
{
    public class BookStoreDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>,
        UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityUserLogin<int>>(x => x.Property(m => m.LoginProvider).HasMaxLength(255));
            builder.Entity<IdentityUserLogin<int>>(x => x.Property(m => m.ProviderKey).HasMaxLength(255));

            builder.Entity<IdentityUserToken<int>>(x => x.Property(m => m.LoginProvider).HasMaxLength(255));
            builder.Entity<IdentityUserToken<int>>(x => x.Property(m => m.Name).HasMaxLength(255));

            builder.Entity<Book>().HasOne(b => b.User).WithMany(u => u.Books);
            builder.Entity<Author>().HasOne(b => b.User).WithMany(u => u.Authors);
            builder.Entity<Purchase>().HasOne(b => b.User).WithMany(u => u.Purchases);

        }
    }
}
