using KineApp.DL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.BLL.DTO.User
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string FistName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public UserGender Gender { get; set; }
        public string? Email { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public bool IsValid { get; set; }

        public UserDTO(DL.Entities.User user)
        {
            Id = user.Id;
            FistName = user.FirstName;
            LastName = user.LastName;
            Gender = user.Gender;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            IsValid = (user.ValidationCode == null) ? true : false;
        }
    }
}
