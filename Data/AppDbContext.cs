using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
      
        private string DbPath { get; set; }
        public DbSet<ContactEntity> Contacts { get; set; }
        public DbSet<OrganizationEntity> Organizations { get; set; }
        public AppDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "contacts.db");
        }
       


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ContactEntity>()
                .HasOne(e => e.Organization)
                .WithMany(o => o.Contacts)
                .HasForeignKey(e => e.OrganizationId);

            base.OnModelCreating(modelBuilder);

            string ADMIN_ID = Guid.NewGuid().ToString();
            string ROLE_ID = Guid.NewGuid().ToString();

            // dodanie roli administratora
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "admin",
                NormalizedName = "ADMIN",
                Id = ROLE_ID,
                ConcurrencyStamp = ROLE_ID
            });

            // utworzenie administratora jako użytkownika
            var admin = new IdentityUser
            {
                Id = ADMIN_ID,
                Email = "adam@wsei.edu.pl",
                EmailConfirmed = true,
                UserName = "adam",
                NormalizedUserName = "ADMIN"
            };

            // haszowanie hasła
            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
            admin.PasswordHash = ph.HashPassword(admin, "gawor");

            // zapisanie użytkownika
            modelBuilder.Entity<IdentityUser>().HasData(admin);

            // przypisanie roli administratora użytkownikowi
            modelBuilder.Entity<IdentityUserRole<string>>()
            .HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });

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