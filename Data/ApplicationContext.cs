
using Microsoft.EntityFrameworkCore;
using Osvip.Api.Models;

namespace Osvip.Api.Data
{
    /// <summary>
    /// Сущность взаимодействия с базой данных
    /// </summary>
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
            
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //установка связи 1 к 1 черещ FluttenAPI
            modelBuilder.Entity<UserToken>()
                .HasOne(x => x.user)
                .WithOne(x => x.Token)
                .HasPrincipalKey<User>(x => x.UserId);

          
            //Схема по стандарту
            modelBuilder.HasDefaultSchema("public");

            
        }

        /// <summary>
        /// Установка моделей в конткест 
        /// </summary>
        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<Direction> Directions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestResponse> TestResponses { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<TestQuestion> Questions { get; set; }
        public DbSet<UsersTest> UsersTests { get; set; }
        
    }
}


