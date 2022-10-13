using KineApp.BLL.DTO.User;
using KineApp.BLL.Exceptions;
using KineApp.BLL.Interfaces;
using KineApp.BLL.Mappers;
using KineApp.DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KineApp.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<UserDTO> FindUsers(UserSearchDTO query)
        {
            return _userRepository.FindUsersWithAppointmentsByCriteria(query.FirstName, query.LastName, query.Email, query.PhoneNumber, query.Offset)
                                  .Select(u => new UserDTO(u));
        }

        public Guid Add(UserAddDTO command)
        {
            if (_userRepository.ExistUser(command.ToEntity()))
            {
                throw new UserException("User already exists");
            }
            User newUser = _userRepository.Add(command.ToEntity());
            return newUser.Id;
        }

        public Guid Remove(Guid id)
        {
            User? user = _userRepository.FindOneWithAppointements(id);
            if(user is null)
            {
                throw new KeyNotFoundException();
            }
            if(user.TimeSlots.Count > 0)
            {
                throw new UserException("User with appointement can't be deleted");
            }
            _userRepository.Remove(user);
            return id;
        }
    }
}
