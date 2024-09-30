

using Microsoft.EntityFrameworkCore;
using PJira.Core.Models;
using System.Reflection;

namespace PJira.Infrastructure.Data
{
    public class PJiraDbContext : DbContext
    {
        public DbSet<Tasks> Tasks { get; set; }

        public PJiraDbContext(DbContextOptions<PJiraDbContext> options) : base (options)
        {
            Database.EnsureCreated();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
