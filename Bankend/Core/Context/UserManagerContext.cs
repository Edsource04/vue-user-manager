using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Mappings;
using Core.Entities;

namespace Core.Context
{
    public class UserManagerContext : DbContext
    {
        public UserManagerContext(DbContextOptions<UserManagerContext> options) : base(options)
        {

        }

        public DbSet<Permission>? Permissions { get; set; }
        public DbSet<PermissionType>? PermissionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
 
            modelBuilder.ApplyConfiguration<Permission>(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration<PermissionType>(new PermissionTypeConfiguration());

        }
    }
}
