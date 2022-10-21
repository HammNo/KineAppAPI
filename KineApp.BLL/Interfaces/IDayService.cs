using KineApp.BLL.DTO.Day;
using KineApp.BLL.DTO.TimeSlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.BLL.Interfaces
{
    public interface IDayService
    {
        DayDTO GetDay(DaySearchDTO query, bool getAsAdmin);
        void RevealDay(Guid id);
        Guid AddTimeSlot(TimeSlotAddDTO command);
    }
}
