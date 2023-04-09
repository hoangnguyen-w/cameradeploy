using Microsoft.EntityFrameworkCore;

namespace CameraBase.Entity
{
    public class CameraBasedContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<NotifiHistory> NotifiHistories { get; set; }

        public DbSet<CarManagement> carManagements { get; set; }

        public DbSet<Carlocator> Carlocators { get; set; }


        public CameraBasedContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Hàm này để ép dữ liệu mặc định
            this.SeedRoles(modelBuilder);
            this.SeedAccounts(modelBuilder);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData(new Role()
            {
                RoleID = 1,
                RoleName = "Admin"
            });
            builder.Entity<Role>().HasData(new Role()
            {
                RoleID = 2,
                RoleName = "Customer"
            });
            builder.Entity<Role>().HasData(new Role()
            {
                RoleID = 3,
                RoleName = "Owner"
            });
        }

        private void SeedAccounts(ModelBuilder builder)
        {
            builder.Entity<Account>().HasData(new Account()
            {
                AccountID = 1,
                AccounName = "admin",
                AccountEmail = "Admin@gmail.com",
                password = "123",
                FullName = "ADMIN",
                RoleID = 1,
            }); 
        }
    }
}
