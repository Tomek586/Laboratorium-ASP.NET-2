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

            // Remove the duplicate call to base.OnModelCreating(modelBuilder)
            // base.OnModelCreating(modelBuilder);

            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();

            var user = new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "tomek",
                Email = "tomek@gmail.com",
                EmailConfirmed = true,
                NormalizedEmail = "TOMEK@GMAIL.COM",
                NormalizedUserName = "TOMEK"
            };

            // Hash the password
            user.PasswordHash = ph.HashPassword(user, "Tomek123!");

            modelBuilder.Entity<IdentityUser>()
                .HasData(user);

            var user2 = new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "jacek",
                Email = "jacek@gmail.com",
                EmailConfirmed = true,
                NormalizedEmail = "JACEK@GMAIL.COM",
                NormalizedUserName = "JACEK"
            };

            // Hash the password for user2
            user2.PasswordHash = ph.HashPassword(user2, "Jacek123!");

            modelBuilder.Entity<IdentityUser>()
                .HasData(user2);

            // Adding the administrator role
            var adminRole = new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "admin",
                NormalizedName = "ADMIN"
            };

            adminRole.ConcurrencyStamp = adminRole.Id;
            modelBuilder.Entity<IdentityRole>()
                .HasData(adminRole);

            // Adding the relationship between user2 and the admin role
            modelBuilder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string>()
                {
                    RoleId = adminRole.Id,
                    UserId = user2.Id
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