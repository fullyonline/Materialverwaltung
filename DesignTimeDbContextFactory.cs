using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Materialverwaltung.Data;

namespace Materialverwaltung
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            return new ApplicationDbContext(builder.Options);
        }
    }
}
