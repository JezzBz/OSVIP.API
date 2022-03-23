
using Microsoft.EntityFrameworkCore;
using Osvip.Api.Models;

namespace Osvip.Api.Data
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
            
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserToken>()
                .HasOne(x => x.user)
                .WithOne(x => x.Token)
                .HasPrincipalKey<User>(x => x.UserId);
            modelBuilder.HasDefaultSchema("public");
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<Direction> Directions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestResponse> TestResponses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
    }
}
