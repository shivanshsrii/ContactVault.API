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

        public ContactsController(ContactVaultDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllContacts()
        {
            var contacts = await _dbContext.Contacts.ToListAsync();
            return Ok(contacts);
        }
        [HttpPost]
        public async Task<IActionResult> AddContact(ContactRequestDto request)
        {
            var contact = new Contact
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email=request.Email,
                Phone=request.Phone,
                Favorite=request.Favorite
            };
            await _dbContext.Contacts.AddAsync(contact);
            await _dbContext.SaveChangesAsync();
            return Ok(contact);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContact(Guid id)
        {
            var contact = await _dbContext.Contacts.FindAsync(id);
            if (contact != null)
            {
                _dbContext.Contacts.Remove(contact);
                await _dbContext.SaveChangesAsync();
            }

            return Ok(contact);
        }
    }
}
