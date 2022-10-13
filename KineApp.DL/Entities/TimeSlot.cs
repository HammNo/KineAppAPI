using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.DL.Entities
{
    public class TimeSlot
    {
        #region columns
        public Guid Id { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }

        public string? Note { get; set; }

        public bool Booked { get; set; } = false;
        #endregion

        #region relations
        public Guid? UserId { get; set; } = null;
        public User? User { get; set; } = null;
        public Guid DayId { get; set; }
        public Day Day { get; set; }
        #endregion
    }
}
