using KineApp.DL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.BLL.DTO.Admin
{
    public class AdminDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public AdminRole Role { get; set; }

        public AdminDTO(DL.Entities.Admin admin)
        {
            Id = admin.Id;
            Name = admin.Name;
            Email = admin.Email;
            Role = admin.Role;
        }
    }
}
