
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SeederTool.Models;
using Newtonsoft.Json;
using SeederTool.Services;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace SampleDataTool
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using IHost host = CreateHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();
            var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();

            var result = seeder.SeedFromJson();
            Console.WriteLine(result);
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: false);
            })

                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<DataContext>(options =>
                        options.UseSqlServer(context.Configuration.GetConnectionString("DefaultConnection")));

                    services.AddScoped<DataSeeder>();
                });
    }
}


namespace SeederTool.Models
{
    public class Data
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public DateTime Birthdate { get; set; }

        public Address Address { get; set; }
        public telephones telephones { get; set; }
        public List<UserInstitutions> UserInstitutions { get; set; }
    }
}


namespace SeederTool.Models
{
    public class Address
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        public Data Data { get; set; }
    }
}


namespace SeederTool.Models
{
    public class telephones
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PhoneNumber { get; set; }
        public string Type { get; set; }

        public Data Data { get; set; }
    }
}

namespace SeederTool.Models
{
    public class Institutions
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Domain { get; set; }

        public List<UserInstitutions> UserInstitutions { get; set; }
    }
}


namespace SeederTool.Models
{
    public class UserInstitutions
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int InstitutionId { get; set; }

        public Data Data { get; set; }
        public Institutions Institutions { get; set; }
    }
}


namespace SeederTool.Models
{
    public class SeedDataRoot
    {
        public List<Data> Users { get; set; }
        public List<Address> Addresses { get; set; }
        public List<telephones> telephones { get; set; }
        public List<Institutions> Institutions { get; set; }
        public List<UserInstitutions> UserInstitutions { get; set; }
    }
}


namespace SeederTool.Services
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Data> Data { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<telephones> telephones { get; set; }
        public DbSet<Institutions> Institutions { get; set; }
        public DbSet<UserInstitutions> UserInstitutions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Data>()
                .HasOne(u => u.Address)
                .WithOne(a => a.Data)
                .HasForeignKey<Address>(a => a.UserId);

            modelBuilder.Entity<Data>()
                .HasOne(u => u.telephones)
                .WithOne(t => t.Data)
                .HasForeignKey<telephones>(t => t.UserId);

            modelBuilder.Entity<UserInstitutions>()
    .HasKey(ui => ui.Id); 

            modelBuilder.Entity<UserInstitutions>()
                .HasOne(ui => ui.Data)
                .WithMany(u => u.UserInstitutions)
                .HasForeignKey(ui => ui.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserInstitutions>()
                .HasOne(ui => ui.Institutions)
                .WithMany(i => i.UserInstitutions)
                .HasForeignKey(ui => ui.InstitutionId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }

}

namespace SeederTool.Services
{
    public class DataSeeder
    {
        private readonly DataContext _context;

        public DataSeeder(DataContext context)
        {
            _context = context;
        }

        public string SeedFromJson()
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "sampleuserdata.json");
                if (!File.Exists(path)) return " JSON file not found.";

                var json = File.ReadAllText(path);
                var data = JsonConvert.DeserializeObject<SeedDataRoot>(json);

                if (data == null) return " Invalid or empty JSON.";

                using var connection = _context.Database.GetDbConnection();
                connection.Open();

                try
                {

                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Data] ON");
                    _context.Data.AddRange(data.Users);
                    _context.SaveChanges();
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Data] OFF");

                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Institutions] ON");
                    _context.Institutions.AddRange(data.Institutions);
                    _context.SaveChanges();
                    _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Institutions] OFF");

                    _context.Addresses.AddRange(data.Addresses);
                    _context.SaveChanges();

                    _context.telephones.AddRange(data.telephones);
                    _context.SaveChanges();

                    _context.UserInstitutions.AddRange(data.UserInstitutions);
                    _context.SaveChanges();

                    return " Seeded successfully.";
                }
                catch
                {
                   
                    throw;
                }
            }
            catch (Exception ex)
            {
                return $"Exception during seeding: {ex.Message}";
            }
        }
    }
}
