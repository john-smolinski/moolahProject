using CodeProject.Server.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CodeProject.Server.Context
{
    public class MoolahContext : DbContext
    { 
        public MoolahContext(DbContextOptions<MoolahContext> options) 
            : base(options) 
        { 
        }

        public DbSet<Provider> Providers { get; set; }
        public DbSet<ToDo> ToDos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<Provider>().HasData(
                new Provider { Id = 1, Name = "home" },
                new Provider { Id = 2, Name = "office" });


        }
    }
}
