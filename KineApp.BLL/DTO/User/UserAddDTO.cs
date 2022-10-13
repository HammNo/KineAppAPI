using KineApp.DL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.BLL.DTO.User
{
    public class UserAddDTO
    {
        [Required, MinLength(2), MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MinLength(2), MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public UserGender Gender { get; set; }

        [Required, MinLength(8), MaxLength(15)]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
