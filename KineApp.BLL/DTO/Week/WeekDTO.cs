using KineApp.BLL.DTO.Day;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.BLL.DTO.Week
{
    public class WeekDTO
    {
        public Guid Id { get; set; }
        public DateTime FirstDay { get; set; }
        public DateTime LastDay { get; set; }
        public string? Note { get; set; } = string.Empty;
        public IEnumerable<DayDTO> Days { get; set; } = new List<DayDTO>();

        public WeekDTO(DL.Entities.Week week)
        {
            Id = week.Id;
            FirstDay = week.FirstDay;
            LastDay = week.LastDay;
            Note = week.Note;
            Days = week.Days.Select(d => new DayDTO(d));
        }
    }
}
