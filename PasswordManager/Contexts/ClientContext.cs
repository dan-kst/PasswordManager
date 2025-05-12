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
        public DbSet<SitePassword> SitePasswords { get; set; }
        public DbSet<Pincode> PinCodes { get; set; }
        public DbSet<SecretBase> Secrets { get; set; }
        public DbSet<ClientBase> Clients { get; set; }
        public ClientContext(DbContextOptions<ClientContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure TPH mapping for ClientBase
            modelBuilder.Entity<ClientBase>()
                .HasDiscriminator<EnumClientType>("ClientType")
                .HasValue<ClientBase>(EnumClientType.None)
                .HasValue<User>(EnumClientType.Personal)
                .HasValue<Admin>(EnumClientType.Admin);

            modelBuilder.Entity<SecretBase>()
                .HasDiscriminator<EnumSecretType>("SecretType")
                .HasValue<SecretBase>(EnumSecretType.None)
                .HasValue<SitePassword>(EnumSecretType.SitePassword)
                .HasValue<Pincode>(EnumSecretType.Pincode);

            base.OnModelCreating(modelBuilder);
        }
    }
}
