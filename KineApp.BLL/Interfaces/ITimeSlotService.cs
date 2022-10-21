using KineApp.BLL.DTO.TimeSlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.BLL.Interfaces
{
    public interface ITimeSlotService
    {
        void Register(TimeSlotRegistrationDTO command, bool hasAdminRights);
        void Unregister(Guid id);
        Guid Remove(Guid id);
        void BookByUser(TimeSlotBookingDTO command, Guid? userId);
    }
}
