using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.BLL.DTO.TimeSlot
{
    public class TimeSlotRegistrationDTO
    {
        public Guid TimeSlotId { get; set; }
        public Guid UserId { get; set; }
        public string? Note { get; set; }
    }
}
