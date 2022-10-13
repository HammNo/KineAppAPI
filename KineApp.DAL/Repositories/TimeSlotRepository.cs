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
    public class TimeSlotRepository : RepositoryBase<TimeSlot>, ITimeSlotRepository
    {
        public TimeSlotRepository(KineAppContext context) : base(context)
        {
        }

        public TimeSlot? GetWithUserAndDay(Guid id)
        {
            return _entities.Include(t => t.Day)
                            .Include(t => t.User)
                            .SingleOrDefault(t => t.Id == id);
        }
    }
}
