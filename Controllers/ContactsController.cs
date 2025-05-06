using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ContactsApi.DTOs;
using ContactsApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ContactsApi.Controllers
{
    [Route("[controller]")]
    public class ContactsController : Controller
    {
        private readonly ContactService _contactService;
        public ContactsController(ContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(Guid id)
        {
            try
            {
                var contact = await _contactService.GetContactById(id);
                if (contact == null)
                {
                    return NotFound("Contact not found.");
                }
                return Ok(contact);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("GetAllContacts")]
        public async Task<IActionResult> GetAllContacts()
        {
            try
            {
                var contacts = await _contactService.GetAllContacts();
                if (contacts == null || !contacts.Any())
                {
                    return NotFound("No contacts found.");
                }
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("SearchContacts")]
        public async Task<IActionResult> SearchContacts(string searchTerm)
        {
            try
            {
                var contacts = await _contactService.SearchContacts(searchTerm);
                return Ok(contacts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("AddContact")]
        public async Task<IActionResult> AddContact([FromBody] CreateContactDto dto)
        {
            try
            {

                var contact = await _contactService.AddContactAsync(dto);
                if (contact == null)
                {
                    return BadRequest("Invalid contact data.");
                }
                return CreatedAtAction(nameof(GetContactById), new { id = contact.Id }, contact);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("UpdateContact/{id}")]
        public async Task<IActionResult> UpdateContact(Guid id, [FromBody] CreateContactDto dto)
        {
            try
            {
                await _contactService.UpdateContactAsync(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("DeleteContact/{id}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            try
            {
                await _contactService.DeleteContact(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}