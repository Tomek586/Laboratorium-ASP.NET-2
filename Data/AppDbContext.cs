using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ContactEntity> Contacts { get; set; }
        public DbSet<OrganizationEntity> Organizations { get; set; }
        private string DbPath { get; set; }
        public AppDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "contacts.db");
        }

      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<ContactEntity>()
                .HasOne(e => e.Organization)
                .WithMany(o => o.Contacts)
                .HasForeignKey(e => e.OrganizationId);

            modelBuilder.Entity<OrganizationEntity>().HasData(
                 new OrganizationEntity()
                 {
                     Id = 1,
                     Name = "WSEI",
                     Description = "wsei krakow",
                    
                 },
                 new OrganizationEntity()
                 {
                     Id = 2,
                     Name = "AGH",
                     Description = "agh krakow",
                    
                 }
             ); ;
            modelBuilder.Entity<ContactEntity>().HasData(
               new ContactEntity()
               {
                   ContactId = 1,
                   Name = "AA",
                   Email = "Adam",
                   Phone = "13424234",
                   OrganizationId = 1,

               },
               new ContactEntity()
               {
                   ContactId = 2,
                   Name = "C#",
                   Email = "Ewa",
                   Phone = "02879283",
                   OrganizationId = 1,
               }
           );
            modelBuilder.Entity<OrganizationEntity>()
    .OwnsOne(e => e.Adress)
    .HasData(
        new { OrganizationEntityId = 1, City = "Kraków", Street = "Św. Filipa 17", PostalCode = "31-150", Region = "małopolskie" },
        new { OrganizationEntityId = 2, City = "Kraków", Street = "Krowoderska 45/6", PostalCode = "31-150", Region = "małopolskie" }
    );
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }

    }
}
