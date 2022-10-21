using KineApp.DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.FirstName).IsRequired();
            builder.Property(u => u.LastName).IsRequired();
            builder.Property(u => u.Gender).IsRequired();
            builder.Property(u => u.PhoneNumber).IsRequired();
            builder.Property(u => u.FirstName).HasMaxLength(50);
            builder.Property(u => u.LastName).HasMaxLength(50);
            builder.Property(u => u.PhoneNumber).HasMaxLength(15);
            builder.HasMany(u => u.TimeSlots).WithOne(t => t.User)
                                             .OnDelete(DeleteBehavior.Restrict);
            builder.HasData(CreateUser());
        }

        private IEnumerable<User> CreateUser()
        {
            yield return new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Firsty",
                LastName = "Zero",
                Gender = DL.Enums.UserGender.Other,
                Email = "test@mail.com",
                PhoneNumber = "0111111111",
                ValidationCode = null
            };
        }
    }
}
