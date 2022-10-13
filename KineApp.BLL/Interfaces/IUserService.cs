using KineApp.BLL.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.BLL.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDTO> FindUsers(UserSearchDTO query);
        Guid Add(UserAddDTO command);
        Guid Remove(Guid id);
    }
}
