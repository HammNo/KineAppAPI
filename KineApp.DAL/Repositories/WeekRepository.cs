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
    public class WeekRepository : RepositoryBase<Week>, IWeekRepository
    {
        public WeekRepository(KineAppContext context) : base(context)
        {
        }

        public Week? GetByIdWithDays(Guid id)
        {
            return _entities.Include(w => w.Days)
                            .SingleOrDefault(w => w.Id == id);  
        }

        public Week? GetWeekWithDaysAndTS(DateTime date, bool getNotVDays)
        {
            return _entities.Include(w => w.Days.Where(d => getNotVDays || d.Visible).OrderBy(d => d.Date))
                            .ThenInclude(d => d.TimeSlots)
                            .SingleOrDefault(w => w.FirstDay == date);
        }

        public Week? GetWeekWithDaysAndTSAndUsers(DateTime date, bool getNotVDays)
        {
            return _entities.Include(w => w.Days.Where(d => getNotVDays || d.Visible).OrderBy(d => d.Date))
                            .ThenInclude(d => d.TimeSlots)
                            .ThenInclude(t => t.User)
                            .SingleOrDefault(w => w.FirstDay == date);
        }
    }
}
