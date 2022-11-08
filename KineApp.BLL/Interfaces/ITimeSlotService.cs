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
        IEnumerable<TimeSlotDTO> GetAllWaiting();
        void Register(TimeSlotRegistrationDTO command);
        Task Unregister(Guid id);
        Guid Remove(Guid id);
        Task TryRegister(TimeSlotBookingDTO command, Guid userId);
        Task ConfirmRegistration(Guid timeSlotId);
        Task RejectRegistration(Guid timeSlotId);
    }
}
