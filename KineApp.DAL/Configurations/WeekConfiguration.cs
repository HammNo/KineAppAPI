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
    public class WeekConfiguration : IEntityTypeConfiguration<Week>
    {
        public void Configure(EntityTypeBuilder<Week> builder)
        {
            builder.Property(w => w.FirstDay).IsRequired();
            builder.Property(w => w.LastDay).IsRequired();
            builder.Property(w => w.Note).HasMaxLength(1000);
            builder.HasMany(w => w.Days).WithOne(d => d.Week)
                                        .OnDelete(DeleteBehavior.Restrict);
            builder.Property(w => w.FirstDay).HasColumnType("Date");
            builder.Property(w => w.LastDay).HasColumnType("Date");
            builder.HasData(CreateWeek());
        }

        private Week CreateWeek()
        {
            return new Week
            {
                Id = Guid.NewGuid(),
                FirstDay = new DateTime(2021, 1, 4),
                LastDay = new DateTime(2021, 1, 10)
            };
        }
    }
}
