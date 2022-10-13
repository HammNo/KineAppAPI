using KineApp.BLL.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.BLL.DTO.TimeSlot
{
    public class TimeSlotDTO
    {
        public Guid Id { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string? Note { get; set; }

        public bool Booked { get; set; } = false;
        public UserDTO? User { get; set; }

        public TimeSlotDTO(DL.Entities.TimeSlot timeSlot)
        {
            Id = timeSlot.Id;
            StartTime = timeSlot.StartTime;
            EndTime = timeSlot.EndTime;
            Note = timeSlot.Note;
            Booked = timeSlot.Booked;
            User = (timeSlot.User == null)? null : new UserDTO(timeSlot.User);
        }
    }
}
