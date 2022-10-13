using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.BLL.DTO.Week
{
    public class WeekAddDTO
    {
        public DateTime? DayOfModelWeek { get; set; }
        public DateTime? DayOfCreationWeek { get; set; }
        public string? Note { get; set; }
    }
}
