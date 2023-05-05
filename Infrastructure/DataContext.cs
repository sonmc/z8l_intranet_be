using Microsoft.EntityFrameworkCore;
using z8l_intranet_be.Modules.PermissionModule.Dto;
using z8l_intranet_be.Modules.RolePermModule.Dto;
using z8l_intranet_be.Modules.UserModule.Dto;
using z8l_intranet_be.Modules.UserRoleModule.Dto;

namespace z8l_intranet_be.Infrastructure
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        { }

        public virtual DbSet<UserSchema> Users { get; set; }
        public virtual DbSet<RoleSchema> Roles { get; set; }
        public virtual DbSet<UserRoleSchema> UserRoles { get; set; }
        public virtual DbSet<PermissionSchema> Permissions { get; set; }
        public virtual DbSet<RolePermSchema> RolePerms { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}