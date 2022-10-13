using KineApp.DAL.Configurations;
using KineApp.DL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.DAL.Contexts
{
    public class KineAppContext : DbContext
    {
        public KineAppContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Admin> Admins => Set<Admin>();
        public DbSet<TimeSlot> TimeSlots => Set<TimeSlot>();
        public DbSet<Day> Days => Set<Day>();
        public DbSet<Week> Weeks => Set<Week>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new AdminConfiguration());
            builder.ApplyConfiguration(new WeekConfiguration());
            builder.ApplyConfiguration(new TimeSlotConfiguration());
            builder.ApplyConfiguration(new DayConfiguration());
        }
    }
}
