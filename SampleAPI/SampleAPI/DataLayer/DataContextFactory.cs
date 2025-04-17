using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SampleAPI.DataLayer;
using System.IO;

namespace SampleAPI.DataLayer
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            // Build config
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // or wherever your .csproj/appsettings.json are located
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Create DbContextOptionsBuilder
            var builder = new DbContextOptionsBuilder<DataContext>();
            var connectionString = configuration.GetConnectionString("CS");

            builder.UseSqlServer(connectionString);

            return new DataContext(builder.Options);
        }
    }
}