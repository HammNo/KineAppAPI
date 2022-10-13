using KineApp.BLL.DTO.User;
using KineApp.DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.BLL.Mappers
{
    public static class UserMappers
    {
        public static User ToEntity(this UserAddDTO command)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                FirstName = command.FirstName,
                LastName = command.LastName,
                Gender = command.Gender,
                Email = command.Email,
                PhoneNumber = command.PhoneNumber
            };
        }
    }
}
