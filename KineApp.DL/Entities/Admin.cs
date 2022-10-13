using KineApp.DL.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.DL.Entities
{
    public class Admin
    {
        #region columns
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public AdminRole Role { get; set; }

        public byte[] EncodedPassword { get; set; } = Array.Empty<byte>();

        public Guid Salt { get; set; } 
        #endregion
    }
}
