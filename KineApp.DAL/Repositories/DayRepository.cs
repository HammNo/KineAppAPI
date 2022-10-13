using KineApp.BLL.Interfaces;
using KineApp.DAL.Contexts;
using KineApp.DL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.EF.Repository;

namespace KineApp.DAL.Repositories
{
    public class DayRepository : RepositoryBase<Day>, IDayRepository
    {
        public DayRepository(KineAppContext context) : base(context)
        {
        }

        public Day? GetDayWithTimeSlots(DateTime date, bool getNotVisible)
        {
            return _entities.Include(d => d.TimeSlots)
                            .SingleOrDefault(d => (d.Date == date) && (getNotVisible || d.Visible));
        }

        public Day? GetDayWithTimeSlotsAndUsers(DateTime date, bool getNotVisible)
        {
            return _entities.Include(d => d.TimeSlots)
                            .ThenInclude(t => t.User)
                            .SingleOrDefault(d => (d.Date == date) && (getNotVisible || d.Visible));
        }
    }
}
