using ContactVault.Data;
using ContactVault.Models;
using ContactVault.Models.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContactVault.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly ContactVaultDBContext _dbContext;

        public ContactsController(ContactVaultDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Contacts
        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            var contacts = await _dbContext.Contacts.ToListAsync();
            return Ok(contacts);
        }

        // POST: api/Contacts
        [HttpPost]
        public async Task<IActionResult> AddContact(ContactRequestDto request)
        {
            var contact = new Contact
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                Favorite = request.Favorite
            };

            await _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();

            // Return the created contact with 201 Created status
            return CreatedAtAction(nameof(GetContactById), new { id = contact.Id }, contact);
        }

        // GET: api/Contacts/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(Guid id)
        {
            var contact = await _dbContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        // DELETE: api/Contacts/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            var contact = await _dbContext.Contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _dbContext.Contacts.Remove(contact);
            await _dbContext.SaveChangesAsync();

            return Ok(contact);
        }
    }
}
