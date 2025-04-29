using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsApi.Context;
using ContactsApi.DTOs;
using ContactsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsApi.Repos
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactsDbContext _context;
        public ContactRepository(ContactsDbContext context)
        {
            _context = context;
        }

        public async Task AddContactAsync(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateContactAsync(Contact contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteContactAsync(Guid id)
        {
            var contact = _context.Contacts.Find(id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Contact> GetContactByIdAsync(Guid id)
        {
            return await _context.Contacts.FirstOrDefaultAsync(c => c.Id == id)
                   ?? throw new InvalidOperationException("Contact not found.");
        }

        public async Task<IEnumerable<Contact>> GetAllContactsAsync()
        {
            return await _context.Contacts.ToListAsync();
        }
        public async Task<IEnumerable<Contact>> SearchContactsAsync(string searchTerm)
        {
            return await _context.Contacts
                .Where(c => c.Name.Contains(searchTerm) || c.Email.Contains(searchTerm) || c.Phone.Contains(searchTerm))
                .ToListAsync();
        }

    }
}