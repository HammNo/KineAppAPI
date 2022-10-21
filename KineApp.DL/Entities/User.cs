using KineApp.DL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.DL.Entities
{
    public class User
    {
        #region columns
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? Email { get; set; }

        public UserGender Gender { get; set; }

        public string PhoneNumber { get; set; } = string.Empty;

        public Guid? ValidationCode { get; set; }
        #endregion

        #region relations
        public ICollection<TimeSlot> TimeSlots { get; set; } = new List<TimeSlot>();
        #endregion    
    }
}
