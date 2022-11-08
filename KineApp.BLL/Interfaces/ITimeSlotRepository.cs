using KineApp.DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.EF.Repository;

namespace KineApp.BLL.Interfaces
{
    public interface ITimeSlotRepository : IRepository<TimeSlot>
    {
        TimeSlot? GetWithUserAndDay(Guid id);
        IEnumerable<TimeSlot> GetAllWaitingForConfirmation();
    }
}
