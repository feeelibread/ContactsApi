using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsApi.Models;

namespace ContactsApi.Repos
{
    public interface IContactRepository
    {
        void AddContact(Contact contact);
        void UpdateContact(Contact contact);
        void DeleteContact(Guid id);
        Contact GetContactById(Guid id);
        IEnumerable<Contact> GetAllContacts();
        IEnumerable<Contact> SearchContacts(string searchTerm);
    }
}