using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.BLL.DTO.TimeSlot
{
    public class TimeSlotBookingDTO
    {
        [Required]
        public Guid TimeSlotId { get; set; }
        public string? Note { get; set; }
    }
}
