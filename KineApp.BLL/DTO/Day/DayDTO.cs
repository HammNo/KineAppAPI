using KineApp.BLL.DTO.TimeSlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.BLL.DTO.Day
{
    public class DayDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string? Note { get; set; } = string.Empty;
        public bool Visible { get; set; }
        public IEnumerable<TimeSlotDTO> TimeSlots { get; set; } = new List<TimeSlotDTO>();

        public DayDTO(DL.Entities.Day day)
        {
            Id = day.Id;
            Date = day.Date;
            Note = day.Note;
            Visible = day.Visible;
            TimeSlots = day.TimeSlots.Select(t => new TimeSlotDTO(t));
        }
    }
}
