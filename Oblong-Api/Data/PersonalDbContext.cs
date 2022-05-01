using Microsoft.EntityFrameworkCore;
using Oblong_Api.Models;

namespace Oblong_Api.Data
{
    public class PersonalDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        public PersonalDbContext(DbContextOptions<PersonalDbContext> options, IConfiguration confguration) : base(options)
        {
            _configuration = confguration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to mysql with connection string from app settings
            var connectionString = _configuration.GetConnectionString("WebApiDatabase");
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        public virtual DbSet<SiteAccess> SiteAccesses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SiteAccess>().ToTable("site_access");
        }
    }
}
