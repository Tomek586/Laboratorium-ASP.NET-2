using Data;
using Data.Entities;
using Laboratorium_3___App.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace Lab3___Aplikacja.Models
{
    public class EFContactService : IContactService
    {
        private readonly AppDbContext _context;

        public EFContactService(AppDbContext context)
        {
            _context = context;
        }

        public int Add(Contact contact)
        {
            var entity = _context.Contacts.Add(ContactMapper.ToEntity(contact));
            int id = entity.Entity.ContactId;
            _context.SaveChanges();
            return id;
        }

        public void Update(Contact contact)
        {
            ContactEntity entity = ContactMapper.ToEntity(contact);
            _context.Update(entity);
            _context.SaveChanges();
            
        }

        public void Delete(int id)
        {
            ContactEntity? find = _context.Contacts.Find(id);
            if (find != null)
            {
                _context.Contacts.Remove(find);
                _context.SaveChanges();
            }
        }

        public List<Contact> FindAll()
        {
            return _context.Contacts.Select(e => ContactMapper.FromEntity(e)).ToList();
        }

        public Contact? FindById(int id)
        {
            ContactEntity? find = _context.Contacts.Find(id);
            return find == null ? null : ContactMapper.FromEntity(find);
        }

        public List<OrganizationEntity> FindAllOrganizations()
        {
            return _context.Organizations.ToList();
        }
    }
}