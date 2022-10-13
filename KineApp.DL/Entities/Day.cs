using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.DL.Entities
{
    public class Day
    {
        #region columns
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public string? Note { get; set; }

        public bool Visible { get; set; }
        #endregion

        #region relations
        public Guid WeekId { get; set; }
        public Week Week { get; set; }
        public ICollection<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
        #endregion   
    }
}
