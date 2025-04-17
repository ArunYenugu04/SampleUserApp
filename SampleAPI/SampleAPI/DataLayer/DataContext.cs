using Microsoft.EntityFrameworkCore;
using SampleAPI.Models;

namespace SampleAPI.DataLayer
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
