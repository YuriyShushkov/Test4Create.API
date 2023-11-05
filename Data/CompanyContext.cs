using Microsoft.EntityFrameworkCore;
using Test4Create.API.Entities;

namespace Test4Create.API.Data
{
    public class CompanyContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public CompanyContext(IConfiguration configuration) => _configuration = configuration;

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<CompanyEmployee> CompanyEmployee { get; set; }
        public DbSet<SystemLog> SystemLog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("PostgreSQL"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
