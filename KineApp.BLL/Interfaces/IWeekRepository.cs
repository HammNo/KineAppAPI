using KineApp.DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.EF.Repository;

namespace KineApp.BLL.Interfaces
{
    public interface IWeekRepository : IRepository<Week>
    {
        public Week? GetDetailedWeek(DateTime date, bool getNotVDays);
        public Week? GetDetailedWeekWithUsers(DateTime date, bool getNotVDays);
    }
}
