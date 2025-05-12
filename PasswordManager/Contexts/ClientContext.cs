using Microsoft.EntityFrameworkCore;
using PasswordManager.Models;

namespace PasswordManager.Contexts
{
    public class ClientContext : DbContext
    {
        DbSet<Admin> Admins { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<SitePassword> SitePasswords { get; set; }
        DbSet<Pincode> PinCodes { get; set; }
        public ClientContext(DbContextOptions<ClientContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
