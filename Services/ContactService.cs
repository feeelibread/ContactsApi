using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContactsApi.DTOs;
using ContactsApi.Models;
using ContactsApi.Repos;

namespace ContactsApi.Services
{
    public class ContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task AddContactAsync(CreateContactDto dto)
        {
            var contact = new Contact
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow,
            };
            await _contactRepository.AddContactAsync(contact);
        }
        public async Task UpdateContactAsync(Guid id, CreateContactDto dto)
        {
            var contact = await _contactRepository.GetContactByIdAsync(id);
            if (contact == null)
            {
                throw new InvalidOperationException("Contact not found.");
            }

            contact.Name = dto.Name;
            contact.Email = dto.Email;
            contact.Phone = dto.Phone;
            contact.Description = dto.Description;
            contact.UpdatedAt = DateTime.UtcNow;

            await _contactRepository.UpdateContactAsync(contact);
        }
        public async Task DeleteContact(Guid id)
        {
            var contact = await _contactRepository.GetContactByIdAsync(id);
            if (contact == null)
            {
                throw new InvalidOperationException("Contact not found.");
            }

            await _contactRepository.DeleteContactAsync(id);
        }
        public async Task<Contact> GetContactById(Guid id)
        {
            var contact = await _contactRepository.GetContactByIdAsync(id);
            if (contact == null)
            {
                throw new InvalidOperationException("Contact not found.");
            }
            return await _contactRepository.GetContactByIdAsync(id);
        }
        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            var contacts = await _contactRepository.GetAllContactsAsync();
            if (contacts == null || !contacts.Any())
            {
                throw new InvalidOperationException("No contacts found.");
            }

            return await _contactRepository.GetAllContactsAsync();
        }
        public async Task<IEnumerable<Contact>> SearchContacts(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                throw new ArgumentException("Search term cannot be empty.", nameof(searchTerm));
            }
            var contacts = await _contactRepository.SearchContactsAsync(searchTerm);

            if (contacts == null || !contacts.Any())
            {
                throw new InvalidOperationException("No contacts found.");
            }
            return await _contactRepository.SearchContactsAsync(searchTerm);
        }

    }
}