using Microsoft.EntityFrameworkCore;
using PasswordManager.Models.Classes.Clients;
using PasswordManager.Models.Classes.Secrets;
using PasswordManager.Models.Enums;

namespace PasswordManager.Contexts
{
    public class ClientContext : DbContext
    {
        public DbSet<Admin> Admins { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ClientBase> Clients { get; set; }
        public DbSet<SitePassword> SitePasswords { get; set; }
        public DbSet<Pincode> PinCodes { get; set; }
        public DbSet<SecretBase> Secrets { get; set; }
        public ClientContext(DbContextOptions<ClientContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure TPT mapping
            modelBuilder.Entity<ClientBase>().ToTable("ClientBase");
            modelBuilder.Entity<Admin>().ToTable("Admin");
            modelBuilder.Entity<User>().ToTable("User");

            base.OnModelCreating(modelBuilder);
        }
    }
}
