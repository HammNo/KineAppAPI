using KineApp.BLL.DTO.User;
using KineApp.BLL.Exceptions;
using KineApp.BLL.Interfaces;
using KineApp.BLL.Mappers;
using KineApp.BLL.Templates;
using KineApp.DL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace KineApp.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMailer _mailer;

        public UserService(IUserRepository userRepository, IMailer mailer)
        {
            _userRepository = userRepository;
            _mailer = mailer;
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

        public async Task<Guid> Register(UserAddDTO command)
        {
            if (_userRepository.ExistUser(command.ToEntity()))
            {
                throw new UserException("User already exists");
            }
            if (command.Email == null)
            {
                throw new UserException("A valid mail adress is required");
            }
            Guid validationCode = Guid.NewGuid();
            User tmpUser = command.ToEntity();
            tmpUser.ValidationCode = validationCode;
            User newUser;
            using TransactionScope t = new(TransactionScopeAsyncFlowOption.Enabled);
            {
                newUser = _userRepository.Add(tmpUser);
                await _mailer.Send(
                    "Inscription KineApp",
                    MailTemplates.UserRegister
                        .Replace("__mail__", command.Email)
                        .Replace("__lastname__", command.LastName)
                        .Replace("__firstname__", command.FirstName)
                        .Replace("__userid__", newUser.Id.ToString())
                        .Replace("__validationcode__", validationCode.ToString()),
                    command.Email
                );
            }
            t.Complete();
            return newUser.Id;
        }

        public void Validate(Guid userId, Guid validationCode)
        {
            User? user = _userRepository.FindOne(u => u.Id == userId);
            if (user == null)
            {
                throw new KeyNotFoundException();
            }
            if (user.ValidationCode == null)
            {
                throw new UserException("User is already valid");
            }
            if (user.ValidationCode != validationCode)
            {
                Remove(user.Id);
                throw new UserException("Wrong validation code, user has been deleted");
            }
            user.ValidationCode = null;
            _userRepository.Update(user);
        }
    }
}
