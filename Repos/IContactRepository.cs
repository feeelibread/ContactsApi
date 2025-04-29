using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsApi.Models;

namespace ContactsApi.Repos
{
    public interface IContactRepository
    {
        Task AddContactAsync(Contact contact);
        Task UpdateContactAsync(Contact contact);
        Task DeleteContactAsync(Guid id);
        Task<Contact> GetContactByIdAsync(Guid id);
        Task<IEnumerable<Contact>> GetAllContactsAsync();
        Task<IEnumerable<Contact>> SearchContactsAsync(string searchTerm);
    }
}