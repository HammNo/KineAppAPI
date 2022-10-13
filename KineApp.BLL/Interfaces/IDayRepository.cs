using KineApp.DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.EF.Repository;

namespace KineApp.BLL.Interfaces
{
    public interface IDayRepository : IRepository<Day>
    {
        public Day? GetDayWithTimeSlots(DateTime date, bool getNotVisible);
        public Day? GetDayWithTimeSlotsAndUsers(DateTime date, bool getNotVisible);
    }
}
