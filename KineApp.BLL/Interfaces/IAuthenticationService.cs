using KineApp.BLL.DTO.Admin;
using KineApp.BLL.DTO.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.BLL.Interfaces
{
    public interface IAuthenticationService
    {
        AdminDTO AdminLogin(AdminLoginDTO dto);
        UserDTO UserLogin(UserLoginDTO dto);
    }
}
