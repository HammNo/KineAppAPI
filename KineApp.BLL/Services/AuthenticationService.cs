using KineApp.BLL.DTO.Admin;
using KineApp.BLL.DTO.User;
using KineApp.BLL.Interfaces;
using KineApp.DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Security.Utils;

namespace KineApp.BLL.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;

        public AuthenticationService(IAdminRepository adminRepository, IUserRepository userRepository)
        {
            _adminRepository = adminRepository;
            _userRepository = userRepository;
        }

        public AdminDTO AdminLogin(AdminLoginDTO dto)
        {
            Admin? admin = _adminRepository.FindOne(a => a.Name == dto.Username);
            if (admin is null || !HashUtils.VerifyPassword(admin.EncodedPassword, dto.Password, admin.Salt))
            {
                throw new AuthenticationException();
            }
            return new AdminDTO(admin);
        }

        public UserDTO UserLogin(UserLoginDTO dto)
        {
            User? user = _userRepository.FindOne(u => u.Email != null 
                                                      && u.FirstName.ToLower() == dto.FirstName.ToLower() 
                                                      && u.LastName.ToLower() == dto.LastName.ToLower()
                                                      && u.Email.ToLower() == dto.Email.ToLower());
            if (user is null)
            {
                throw new AuthenticationException();
            }
            return new UserDTO(user);
        }
    }
}
