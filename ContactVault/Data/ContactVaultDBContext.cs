using ContactVault.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ContactVault.Data
{
    public class ContactVaultDBContext:DbContext
    {
        public ContactVaultDBContext(DbContextOptions options):base(options)
        {
        }
        public DbSet<Contact> Contacts { get; set; }
    }
}
