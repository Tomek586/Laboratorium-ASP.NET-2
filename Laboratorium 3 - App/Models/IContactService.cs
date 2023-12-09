using Data.Entities;
using Lab3___Aplikacja.Models;

namespace Laboratorium_3___App.Models
{
    public interface IContactService
    {
        int Add(Contact contat);
        void Delete(int id);
        void Update(Contact contact);
        List<Contact> FindAll();
        Contact? FindById(int id);
        List<OrganizationEntity> FindAllOrganizations();
        PagingList<Contact> FindPage(int page, int size);
    }
}
