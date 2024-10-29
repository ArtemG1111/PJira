
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PJira.Application.Common.Interfaces;
using PJira.Core.Models;
using System.Reflection;

namespace PJira.Infrastructure.Data
{
    public class PJiraDbContext : IdentityDbContext<IdentityUser>, IPJiraDbContext
    {
        public DbSet<Assignment> Assignments { get; set; }

        public DbSet<Project> Projects { get; set; }

        public PJiraDbContext(DbContextOptions<PJiraDbContext> options) : base (options)
        {
 
            Database.EnsureCreated();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
