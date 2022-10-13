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
        void Register(TimeSlotRegistrationDTO command);
    }
}
