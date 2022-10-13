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
    public class DayConfiguration : IEntityTypeConfiguration<Day>
    {
        public void Configure(EntityTypeBuilder<Day> builder)
        {
            builder.Property(d => d.Date).IsRequired();
            builder.Property(d => d.Visible).IsRequired();
            builder.Property(d => d.Note).HasMaxLength(1000);
            builder.HasMany(d => d.TimeSlots).WithOne(t => t.Day)
                                             .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(d => d.Week).WithMany(w => w.Days)
                                       .HasForeignKey(t => t.WeekId)
                                       .OnDelete(DeleteBehavior.Restrict);
            builder.Property(d => d.Date).HasColumnType("Date");
            //builder.HasData(CreateDays());
        }

        //private IEnumerable<Day> CreateDays()
        //{
        //    for (int i = 1; i <= 7; i++)
        //    {
        //        yield return new Day
        //        {
        //            Id = Guid.NewGuid(),
        //            Date = new DateTime(2021, 1, 3 + i),
        //            WeekId = 1,
        //            Visible = true
        //        };
        //    }
        //}
    }
}
