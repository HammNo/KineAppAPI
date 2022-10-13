using KineApp.DL.Entities;
using KineApp.DL.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Security.Utils;

namespace KineApp.DAL.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.Property(a => a.Name).IsRequired();
            builder.Property(a => a.Email).IsRequired();
            builder.Property(a => a.Role).IsRequired();
            builder.Property(a => a.EncodedPassword).IsRequired();
            builder.Property(a => a.Salt).IsRequired();
            builder.HasIndex(a => a.Name).IsUnique();
            builder.HasIndex(a => a.Email).IsUnique();
            builder.HasIndex(a => a.Salt).IsUnique();
            builder.HasData(CreateAdmin());
        }

        private IEnumerable<Admin> CreateAdmin()
        {
            Guid salt = Guid.NewGuid();
            yield return new Admin
            {
                Id = Guid.NewGuid(),
                Name = "Admin",
                Email = "admin@mail.be",
                Role = AdminRole.Superadmin,
                Salt = salt,
                EncodedPassword = HashUtils.HashPassword("verysecret1234", salt)
            };
        }
    }
}
