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
    public class TimeSlotConfiguration : IEntityTypeConfiguration<TimeSlot>
    {
        public void Configure(EntityTypeBuilder<TimeSlot> builder)
        {
            builder.Property(t => t.StartTime).IsRequired();
            builder.Property(t => t.EndTime).IsRequired();
            builder.Property(d => d.Status).IsRequired();
            builder.Property(d => d.Note).HasMaxLength(1000);
            builder.HasOne(t => t.User).WithMany(u => u.TimeSlots)
                                       .HasForeignKey(t => t.UserId)
                                       .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.Day).WithMany(d => d.TimeSlots)
                                      .HasForeignKey(t => t.DayId)
                                      .OnDelete(DeleteBehavior.Restrict);
            //builder.HasData(CreateSlots());
        }

        //public IEnumerable<TimeSlot> CreateSlots()
        //{
        //    yield return new TimeSlot
        //    {
        //        Id = Guid.NewGuid(),
        //        StartTime = TimeSpan.FromHours(14),
        //        EndTime = TimeSpan.FromHours(14.50),
        //        Booked = false,
        //        DayId = 1
        //    };
        //    yield return new TimeSlot
        //    {
        //        Id = Guid.NewGuid(),
        //        StartTime = TimeSpan.FromHours(15.50),
        //        EndTime = TimeSpan.FromHours(16),
        //        Booked = false,
        //        DayId = 1
        //    };
        //    yield return new TimeSlot
        //    {
        //        Id = Guid.NewGuid(),
        //        StartTime = TimeSpan.FromHours(8.50),
        //        EndTime = TimeSpan.FromHours(9),
        //        Booked = false,
        //        DayId = 3
        //    };
        //    yield return new TimeSlot
        //    {
        //        Id = Guid.NewGuid(),
        //        StartTime = TimeSpan.FromHours(10.50),
        //        EndTime = TimeSpan.FromHours(11),
        //        Booked = false,
        //        DayId = 3
        //    };
        //    yield return new TimeSlot
        //    {
        //        Id = Guid.NewGuid(),
        //        StartTime = TimeSpan.FromHours(13.50),
        //        EndTime = TimeSpan.FromHours(14),
        //        Booked = false,
        //        DayId = 3
        //    };
        //    yield return new TimeSlot
        //    {
        //        Id = Guid.NewGuid(),
        //        StartTime = TimeSpan.FromHours(10),
        //        EndTime = TimeSpan.FromHours(10.50),
        //        Booked = false,
        //        DayId = 5
        //    };
        //}
    }
}
